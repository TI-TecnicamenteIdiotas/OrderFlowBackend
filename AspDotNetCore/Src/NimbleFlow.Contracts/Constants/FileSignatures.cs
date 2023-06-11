namespace NimbleFlow.Contracts.Constants;

public static class FileSignatures
{
    public static readonly byte[] Jpeg = { 0xFF, 0xD8 };
    public static readonly byte[] Png = { 0x89, 0x50, 0x4E, 0x47, 0x0D, 0x0A, 0x1A, 0x0A };
}