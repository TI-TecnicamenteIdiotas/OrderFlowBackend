using NimbleFlow.Api.Repositories;

namespace NimbleFlow.Api.ServiceCollectionExtensions;

public static partial class ServiceCollectionExtensions
{
    public static void InjectRepositories(this IServiceCollection services)
    {
        services.AddScoped<CategoryRepository>();
        services.AddScoped<ProductRepository>();
        services.AddScoped<TableRepository>();
        services.AddScoped<OrderRepository>();
    }
}