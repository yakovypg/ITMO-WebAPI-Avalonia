using IPManager.Models;
using System.Threading.Tasks;

namespace IPManager.DataAccess;

public interface IIpDetailsRepository
{
    Task<IpAddress> FindIpDetailsAsync(string ip);
}
