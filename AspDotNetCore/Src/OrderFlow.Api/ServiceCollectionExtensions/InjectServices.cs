using OrderFlow.Api.Services;
using OrderFlow.Contracts.Interfaces.Services;

namespace OrderFlow.Api.ServiceCollectionExtensions;

public static partial class ServiceCollectionExtensions
{
    public static void InjectServices(this IServiceCollection services)
    {
        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<ICategoryService, CategoryService>();
        services.AddScoped<ITableService, TableService>();
        services.AddScoped<IItemService, ItemService>();
    }
}