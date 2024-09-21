using IpManager.Serialization;
using System.Collections.Generic;

namespace IPManager.Server.DataAccess;

public interface IOrganizationRepository
{
    IAsyncEnumerable<Organization> FindOrganizationsAsync(string pattern, int maxCount);
}
