using NimbleFlow.Contracts.DTOs.Orders;
using NimbleFlow.Data.Models;

namespace NimbleFlow.Contracts.DTOs.Tables;

public class TableWithRelationsDto
{
    public Guid Id { get; set; }
    public string Accountable { get; set; } = null!;
    public bool IsFullyPaid { get; set; }
    public ICollection<OrderWithRelationsDto> Orders { get; set; } = Array.Empty<OrderWithRelationsDto>();

    public static TableWithRelationsDto FromModels(Table table)
        => new()
        {
            Id = table.Id,
            Accountable = table.Accountable,
            IsFullyPaid = table.IsFullyPaid,
            Orders = table.Orders.SelectMany(x => x.OrderProducts.Select(OrderWithRelationsDto.FromModel)).ToArray()
        };
}