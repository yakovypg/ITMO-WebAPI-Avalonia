using System;
using System.Net;
using System.Net.Http;

namespace StarWars.Infrastructure.Exceptions
{
    public class ErrorResponseException : Exception
    {
        public ErrorResponseException()
            : this(HttpStatusCode.NotFound)
        {
        }

        public ErrorResponseException(string? message, Exception? innerException)
            : this(HttpStatusCode.NotFound, null, message, innerException)
        {
        }

        public ErrorResponseException(
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

        public ErrorResponseException(
            HttpStatusCode statusCode,
            string? reason = null,
            string? message = null,
            Exception? innerException = null)
            : base(message ?? $"Status: {statusCode}\n{reason}", innerException)
        {
            StatusCode = statusCode;
            Reason = reason ?? string.Empty;
        }

        public HttpStatusCode StatusCode { get; }
        public string Reason { get; }
    }
}
