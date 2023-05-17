using NimbleFlow.Api.Services;
using NimbleFlow.Contracts.Interfaces.Services;

namespace NimbleFlow.Api.ServiceCollectionExtensions;

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