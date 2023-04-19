using OrderFlow.Data.Models;

namespace OrderFlow.Contracts.DTOs.Tables;

public class GetTable
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public decimal PaidValue { get; set; }
    public virtual List<Item> Items { get; set; } = new();

    public static GetTable FromModel(Table table)
        => new()
        {
            Id = table.Id,
            Name = table.Name,
            PaidValue = table.PaidValue,
            Items = table.Items.ToList()
        };
}