using Newtonsoft.Json;
using StarWars.Infrastructure.Exceptions;
using StarWars.Infrastructure.Services;
using StarWars.Models.Serialization;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace StarWars.DataAccess
{
    public class SwapiEntityDataRepository<T> : IEntityDataRepository<T> where T : EntityBaseData
    {
        public async Task<T> FindEntityByUrlAsync(string url)
        {
            ArgumentNullException.ThrowIfNull(url, nameof(url));

            using var httpClient = new HttpClient();
            HttpResponseMessage response = await httpClient.GetAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                string reason = await ResponseReasonService.GetReasonAsync(response);
                throw new ErrorResponseException(response, reason);
            }

            string json = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<T>(json)
                ?? throw new UnknownContentFormatException();
        }

        public async Task<T> FindEntityByIdAsync(int id)
        {
            string baseUrl = EntityUrlService.GetUrl<T>();
            string urlWithId = UrlService.AddIdToUrl(baseUrl, id);

            using var httpClient = new HttpClient();
            HttpResponseMessage response = await httpClient.GetAsync(urlWithId);

            if (!response.IsSuccessStatusCode)
            {
                string reason = await ResponseReasonService.GetReasonAsync(response);
                throw new ErrorResponseException(response, reason);
            }

            string json = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<T>(json)
                ?? throw new UnknownContentFormatException();
        }

        public async Task<EntityPage<T>> FindEntitiesAsync(int page)
        {
            string baseUrl = EntityUrlService.GetUrl<T>();
            string urlWithPage = UrlService.AddPageToUrl(baseUrl, page);

            using var httpClient = new HttpClient();
            HttpResponseMessage response = await httpClient.GetAsync(urlWithPage);

            if (!response.IsSuccessStatusCode)
            {
                string reason = await ResponseReasonService.GetReasonAsync(response);
                throw new ErrorResponseException(response, reason);
            }

            string json = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<EntityPage<T>>(json)
                ?? throw new UnknownContentFormatException();
        }
    }
}
