namespace NimbleFlow.Api.Options;

public class PostgresOptions : BaseInitOptions<PostgresOptions>
{
    public new const string OptionsName = "PostgresOptions";

    public string Server { get; set; } = null!;
    public ushort Port { get; set; } = 5432;
    public string Database { get; set; } = null!;
    public string User { get; set; } = null!;
    public string Password { get; set; } = null!;
}