using NimbleFlow.Api.Options.Base;

namespace NimbleFlow.Api.Options;

public class SwaggerOptions : BaseInitOptions<SwaggerOptions>
{
    public new const string OptionsName = "SwaggerOptions";
    public bool IsEnabled { get; init; } = false;
    public string Title { get; init; } = null!;
    public string Version { get; init; } = null!;
    public string Description { get; init; } = null!;
    public string LicenseName { get; init; } = null!;
    public string LicenseUrl { get; init; } = null!;
}