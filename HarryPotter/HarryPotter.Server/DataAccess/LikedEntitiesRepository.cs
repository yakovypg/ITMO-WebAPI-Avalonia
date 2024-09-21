using HarryPotter.ServerConnection.Models;
using Itmo.Dev.Platform.Postgres.Connection;
using Npgsql;
using System;
using System.Collections.Generic;

namespace HarryPotter.Server.DataAccess;

public class LikedEntitiesRepository : ILikedEntitiesRepository
{
    private readonly IPostgresConnectionProvider _poostgresConnection;

    public LikedEntitiesRepository(IPostgresConnectionProvider poostgresConnection)
    {
        ArgumentNullException.ThrowIfNull(poostgresConnection, nameof(poostgresConnection));
        _poostgresConnection = poostgresConnection;
    }

    public async IAsyncEnumerable<LikedItem> FindAllAsync()
    {
        const string sql = """
        SELECT liked_entity_id, liked_entity_json
        FROM liked_entities
        ORDER BY liked_entity_id;
        """;

        NpgsqlConnection npgConnection = await _poostgresConnection.GetConnectionAsync(default);

        using NpgsqlCommand npgCommand = new(sql, npgConnection);
        using NpgsqlDataReader npgDataReader = npgCommand.ExecuteReader();

        while (npgDataReader.Read())
        {
            yield return new LikedItem()
            {
                Id = npgDataReader.GetString(0),
                ItemJson = npgDataReader.GetString(1),
            };
        }
    }
}
