using NimbleFlow.Api.Options;

namespace NimbleFlow.Api.ConfigurationExtensions;

public static partial class ConfigurationExtensions
{
    public static void ConfigurePostgresOptions(this IConfiguration configuration)
    {
        PostgresOptions.ConfigureInstance(new PostgresOptions
        {
            Server = configuration["SQL_DATABASE_SERVER"],
            Port = ushort.Parse(configuration["SQL_DATABASE_PORT"]),
            Database = configuration["SQL_DATABASE_DATABASE"],
            User = configuration["SQL_DATABASE_USER"],
            Password = configuration["SQL_DATABASE_PASSWORD"]
        });
    }
}