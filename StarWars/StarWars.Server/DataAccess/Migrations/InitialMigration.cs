using FluentMigrator;
using Itmo.Dev.Platform.Postgres.Migrations;

namespace StarWars.Server.DataAccess.Migrations
{
    [Migration(1, "Initial migration")]
    public class InitialMigration : SqlMigration
    {
        protected override string GetUpSql(IServiceProvider serviceProvider)
        {
            return """
            CREATE TABLE favorite_entities
            (
                favorite_entity_id BIGINT PRIMARY KEY GENERATED ALWAYS AS IDENTITY,
                favorite_entity_url TEXT NOT NULL UNIQUE
            );
            """;
        }

        protected override string GetDownSql(IServiceProvider serviceProvider)
        {
            return """
            DROP TABLE favorite_entities;
            """;
        }
    }
}
