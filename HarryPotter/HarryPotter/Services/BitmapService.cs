using Avalonia.Media.Imaging;
using Avalonia.Platform;
using HarryPotter.Infrastructure.Enums;
using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace HarryPotter.Services;

public static class BitmapService
{
    public static async Task<Bitmap?> LoadFromWebAsync(
        string? url,
        ActionAfterImageLoadError actionAfterError = ActionAfterImageLoadError.LoadDefaultImage)
    {
        if (string.IsNullOrEmpty(url))
            return PerformActionAfterLoadError(actionAfterError);

        using var httpClient = new HttpClient();

        try
        {
            HttpResponseMessage responseMessage = await httpClient.GetAsync(url);
            _ = responseMessage.EnsureSuccessStatusCode();

            byte[] contentBytes = await responseMessage.Content.ReadAsByteArrayAsync();
            var contentStream = new MemoryStream(contentBytes);

            return new Bitmap(contentStream);
        }
        catch
        {
            return PerformActionAfterLoadError(actionAfterError);
        }
    }

    public static Bitmap? LoadFromResources(Uri uri)
    {
        ArgumentNullException.ThrowIfNull(uri, nameof(uri));

        try
        {
            return new Bitmap(AssetLoader.Open(uri));
        }
        catch
        {
            return null;
        }
    }

    public static Bitmap? LoadDefaultImage()
    {
        var uri = new Uri("avares://HarryPotter/Assets/no-photo.jpg");
        return LoadFromResources(uri);
    }

    private static Bitmap? PerformActionAfterLoadError(
        ActionAfterImageLoadError action)
    {
        return action switch
        {
            ActionAfterImageLoadError.ReturnNull => null,
            ActionAfterImageLoadError.LoadDefaultImage => LoadDefaultImage(),

            _ => throw new ArgumentOutOfRangeException(nameof(action)),
        };
    }
}
