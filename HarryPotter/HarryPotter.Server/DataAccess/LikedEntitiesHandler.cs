using HarryPotter.ServerConnection.Models;
using Itmo.Dev.Platform.Postgres.Connection;
using Itmo.Dev.Platform.Postgres.Extensions;
using Npgsql;
using System;
using System.Threading.Tasks;

namespace HarryPotter.Server.DataAccess;

public class LikedEntitiesHandler : ILikedEntitiesHandler
{
    private readonly IPostgresConnectionProvider _poostgresConnection;

    public LikedEntitiesHandler(IPostgresConnectionProvider poostgresConnection)
    {
        ArgumentNullException.ThrowIfNull(poostgresConnection, nameof(poostgresConnection));
        _poostgresConnection = poostgresConnection;
    }

    public async Task AddAsync(LikedItem entity)
    {
        ArgumentNullException.ThrowIfNull(entity, nameof(entity));

        const string sql = """
        INSERT INTO liked_entities
        (liked_entity_id, liked_entity_json)
        VALUES
        (:id, :json);
        """;

        NpgsqlConnection npgConnection = await _poostgresConnection.GetConnectionAsync(default);

        using var ngpCommand = new NpgsqlCommand(sql, npgConnection)
            .AddParameter("id", entity.Id)
            .AddParameter("json", entity.ItemJson);

        _ = ngpCommand.ExecuteNonQuery();
    }

    public async Task DeleteAsync(string entityId)
    {
        ArgumentNullException.ThrowIfNull(entityId, nameof(entityId));
        
        const string sql = """
        DELETE FROM liked_entities
        WHERE liked_entity_id = :id;
        """;

        NpgsqlConnection npgConnection = await _poostgresConnection.GetConnectionAsync(default);

        using var ngpCommand = new NpgsqlCommand(sql, npgConnection)
            .AddParameter("id", entityId);

        _ = ngpCommand.ExecuteNonQuery();
    }
}
