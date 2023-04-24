using Microsoft.EntityFrameworkCore;
using NimbleFlow.Data.Context;

namespace NimbleFlow.Api.ServiceCollectionExtensions;

public static partial class ServiceCollectionExtensions
{
    public static void InjectDatabases(this IServiceCollection services)
    {
        services.AddDbContext<OrderFlowContext>(optionsBuilder => optionsBuilder.UseNpgsql(""));
    }
}