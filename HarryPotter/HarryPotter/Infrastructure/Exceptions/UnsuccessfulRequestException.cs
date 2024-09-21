using System;
using System.Net;

namespace HarryPotter.Infrastructure.Exceptions;

public class UnsuccessfulRequestException(
    HttpStatusCode statusCode,
    string? reason = null,
    string? message = null,
    Exception? innerException = null)
    : Exception(message ?? _defaultMessage, innerException)
{
    private const string _defaultMessage = "Request failed";

    public UnsuccessfulRequestException(
        string? message = null,
        Exception? innerException = null)
        : this(HttpStatusCode.BadRequest, null, message, innerException)
    {
    }

    public HttpStatusCode StatusCode { get; } = statusCode;
    public string? Reason { get; } = reason;
}
