using OrderFlow.Business.Interfaces.Services;
using OrderFlow.Business.Services;

namespace OrderFlow.Api.ServiceCollectionExtensions;

public static partial class ServiceCollectionExtensions
{
    public static void InjectServices(this IServiceCollection services)
    {
        services.AddScoped<IProductsService, ProductsService>();
        services.AddScoped<ICategoriesService, CategoriesService>();
        services.AddScoped<ITablesService, TablesService>();
        services.AddScoped<IItemsService, ItemsService>();
    }
}