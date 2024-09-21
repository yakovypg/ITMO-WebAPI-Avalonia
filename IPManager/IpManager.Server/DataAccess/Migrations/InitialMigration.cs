using FluentMigrator;
using Itmo.Dev.Platform.Postgres.Migrations;
using System;

namespace IpManager.Server.DataAccess.Migrations
{
    [Migration(1, "Initial migration")]
    public class InitialMigration : SqlMigration
    {
        protected override string GetUpSql(IServiceProvider serviceProvider)
        {
            return """
            CREATE TABLE favorite_ips
            (
                favorite_ip_id BIGINT PRIMARY KEY GENERATED ALWAYS AS IDENTITY,
                favorite_ip_value TEXT NOT NULL UNIQUE,
                favorite_ip_details TEXT
            );
            CREATE TABLE organizations
            (
                start_ip TEXT,
                end_ip TEXT,
                join_key TEXT,
                name TEXT,
                domain TEXT,
                type TEXT,
                asn TEXT,
                as_name TEXT,
                as_domain TEXT,
                as_type TEXT,
                country TEXT
            );

            CREATE EXTENSION pg_trgm;
            CREATE EXTENSION btree_gin;
            CREATE INDEX org_name_index ON organizations USING GIN (name);
            """;
        }

        protected override string GetDownSql(IServiceProvider serviceProvider)
        {
            return """
            DROP TABLE favorite_ips;
            DROP TABLE organizations;
            DROP INDEX org_name_index;
            DROP EXTENSION pg_trgm;
            DROP EXTENSION btree_gin;
            """;
        }
    }
}
