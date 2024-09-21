using IPManager.DataAccess;
using IPManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace IPManager.Infrastructure.Services;

public static class OrganizationService
{
    private static readonly IOrganizationRepository _repository;

    static OrganizationService()
    {
        _repository = new OrganizationRepository();
    }

    public static IAsyncEnumerable<Organization> GetOrganizationsAsync(string pattern, int maxCount)
    {
        ArgumentNullException.ThrowIfNull(pattern, nameof(pattern));

        return maxCount > 0
            ? _repository.FindOrganizationsAsync(pattern, maxCount)
            : AsyncEnumerable.Empty<Organization>();
    }
}
