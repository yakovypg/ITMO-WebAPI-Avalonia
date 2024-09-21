using IPManager.DataAccess;
using IPManager.Models;
using System;
using System.Threading.Tasks;

namespace IPManager.Infrastructure.Services;

public static class IpDetailsService
{
    private static readonly IIpDetailsRepository _repository;

    static IpDetailsService()
    {
        _repository = new IpinfoIpDetailsRepository();
    }

    public static async Task<IpAddress> GetIpDetailsAsync(string ip)
    {
        ArgumentNullException.ThrowIfNull(ip, nameof(ip));
        return await _repository.FindIpDetailsAsync(ip);
    }
}
