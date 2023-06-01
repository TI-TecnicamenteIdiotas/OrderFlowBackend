using NimbleFlow.Contracts.Enums;

namespace NimbleFlow.Api.Helpers;

public static class FilesHelper
{
    public static FileTypesEnum GetByteArrayFileType(
        this byte[] data,
        Dictionary<FileTypesEnum, byte[]> validFileHeaders
    )
    {
        foreach (var check in validFileHeaders)
        {
            if (data.Length < check.Value.Length)
                continue;

            var slice = data[..check.Value.Length];
            if (slice.SequenceEqual(check.Value))
                return check.Key;
        }

        return FileTypesEnum.Invalid;
    }
}