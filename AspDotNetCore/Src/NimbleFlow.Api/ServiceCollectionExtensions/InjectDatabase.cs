using Microsoft.EntityFrameworkCore;
using NimbleFlow.Api.Options;
using NimbleFlow.Data.Context;

namespace NimbleFlow.Api.ServiceCollectionExtensions;

public static partial class ServiceCollectionExtensions
{
    public static void InjectDatabases(this IServiceCollection services)
    {
        var postgresOptions = PostgresOptions.GetConfiguredInstance();
        var postgresConnectionString = $"Server={postgresOptions.Server};" +
                                       $"Port={postgresOptions.Port};" +
                                       $"Database={postgresOptions.Database};" +
                                       $"User Id={postgresOptions.User};" +
                                       $"Password={postgresOptions.Password}";

        services.AddDbContext<NimbleFlowContext>(optionsBuilder => optionsBuilder.UseNpgsql(postgresConnectionString));
    }
}