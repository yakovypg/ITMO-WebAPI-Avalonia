using IPManager.Infrastructure.Exceptions;
using IPManager.Infrastructure.Services;
using IPManager.Models;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace IPManager.DataAccess;

public class IpinfoIpDetailsRepository : IIpDetailsRepository
{
    public async Task<IpAddress> FindIpDetailsAsync(string ip)
    {
        ArgumentNullException.ThrowIfNull(ip, nameof(ip));

        string url = $"https://ipinfo.io/{ip}/json";

        using var httpClient = new HttpClient();
        HttpResponseMessage response = await httpClient.GetAsync(url);

        if (!response.IsSuccessStatusCode)
        {
            string reason = await ResponseReasonService.GetReasonAsync(response);
            throw new ResponseException(response, reason);
        }

        string json = await response.Content.ReadAsStringAsync();

        object? obj = JsonConvert.DeserializeObject(json);
        string details = JsonConvert.SerializeObject(obj, Formatting.Indented);

        return new IpAddress(ip, details);
    }
}
