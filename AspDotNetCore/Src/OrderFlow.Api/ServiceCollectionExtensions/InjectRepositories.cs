using OrderFlow.Data.Repositories;

namespace OrderFlow.Api.ServiceCollectionExtensions;

public static partial class ServiceCollectionExtensions
{
    public static void InjectRepositories(this IServiceCollection services)
    {
        services.AddScoped<ProductsRepository>();
        services.AddScoped<CategoriesRepository>();
        services.AddScoped<TablesRepository>();
        services.AddScoped<ItemsRepository>();
    }
}