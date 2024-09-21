using System;

namespace StarWars.Infrastructure.Exceptions
{
    public class UnknownContentFormatException : Exception
    {
        public UnknownContentFormatException()
            : this(string.Empty, null)
        {
        }

        public UnknownContentFormatException(string? message, Exception? innerException)
            : this(string.Empty, message, innerException)
        {
        }

        public UnknownContentFormatException(string? content, string? message = null, Exception? innerException = null)
            : base(message ?? "Received content has unknown format", innerException)
        {
            Content = content ?? string.Empty;
        }

        public string Content { get; }
    }
}
