using IPManager.Models;
using System.Collections.Generic;

namespace IPManager.DataAccess;

public interface IOrganizationRepository
{
    IAsyncEnumerable<Organization> FindOrganizationsAsync(string pattern, int maxCount);
}
