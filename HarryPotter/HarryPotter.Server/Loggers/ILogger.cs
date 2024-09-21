namespace HarryPotter.Server.Loggers;

public interface ILogger
{
    void Write(string? message);
    void WriteLine(string? message);
}
