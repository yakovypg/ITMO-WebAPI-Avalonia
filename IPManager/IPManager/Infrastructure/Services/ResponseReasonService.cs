using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace IPManager.Infrastructure.Services;

public static class ResponseReasonService
{
    public async static Task<string> GetReasonAsync(HttpResponseMessage responseMessage)
    {
        ArgumentNullException.ThrowIfNull(responseMessage, nameof(responseMessage));

        string content = await responseMessage.Content.ReadAsStringAsync();
        object? obj = JsonConvert.DeserializeObject(content);

        return JsonConvert.SerializeObject(obj, Formatting.Indented);
    }
}
