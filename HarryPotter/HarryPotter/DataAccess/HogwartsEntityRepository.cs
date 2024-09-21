using HarryPotter.Infrastructure.Exceptions;
using HarryPotter.Models;
using HarryPotter.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;

namespace HarryPotter.DataAccess;

public abstract class HogwartsEntityRepository<T> : IHogwartsEntityRepository<T>
    where T : HogwartsEntity
{
    private readonly string _url;

    protected HogwartsEntityRepository(string url)
    {
        ArgumentNullException.ThrowIfNull(url, nameof(url));
        _url = url;
    }

    public async IAsyncEnumerable<T> FindAllAsync()
    {
        using HttpClient httpClient = new();
        HttpResponseMessage response = await httpClient.GetAsync(_url);

        if (!response.IsSuccessStatusCode)
        {
            string reason = await ResponseContentService.GetJsonContentAsync(response);
            throw new UnsuccessfulRequestException(response.StatusCode, reason);
        }

        string jsonContent = await response.Content.ReadAsStringAsync();
        List<T>? entities = JsonConvert.DeserializeObject<List<T>>(jsonContent);

        foreach (T entity in entities ?? Enumerable.Empty<T>())
        {
            yield return entity;
        }
    }
}
