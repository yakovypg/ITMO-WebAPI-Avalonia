using StarWars.Server.DataAccess;
using StarWars.Server.Infrastructure.Services;
using StarWars.Server.Models;
using StarWars.SharedConfig;
using System.Net;
using System.Text;

namespace StarWars.Server.Servers
{
    public class FavoritesEntitiesServer(string host, int port, string protocol)
    {
        public delegate void StartedHandler();
        public event StartedHandler? Started;

        public delegate void RequestRecievedHandler(string httpMethod, IPEndPoint remoteEndPoint);
        public event RequestRecievedHandler? RequestRecieved;

        public string Route { get; } = ServerConfig.FavoriteEntitiesRoute;

        public async Task Start()
        {
            string prefixStart = $"{protocol}://{host}:{port}";

            using var server = new HttpListener();
            server.Prefixes.Add($"{prefixStart}{Route}");
            server.Start();

            Started?.Invoke();

            while (true)
            {
                HttpListenerContext context = await server.GetContextAsync();
                HttpListenerRequest request = context.Request;

                RequestRecieved?.Invoke(request.HttpMethod, request.RemoteEndPoint);

                PerformRequestResult result = await PerformRequest(context.Request);
                await SendResponse(context.Response, result.ResponseContent, result.StatusCode);
            }
        }

        private static async Task SendResponse(HttpListenerResponse response, string content, HttpStatusCode status = HttpStatusCode.OK)
        {
            ArgumentNullException.ThrowIfNull(response, nameof(response));
            ArgumentNullException.ThrowIfNull(content, nameof(content));

            await SendResponse(response, content, Encoding.UTF8, status);
        }

        private static async Task SendResponse(HttpListenerResponse response, string content, Encoding encoding, HttpStatusCode status = HttpStatusCode.OK)
        {
            ArgumentNullException.ThrowIfNull(response, nameof(response));
            ArgumentNullException.ThrowIfNull(content, nameof(content));

            response.StatusCode = (int)status;
            response.Headers.Add("Access-Control-Allow-Origin", "*");
            response.Headers.Add("Access-Control-Allow-Methods", "POST, PUT, OPTIONS, DELETE, GET");

            byte[] buffer = encoding.GetBytes(content);

            response.ContentLength64 = buffer.Length;
            using Stream output = response.OutputStream;

            await output.WriteAsync(buffer);
            await output.FlushAsync();
        }

        private async Task<PerformRequestResult> PerformRequest(HttpListenerRequest request)
        {
            using StreamReader requestContentReader = new(request.InputStream);

            string requestContent = requestContentReader.ReadToEnd();
            string requestUrlContent = GetRequestUrlContent(request.RawUrl);

            IEntityRepository repository = await FavoriteEntityService.GetFavoriteEntityRepository();

            switch (request.HttpMethod)
            {
                case "GET":
                    if (!string.IsNullOrEmpty(requestUrlContent))
                        return new PerformRequestResult(string.Empty, HttpStatusCode.NotFound);

                    string[] entities = await repository.FindAllEntitiesAsync().ToArrayAsync();
                    string content = string.Join(' ', entities);

                    return new PerformRequestResult(content, HttpStatusCode.OK);

                case "DELETE":
                    if (string.IsNullOrEmpty(requestUrlContent))
                        return new PerformRequestResult(string.Empty, HttpStatusCode.NotFound);

                    await repository.RemoveAsync(requestUrlContent);
                    return new PerformRequestResult(string.Empty, HttpStatusCode.OK);

                case "PUT":
                    if (string.IsNullOrEmpty(requestContent))
                        return new PerformRequestResult(string.Empty, HttpStatusCode.BadRequest);

                    await repository.AddAsync(requestContent);
                    return new PerformRequestResult(string.Empty, HttpStatusCode.OK);

                default:
                    return new PerformRequestResult(string.Empty, HttpStatusCode.OK);
            }
        }

        private string GetRequestUrlContent(string? rawUrl)
        {
            return rawUrl?.Replace(Route, string.Empty) ?? string.Empty;
        }
    }
}
