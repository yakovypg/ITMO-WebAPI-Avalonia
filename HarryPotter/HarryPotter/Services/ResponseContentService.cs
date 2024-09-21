using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace HarryPotter.Services;

public static class ResponseContentService
{
    public static async Task<string> GetJsonContentAsync(
        HttpResponseMessage response,
        Formatting formatting = Formatting.Indented)
    {
        ArgumentNullException.ThrowIfNull(response, nameof(response));

        string jsonContent = await response.Content.ReadAsStringAsync();

        switch (formatting)
        {
            case Formatting.Indented:
                object? deserializedContent = JsonConvert.DeserializeObject(jsonContent);
                return JsonConvert.SerializeObject(deserializedContent, Formatting.Indented);

            case Formatting.None:
                return jsonContent;

            default:
                throw new ArgumentOutOfRangeException(nameof(formatting));
        }
    }
}
