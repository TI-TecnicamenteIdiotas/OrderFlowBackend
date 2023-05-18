namespace NimbleFlow.Contracts.DTOs.Tables;

public sealed class PutTable
{
    public string? Accountable { get; set; } = null;
    public bool? IsFullyPaid { get; set; } = null;
}