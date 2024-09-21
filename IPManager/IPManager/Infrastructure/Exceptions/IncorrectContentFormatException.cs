using System;
using System.Net.Http;

namespace IPManager.Infrastructure.Exceptions;

public class IncorrectContentFormatException(
    HttpContent? content,
    string? message = null,
    Exception? innerException = null)
    : Exception(message ?? "Received content has incorrect format", innerException)
{
    public IncorrectContentFormatException()
            : this(string.Empty, null)
    {
    }

    public IncorrectContentFormatException(string? message, Exception? innerException)
        : this(null, message, innerException)
    {
    }

    public HttpContent? Content { get; } = content;
}
