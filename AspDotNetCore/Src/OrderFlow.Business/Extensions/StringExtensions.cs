namespace OrderFlow.Business.Extensions;

public static class StringExtensions
{
    public static bool IsNullOrEmpty(this string src)
    {
        return string.IsNullOrEmpty(src) || string.IsNullOrWhiteSpace(src);
    }
}