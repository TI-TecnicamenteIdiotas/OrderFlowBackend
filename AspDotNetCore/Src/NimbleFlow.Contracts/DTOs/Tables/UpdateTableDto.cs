namespace NimbleFlow.Contracts.DTOs.Tables;

public sealed class UpdateTableDto
{
    public string? Accountable { get; set; } = null;
    public bool? IsFullyPaid { get; set; } = null;
}