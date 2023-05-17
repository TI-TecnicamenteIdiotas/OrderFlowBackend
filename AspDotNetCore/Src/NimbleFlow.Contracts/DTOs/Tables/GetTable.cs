using NimbleFlow.Data.Models;

namespace NimbleFlow.Contracts.DTOs.Tables;

public sealed class GetTable
{
    public Guid Id { get; set; }
    public string Accountable { get; set; } = null!;
    public bool IsFullyPaid { get; set; }
    public ICollection<GetOrder> Orders { get; set; } = Array.Empty<GetOrder>();

    public static GetTable FromModel(Table table)
        => new()
        {
            Id = table.Id,
            Accountable = table.Accountable,
            IsFullyPaid = table.IsFullyPaid,
            Orders = table.Orders.Select(GetOrder.FromModel)
        };
}