using IPManager.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IPManager.DataAccess;

public interface IIpAddressRepository
{
    IAsyncEnumerable<IpAddress> FindAllIpAddressesAsync();
    Task AddIpAddressAsync(IpAddress ip);
}
