using System;

namespace HarryPotter.Server.Loggers;

public class ConsoleLogger() : TextLogger(Console.Out)
{
}
