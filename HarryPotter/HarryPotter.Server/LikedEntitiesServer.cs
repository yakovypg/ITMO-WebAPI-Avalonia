using HarryPotter.Server.Loggers;
using HarryPotter.Server.Services;
using HarryPotter.ServerConnection.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace HarryPotter.Server;

using HandleRequestResult = (HttpStatusCode status, string content);

public class LikedEntitiesServer(int port, string host, ILogger? logger = null)
{
    private const string _likedEntitiesRoute = "/likes/";

    public int Port { get; } = port;
    public string Host { get; } = host ?? throw new ArgumentNullException(nameof(host));
    public ILogger? Logger { get; set; } = logger;

    public string ServerUrl => $"http://{Host}:{Port}";
    public string LikedEntitiesUrl => $"{ServerUrl}{_likedEntitiesRoute}";

    public async Task StartAsync()
    {
        using HttpListener server = new();
        server.Prefixes.Add(LikedEntitiesUrl);
        server.Start();

        Logger?.WriteLine("Server started");
        Logger?.WriteLine($"Listen {LikedEntitiesUrl}");

        while (true)
        {
            HttpListenerContext context = await server.GetContextAsync();

            IPEndPoint ip = context.Request.RemoteEndPoint;
            Logger?.WriteLine($"Request from {ip} recieved");

            (HttpStatusCode status, string content) = await HandleRequest(context.Request);
            Logger?.WriteLine($"Request from {ip} processed");

            await ResponseConfigurationService.SendResponseAsync(
                context.Response,
                status,
                content);

            Logger?.WriteLine($"Response to {ip} sent");
        }
    }

    private static async Task<HandleRequestResult> HandleRequest(
        HttpListenerRequest request)
    {
        ArgumentNullException.ThrowIfNull(request, nameof(request));

        using StreamReader contentReader = new(request.InputStream);

        string content = contentReader.ReadToEnd();
        string rawUrl = request.RawUrl ?? string.Empty;
        string entityId = rawUrl.Replace(_likedEntitiesRoute, string.Empty);

        return request.HttpMethod switch
        {
            "GET" => string.IsNullOrEmpty(content)
                ? await GetLikedEntities()
                : (HttpStatusCode.BadRequest, string.Empty),

            "DELETE" => !string.IsNullOrEmpty(entityId)
                ? await DeleteLikedEntity(entityId)
                : (HttpStatusCode.BadRequest, string.Empty),

            "PUT" => !string.IsNullOrEmpty(content)
                ? await AddLikedEntity(content)
                : (HttpStatusCode.BadRequest, string.Empty),

            _ => (HttpStatusCode.OK, string.Empty),
        };
    }

    private static async Task<HandleRequestResult> AddLikedEntity(string likedEntityJson)
    {
        LikedItem? likedEntity = JsonConvert.DeserializeObject<LikedItem>(likedEntityJson);

        if (likedEntity is null)
            return (HttpStatusCode.BadRequest, string.Empty);

        await PostgresConnectionService.LikedEntitiesHandler.AddAsync(likedEntity);
        
        return (HttpStatusCode.OK, string.Empty);
    }

    private static async Task<HandleRequestResult> DeleteLikedEntity(string likedEntityId)
    {
        await PostgresConnectionService.LikedEntitiesHandler.DeleteAsync(likedEntityId);
        return (HttpStatusCode.OK, string.Empty);
    }

    private static async Task<HandleRequestResult> GetLikedEntities()
    {
        List<string> likedEntities = await PostgresConnectionService.LikedEntitiesRepository
            .FindAllAsync()
            .Select(t => t.ItemJson)
            .ToListAsync();

        string content = $"[{string.Join(',', likedEntities)}]";
        return (HttpStatusCode.OK, content);
    }
}
