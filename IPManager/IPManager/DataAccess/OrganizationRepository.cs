using IpManager.Serialization;
using IpManager.SharedSettings;
using IPManager.Infrastructure.Exceptions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;

namespace IPManager.DataAccess;

public class OrganizationRepository : IOrganizationRepository
{
    public async IAsyncEnumerable<Models.Organization> FindOrganizationsAsync(string pattern, int maxCount)
    {
        ArgumentNullException.ThrowIfNull(pattern, nameof(pattern));

        string url = $"{ServerRoutes.OrganizationsUrl}?pattern={pattern}&max={maxCount}";

        HttpClient httpClient = new();
        HttpResponseMessage response = await httpClient.GetAsync(url);

        string content = await response.Content.ReadAsStringAsync();

        OrganizationCollection collection = JsonConvert.DeserializeObject<OrganizationCollection>(content)
            ?? throw new IncorrectContentFormatException(response.Content);

        var organizations = collection.Organizations
            .Select(t => new Models.Organization(t.Name, t.Ip));

        foreach (var organization in organizations)
        {
            yield return organization;
        }
    }
}
