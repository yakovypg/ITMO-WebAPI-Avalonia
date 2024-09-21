using IpManager.Serialization;
using Itmo.Dev.Platform.Postgres.Connection;
using Itmo.Dev.Platform.Postgres.Extensions;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IPManager.Server.DataAccess;

public class FavoriteIpRepository(IPostgresConnectionProvider postgres) : IIpRepository
{
    private readonly IPostgresConnectionProvider _postgres = postgres
        ?? throw new ArgumentNullException(nameof(postgres));

    public async Task AddIpAsync(Ip ip)
    {
        ArgumentNullException.ThrowIfNull(ip, nameof(ip));

        const string query = """
        INSERT INTO favorite_ips
        (favorite_ip_value, favorite_ip_details)
        VALUES (:value, :details);
        """;

        NpgsqlConnection connection = await _postgres.GetConnectionAsync(default);

        using var command = new NpgsqlCommand(query, connection);
        command.AddParameter("value", ip.Value);
        command.AddParameter("details", ip.Details);

        _ = command.ExecuteNonQuery();
    }

    public async IAsyncEnumerable<Ip> FindAllIpsAsync()
    {
        const string query = """
        SELECT favorite_ip_value, favorite_ip_details
        FROM favorite_ips
        ORDER BY favorite_ip_value;
        """;

        NpgsqlConnection connection = await _postgres.GetConnectionAsync(default);

        using var command = new NpgsqlCommand(query, connection);
        using var reader = command.ExecuteReader();

        while (reader.Read())
        {
            yield return new Ip()
            {
                Value = reader.GetString(0),
                Details = reader.GetString(1),
            };
        }
    }
}
