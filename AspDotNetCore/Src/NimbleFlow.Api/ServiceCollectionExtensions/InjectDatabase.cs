using Microsoft.EntityFrameworkCore;
using NimbleFlow.Api.Options;
using NimbleFlow.Data.Context;

namespace NimbleFlow.Api.ServiceCollectionExtensions;

public static partial class ServiceCollectionExtensions
{
    public static void InjectDatabases(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<NimbleFlowContext>(optionsBuilder =>
            optionsBuilder.UseNpgsql(
                configuration["SQL_CONNECTION_STRING"],
                builder => builder.UseQuerySplittingBehavior(QuerySplittingBehavior.SingleQuery)
            )
        );
    }
}