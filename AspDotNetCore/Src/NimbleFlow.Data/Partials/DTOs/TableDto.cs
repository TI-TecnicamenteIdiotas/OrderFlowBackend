namespace NimbleFlow.Data.Partials.DTOs;

public class TableDto
{
    public Guid Id { get; init; }
    public string Accountable { get; init; }
    public bool IsFullyPaid { get; init; }

    public TableDto(Guid id, string accountable)
    {
        Id = id;
        Accountable = accountable;
    }
}