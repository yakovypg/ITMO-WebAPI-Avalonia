using HarryPotter.Infrastructure.Exceptions;
using HarryPotter.Models;
using HarryPotter.ServerConnection.Models;
using HarryPotter.ServerConnection.Services;
using HarryPotter.Services;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace HarryPotter.DataAccess;

public class LikedCharactersHandler : IHogwartsEntityHandler<Character>
{
    public async Task AddAsync(Character entity)
    {
        ArgumentNullException.ThrowIfNull(entity, nameof(entity));

        string entityJson = JsonConvert.SerializeObject(entity);

        var likedItem = new LikedItem()
        {
            Id = entity.Id ?? string.Empty,
            ItemJson = entityJson,
        };

        string json = JsonConvert.SerializeObject(likedItem);
        string url = ServerConnectionService.LikedCharactersUrl;

        using var httpClient = new HttpClient();
        var content = new StringContent(json);

        HttpResponseMessage response = await httpClient.PutAsync(url, content);

        if (!response.IsSuccessStatusCode)
        {
            string reason = await ResponseContentService.GetJsonContentAsync(response);
            throw new UnsuccessfulRequestException(response.StatusCode, reason);
        }
    }

    public async Task DeleteAsync(Character entity)
    {
        ArgumentNullException.ThrowIfNull(entity, nameof(entity));

        string url = $"{ServerConnectionService.LikedCharactersUrl}{entity.Id}";

        using HttpClient httpClient = new();
        HttpResponseMessage response = await httpClient.DeleteAsync(url);

        if (!response.IsSuccessStatusCode)
        {
            string reason = await ResponseContentService.GetJsonContentAsync(response);
            throw new UnsuccessfulRequestException(response.StatusCode, reason);
        }
    }
}
