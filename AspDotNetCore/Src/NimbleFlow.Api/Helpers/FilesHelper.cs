using NimbleFlow.Contracts.Enums;

namespace NimbleFlow.Api.Helpers;

public static class FilesHelper
{
    public static FileTypeEnum GetFileTypeBySignature(
        this byte[] data,
        Dictionary<FileTypeEnum, byte[]> fileSignatures
    )
    {
        foreach (var check in fileSignatures)
        {
            if (data.Length < check.Value.Length)
                continue;

            var slice = data[..check.Value.Length];
            if (slice.SequenceEqual(check.Value))
                return check.Key;
        }

        return FileTypeEnum.Unknown;
    }
}