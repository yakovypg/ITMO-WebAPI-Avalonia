using IpManager.Serialization;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IPManager.Server.DataAccess;

public interface IIpRepository
{
    Task AddIpAsync(Ip ip);
    IAsyncEnumerable<Ip> FindAllIpsAsync();
}
