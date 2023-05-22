namespace NimbleFlow.Api.Extensions;

public static partial class GeneralExtensions
{
    /// <summary>
    /// Checks if the comparison between two strings is not null
    /// or white space and uses equals with invariant culture and ignore case
    /// </summary>
    /// <param name="firstValue">string value</param>
    /// <param name="secondValue">string value</param>
    /// <returns><see cref="bool"/> comparison between values</returns>
    public static bool IsNotNullAndNotEquals(this string? firstValue, string? secondValue)
    {
        if (string.IsNullOrWhiteSpace(firstValue) || string.IsNullOrWhiteSpace(secondValue))
            return false;

        return !firstValue.Equals(secondValue, StringComparison.InvariantCultureIgnoreCase);
    }
}