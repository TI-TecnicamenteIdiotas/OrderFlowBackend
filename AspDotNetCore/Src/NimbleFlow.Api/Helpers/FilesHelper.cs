namespace NimbleFlow.Api.Helpers;

public static class FilesHelper
{
    public static bool IsFileTypeValid(this byte[] data, Dictionary<string, byte[]> validFileHeaders)
    {
        foreach (var check in validFileHeaders)
        {
            if (data.Length < check.Value.Length)
                continue;

            var slice = data[..check.Value.Length];
            if (slice.SequenceEqual(check.Value))
                return true;
        }

        return false;
    }
}