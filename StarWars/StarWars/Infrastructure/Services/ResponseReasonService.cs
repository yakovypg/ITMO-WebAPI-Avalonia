using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace StarWars.Infrastructure.Services
{
    public static class ResponseReasonService
    {
        public async static Task<string> GetReasonAsync(HttpResponseMessage response)
        {
            ArgumentNullException.ThrowIfNull(response, nameof(response));

            string contentString = await response.Content.ReadAsStringAsync();
            object? jsonObject = JsonConvert.DeserializeObject(contentString);

            return JsonConvert.SerializeObject(jsonObject, Formatting.Indented);
        }
    }
}
