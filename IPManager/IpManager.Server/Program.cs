using IpManager.Server;
using IpManager.SharedSettings;
using System;

var server = new Server(ServerSettings.Host, ServerSettings.Port);

server.Started += () => Console.WriteLine("Listening...");
server.RequestRecieved += ip => Console.WriteLine($"Request from {ip}");

await server.StartAsync();
