using IpManager.Serialization;
using IpManager.Server.Models;
using IpManager.Server.Services;
using IpManager.SharedSettings;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace IpManager.Server;

public class Server(string host, int port)
{
    public delegate void RequestRecievedHandler(IPEndPoint remoteIp);
    public event RequestRecievedHandler? RequestRecieved;

    public delegate void StartedHandler();
    public event StartedHandler? Started;

    public async Task StartAsync()
    {
        string prefixBase = $"http://{host}:{port}";

        using var server = new HttpListener();
        server.Prefixes.Add(ServerRoutes.FavoriteIpsUrl);
        server.Prefixes.Add(ServerRoutes.OrganizationsUrl);

        server.Start();
        Started?.Invoke();

        while (true)
        {
            HttpListenerContext context = await server.GetContextAsync();
            RequestRecieved?.Invoke(context.Request.RemoteEndPoint);

            ProcessingRequestResult processingResult = await ProcessRequest(context.Request);

            await ResponseService.SendResponseAsync(
                context.Response,
                processingResult.ResponseContent,
                processingResult.StatusCode);
        }
    }

    private static async Task<ProcessingRequestResult> ProcessRequest(HttpListenerRequest request)
    {
        ArgumentNullException.ThrowIfNull(request, nameof(request));

        using var requestContentReader = new StreamReader(request.InputStream);

        string requestContent = requestContentReader.ReadToEnd();
        string requestUrl = request.RawUrl ?? string.Empty;

        Dictionary<string, string> parameters = RequestService.ExtractParametersFromUrl(requestUrl);

        return request.HttpMethod switch
        {
            "GET" => requestUrl.StartsWith(ServerRoutes.FavoriteIpsRoute)
                ? await GetFavoriteIps()
                : requestUrl.StartsWith(ServerRoutes.OrganizationsRoute)
                ? await GetOrganizations(parameters)
                : new ProcessingRequestResult(string.Empty, HttpStatusCode.NotFound),

            "PUT" => !string.IsNullOrEmpty(requestContent)
                ? await AddFavoriteIp(requestContent)
                : new ProcessingRequestResult(string.Empty, HttpStatusCode.BadRequest),

            _ => new ProcessingRequestResult(string.Empty, HttpStatusCode.OK),
        };
    }

    private static async Task<ProcessingRequestResult> AddFavoriteIp(string ipJson)
    {
        Ip? ip = JsonConvert.DeserializeObject<Ip>(ipJson);

        if (ip is null)
            return new ProcessingRequestResult(string.Empty, HttpStatusCode.BadRequest);

        await DatabaseConnectionService.FavoriteIpRepository.AddIpAsync(ip);
        return new ProcessingRequestResult(string.Empty, HttpStatusCode.OK);
    }

    private static async Task<ProcessingRequestResult> GetFavoriteIps()
    {
        List<Ip> ips = await DatabaseConnectionService.FavoriteIpRepository
            .FindAllIpsAsync()
            .ToListAsync();

        var collection = new IpCollection()
        {
            Ips = ips,
        };

        string content = JsonConvert.SerializeObject(collection);
        return new ProcessingRequestResult(content, HttpStatusCode.OK);
    }

    private static async Task<ProcessingRequestResult> GetOrganizations(Dictionary<string, string> parameters)
    {
        const string patternParameterKey = "pattern";
        const string maxCountParameterKey = "maxCount";

        _ = parameters.TryGetValue(patternParameterKey, out string? pattern);

        int maxCount = parameters.TryGetValue(maxCountParameterKey, out string? value)
            ? int.Parse(value)
            : short.MaxValue;

        if (string.IsNullOrEmpty(pattern))
            return new ProcessingRequestResult(string.Empty, HttpStatusCode.BadRequest);

        List<Organization> organizations = await DatabaseConnectionService.OrganizationRepository
            .FindOrganizationsAsync(pattern, maxCount)
            .ToListAsync();

        var collection = new OrganizationCollection()
        {
            Organizations = organizations,
        };

        string content = JsonConvert.SerializeObject(collection);
        return new ProcessingRequestResult(content, HttpStatusCode.OK);
    }
}
