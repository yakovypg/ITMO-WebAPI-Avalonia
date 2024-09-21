using IpManager.Serialization;
using IpManager.SharedSettings;
using IPManager.Infrastructure.Exceptions;
using IPManager.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace IPManager.DataAccess;

public class FavoriteIpAddressRepository : IIpAddressRepository
{
    public async Task AddIpAddressAsync(IpAddress ip)
    {
        ArgumentNullException.ThrowIfNull(ip, nameof(ip));

        var contentObject = new Ip()
        {
            Value = ip.Ip,
            Details = ip.Details,
        };

        HttpContent content = new StringContent(JsonConvert.SerializeObject(contentObject));
        HttpClient httpClient = new();

        _ = await httpClient.PutAsync(ServerRoutes.FavoriteIpsUrl, content);
    }

    public async IAsyncEnumerable<IpAddress> FindAllIpAddressesAsync()
    {
        HttpClient httpClient = new();
        HttpResponseMessage response = await httpClient.GetAsync(ServerRoutes.FavoriteIpsUrl);

        string content = await response.Content.ReadAsStringAsync();

        IpCollection ips = JsonConvert.DeserializeObject<IpCollection>(content)
            ?? throw new IncorrectContentFormatException(response.Content);

        var ipAddresses = ips.Ips.Select(t => new IpAddress(t.Value, t.Details));

        foreach (var ipAddress in ipAddresses)
        {
            yield return ipAddress;
        }
    }
}
