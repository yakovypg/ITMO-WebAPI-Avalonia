using System;
using System.Net;

namespace HarryPotter.Server.Infrastructure.Extensions;

public static class ResponseExtensions
{
    public static HttpListenerResponse AddStatusCode(
        this HttpListenerResponse response,
        HttpStatusCode statusCode)
    {
        ArgumentNullException.ThrowIfNull(response, nameof(response));

        response.StatusCode = (int)statusCode;
        return response;
    }

    public static HttpListenerResponse AddContentLength(
        this HttpListenerResponse response,
        long contentLength)
    {
        ArgumentNullException.ThrowIfNull(response, nameof(response));

        response.ContentLength64 = contentLength;
        return response;
    }

    public static HttpListenerResponse AddDefaultHeaders(this HttpListenerResponse response)
    {
        ArgumentNullException.ThrowIfNull(response, nameof(response));

        response.Headers.Add("Access-Control-Allow-Origin", "*");
        response.Headers.Add("Access-Control-Allow-Methods", "POST, PUT, OPTIONS, DELETE, GET");

        return response;
    }
}
