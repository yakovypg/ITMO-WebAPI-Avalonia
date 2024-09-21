using System;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace IpManager.Server.Services;

public static class ResponseService
{
    public static void ConfigureResponse(HttpListenerResponse response, HttpStatusCode status)
    {
        ArgumentNullException.ThrowIfNull(response, nameof(response));

        response.StatusCode = (int)status;
        response.Headers.Add("Access-Control-Allow-Origin", "*");
        response.Headers.Add("Access-Control-Allow-Methods", "POST, PUT, OPTIONS, DELETE, GET");
    }

    public static byte[] AddContentToResponse(HttpListenerResponse response, string content, Encoding encoding)
    {
        ArgumentNullException.ThrowIfNull(response, nameof(response));
        ArgumentNullException.ThrowIfNull(encoding, nameof(encoding));

        byte[] buffer = encoding.GetBytes(content);
        response.ContentLength64 = buffer.Length;

        return buffer;
    }

    public static async Task SendResponseAsync(HttpListenerResponse response, string content, HttpStatusCode status)
    {
        ArgumentNullException.ThrowIfNull(response, nameof(response));
        ArgumentNullException.ThrowIfNull(content, nameof(content));

        await SendResponseAsync(response, content, status, Encoding.UTF8);
    }

    public static async Task SendResponseAsync(HttpListenerResponse response, string content, HttpStatusCode status, Encoding encoding)
    {
        ArgumentNullException.ThrowIfNull(response, nameof(response));
        ArgumentNullException.ThrowIfNull(content, nameof(content));
        ArgumentNullException.ThrowIfNull(encoding, nameof(encoding));

        ConfigureResponse(response, status);
        byte[] data = AddContentToResponse(response, content, encoding);

        using Stream outputStream = response.OutputStream;
        await outputStream.WriteAsync(data);
        await outputStream.FlushAsync();
    }
}
