namespace NimbleFlow.Api.Options.Base;

public abstract class BaseInitOptions<T>
{
    public const string OptionsName = "";
    private static T? _instance;
    public static void ConfigureInstance(T values) => _instance ??= values;
    public static T GetConfiguredInstance() => _instance is null ? throw new NullReferenceException() : _instance;
}