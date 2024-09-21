using HarryPotter.Server;
using HarryPotter.Server.Loggers;

const int port = 7654;
const string host = "localhost";

var logger = new ConsoleLogger();
var likedEntitiesServer = new LikedEntitiesServer(port, host, logger);

await likedEntitiesServer.StartAsync();
