using IpManager.Serialization;
using Itmo.Dev.Platform.Postgres.Connection;
using Itmo.Dev.Platform.Postgres.Extensions;
using Npgsql;
using System;
using System.Collections.Generic;

namespace IPManager.Server.DataAccess;

public class OrganizationRepository(IPostgresConnectionProvider postgres) : IOrganizationRepository
{
    private readonly IPostgresConnectionProvider _postgres = postgres
        ?? throw new ArgumentNullException(nameof(postgres));

    public async IAsyncEnumerable<Organization> FindOrganizationsAsync(string pattern, int maxCount)
    {
        ArgumentNullException.ThrowIfNull(pattern, nameof(pattern));

        if (maxCount <= 0)
            yield break;

        if (!pattern.EndsWith('%'))
            pattern += '%';

        const string query = """
        SELECT name, start_ip
        FROM organizations
        WHERE name LIKE :pattern
        LIMIT :maxCount;
        """;

        NpgsqlConnection connection = await _postgres.GetConnectionAsync(default);

        using var command = new NpgsqlCommand(query, connection);
        command.AddParameter("pattern", pattern);
        command.AddParameter("maxCount", maxCount);

        using var reader = command.ExecuteReader();

        while (reader.Read())
        {
            yield return new Organization()
            {
                Name = reader.GetString(0),
                Ip = reader.GetString(1),
            };
        }
    }
}
