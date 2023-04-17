using Microsoft.EntityFrameworkCore;
using OrderFlow.Data.Context;

namespace OrderFlow.Api.ServiceCollectionExtensions;

public static partial class ServiceCollectionExtensions
{
    public static void InjectDatabases(this IServiceCollection services)
    {
        services.AddDbContext<OrderFlowContext>(optionsBuilder => optionsBuilder.UseNpgsql(""));
    }
}