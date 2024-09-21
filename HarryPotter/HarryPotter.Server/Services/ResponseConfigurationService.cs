using HarryPotter.Server.Infrastructure.Extensions;
using System;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace HarryPotter.Server.Services;

public static class ResponseConfigurationService
{
    private static Encoding _contentEncoding = Encoding.UTF8;

    public static Encoding ContentEncoding
    {
        get => _contentEncoding;
        set => _contentEncoding = value ?? throw new ArgumentNullException(nameof(value));
    }

    public static async Task SendResponseAsync(
        HttpListenerResponse response,
        HttpStatusCode statusCode,
        string content)
    {
        ArgumentNullException.ThrowIfNull(response, nameof(response));
        ArgumentNullException.ThrowIfNull(content, nameof(content));

        byte[] contentBytes = ContentEncoding.GetBytes(content);

        _ = response.AddDefaultHeaders()
            .AddStatusCode(statusCode)
            .AddContentLength(contentBytes.Length);

        await response.OutputStream.WriteAsync(contentBytes);
        await response.OutputStream.FlushAsync();
    }
}
