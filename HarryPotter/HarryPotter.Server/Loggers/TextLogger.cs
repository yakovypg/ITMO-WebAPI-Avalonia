using System;
using System.IO;

namespace HarryPotter.Server.Loggers;

public class TextLogger(TextWriter writer) : ILogger
{
    private readonly TextWriter _writer = writer
        ?? throw new ArgumentNullException(nameof(writer));

    public void Write(string? message)
    {
        _writer.Write(message);
    }

    public void WriteLine(string? message)
    {
        _writer.WriteLine(message);
    }
}
