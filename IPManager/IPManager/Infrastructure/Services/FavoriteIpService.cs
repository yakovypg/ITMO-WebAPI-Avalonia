using IPManager.DataAccess;
using IPManager.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IPManager.Infrastructure.Services;

public static class FavoriteIpService
{
    private static readonly IIpAddressRepository _repository;

    static FavoriteIpService()
    {
        _repository = new FavoriteIpAddressRepository();
    }

    public static IAsyncEnumerable<IpAddress> GetFavoriteIpAddresses()
    {
        return _repository.FindAllIpAddressesAsync();
    }

    public static async Task AddFavoriteIpAddressAsync(IpAddress ip)
    {
        ArgumentNullException.ThrowIfNull(ip, nameof(ip));
        await _repository.AddIpAddressAsync(ip);
    }
}
