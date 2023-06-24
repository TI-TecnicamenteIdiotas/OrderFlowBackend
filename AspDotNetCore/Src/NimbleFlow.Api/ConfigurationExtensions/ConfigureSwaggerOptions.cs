using NimbleFlow.Api.Options;

namespace NimbleFlow.Api.ConfigurationExtensions;

public static partial class ConfigurationExtensions
{
    public static void ConfigureSwaggerOptions(this IConfiguration configuration)
    {
        var swaggerOptions = new SwaggerOptions();
        configuration.Bind(SwaggerOptions.OptionsName, swaggerOptions);
        SwaggerOptions.ConfigureInstance(swaggerOptions);
    }
}