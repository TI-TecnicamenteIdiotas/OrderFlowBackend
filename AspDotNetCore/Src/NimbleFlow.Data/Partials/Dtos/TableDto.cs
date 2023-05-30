namespace NimbleFlow.Data.Partials.Dtos;

public sealed class TableDto
{
    public Guid Id { get; set; }
    public string Accountable { get; set; } = null!;
    public bool IsFullyPaid { get; set; }
}