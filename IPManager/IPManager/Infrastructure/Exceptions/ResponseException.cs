using System;
using System.Net;
using System.Net.Http;

namespace IPManager.Infrastructure.Exceptions;

public class ResponseException(
    HttpStatusCode statusCode,
    string? reason = null,
    string? message = null,
    Exception? innerException = null)
    : Exception(message ?? $"Status: {statusCode}\n{reason}", innerException)
{
    public ResponseException()
        : this(HttpStatusCode.NotFound)
    {
    }

    public ResponseException(string? message, Exception? innerException)
        : this(HttpStatusCode.NotFound, null, message, innerException)
    {
    }

    public ResponseException(
        HttpResponseMessage response,
        string? reason = null,
        string? message = null,
        Exception? innerException = null)
        : this(response?.StatusCode ?? throw new ArgumentNullException(nameof(response)),
               reason ?? string.Empty,
               message,
               innerException)
    {
    }

    public string Reason { get; } = reason ?? string.Empty;
    public HttpStatusCode StatusCode { get; } = statusCode;
}
