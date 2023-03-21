using Microsoft.EntityFrameworkCore;
using OrderFlow.Data.Context;

namespace OrderFlow.Api.ServiceCollectionExtensions;

public static partial class ServiceCollectionExtensions
{
	public static void InjectDatabases(this IServiceCollection services)
	{
		services.AddDbContext<OrderFlowContext>(optionsBuilder =>
			optionsBuilder.UseSqlServer(
				"Data Source=179.0.75.150\\MSSQLSERVER,5657;Initial Catalog=OrderFlowDB;User Id=Admin;Password=qwerty123456"));
	}
}