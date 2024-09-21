using FluentMigrator;
using Itmo.Dev.Platform.Postgres.Migrations;
using System;

namespace HarryPotter.Server.DataAccess.Migrations;

[Migration(1, "Primary migration")]
public class PrimaryMigration : SqlMigration
{
    protected override string GetUpSql(IServiceProvider serviceProvider)
    {
        return """
        CREATE TABLE liked_entities
        (
            liked_entity_id TEXT PRIMARY KEY,
            liked_entity_json TEXT
        );
        """;
    }

    protected override string GetDownSql(IServiceProvider serviceProvider)
    {
        return """
        DROP TABLE liked_entities;
        """;
    }
}
