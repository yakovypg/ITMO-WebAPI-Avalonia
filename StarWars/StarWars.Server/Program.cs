using StarWars.SharedConfig;
using StarWars.Server.Servers;

var server = new FavoritesEntitiesServer(
    ServerConfig.Host,
    ServerConfig.Port,
    ServerConfig.Protocol);

server.Started += () =>
    Console.WriteLine("Server started. Listening...");

server.RequestRecieved += (httpMethod, remoteEndPoint) =>
    Console.WriteLine($"{httpMethod} request from {remoteEndPoint}");

await server.Start();
