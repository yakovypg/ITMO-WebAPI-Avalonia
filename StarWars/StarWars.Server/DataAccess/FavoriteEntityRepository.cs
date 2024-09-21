using Itmo.Dev.Platform.Postgres.Connection;
using Itmo.Dev.Platform.Postgres.Extensions;
using Npgsql;

namespace StarWars.Server.DataAccess
{
    public class FavoriteEntityRepository(IPostgresConnectionProvider connectionProvider) : IEntityRepository
    {
        private readonly IPostgresConnectionProvider _connectionProvider = connectionProvider
            ?? throw new ArgumentNullException(nameof(connectionProvider));

        public async IAsyncEnumerable<string> FindAllEntitiesAsync()
        {
            const string query = """
            SELECT favorite_entity_url
            FROM favorite_entities
            ORDER BY favorite_entity_url;
            """;

            NpgsqlConnection connection = await _connectionProvider.GetConnectionAsync(default);

            using NpgsqlCommand command = new(query, connection);
            using NpgsqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                yield return reader.GetString(0);
            }
        }

        public async Task AddAsync(string entityUrl)
        {
            ArgumentNullException.ThrowIfNull(entityUrl, nameof(entityUrl));

            const string query = """
            INSERT INTO favorite_entities
            (favorite_entity_url)
            VALUES (:url);
            """;

            NpgsqlConnection connection = await _connectionProvider.GetConnectionAsync(default);

            using var command = new NpgsqlCommand(query, connection);
            command.AddParameter("url", entityUrl);

            _ = command.ExecuteNonQuery();
        }

        public async Task RemoveAsync(string entityUrl)
        {
            ArgumentNullException.ThrowIfNull(entityUrl, nameof(entityUrl));

            const string query = """
            DELETE FROM favorite_entities
            WHERE favorite_entity_url = :url;
            """;

            NpgsqlConnection connection = await _connectionProvider.GetConnectionAsync(default);

            using var command = new NpgsqlCommand(query, connection);
            command.AddParameter("url", entityUrl);

            _ = command.ExecuteNonQuery();
        }
    }
}
