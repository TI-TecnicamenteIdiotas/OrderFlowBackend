using OrderFlow.Business.Interfaces.Repositories;
using OrderFlow.Data.Repository;

namespace OrderFlow.Api.ServiceCollectionExtensions;

public static partial class ServiceCollectionExtensions
{
	public static void InjectRepositories(this IServiceCollection services)
	{
		services.AddScoped<IProductsRepository, ProductsRepository>();
		services.AddScoped<ICategoriesRepository, CategoriesRepository>();
		services.AddScoped<ITablesRepository, TablesRepository>();
		services.AddScoped<IItemsRepository, ItemsRepository>();
	}
}