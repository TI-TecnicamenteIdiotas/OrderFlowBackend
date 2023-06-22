using NimbleFlow.Api.Services;

namespace NimbleFlow.Api.ServiceCollectionExtensions;

public static partial class ServiceCollectionExtensions
{
    public static void InjectServices(this IServiceCollection services)
    {
        services.AddScoped<UploadService>();
        services.AddScoped<CategoryService>();
        services.AddScoped<ProductService>();
        services.AddScoped<TableService>();
        services.AddScoped<TableHubService>();
        services.AddScoped<CategoryHubService>();
        services.AddScoped<ProductHubService>();
    }
}