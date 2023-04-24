using System.Reflection;
using Microsoft.OpenApi.Models;
using NimbleFlow.Api.Options;

namespace NimbleFlow.Api.ServiceCollectionExtensions;

public static partial class ServiceCollectionExtensions
{
    public static void InjectSwagger(this IServiceCollection services, out bool enableSwagger)
    {
        var swaggerOptions = SwaggerOptions.GetConfiguredInstance();

        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1",
                new OpenApiInfo
                {
                    Title = swaggerOptions.Title,
                    Version = swaggerOptions.Version,
                    Description = swaggerOptions.Description,
                    License = new OpenApiLicense
                    {
                        Name = swaggerOptions.LicenseName,
                        Url = new Uri(swaggerOptions.LicenseUrl)
                    }
                });

            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

            options.IncludeXmlComments(xmlPath);
        });

        enableSwagger = swaggerOptions.IsEnabled;
    }
}