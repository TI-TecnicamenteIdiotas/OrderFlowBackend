using OrderFlow.Api.Repositories;
using OrderFlow.Contracts.Interfaces.Repositories;

namespace OrderFlow.Api.ServiceCollectionExtensions;

public static partial class ServiceCollectionExtensions
{
    public static void InjectRepositories(this IServiceCollection services)
    {
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<ITableRepository, TableRepository>();
        services.AddScoped<IItemRepository, ItemRepository>();
    }
}