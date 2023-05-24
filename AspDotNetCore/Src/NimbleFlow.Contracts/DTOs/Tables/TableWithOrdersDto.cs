using NimbleFlow.Contracts.DTOs.Orders;
using NimbleFlow.Data.Models;

namespace NimbleFlow.Contracts.DTOs.Tables;

public class TableWithOrdersDto
{
    public Guid Id { get; set; }
    public string Accountable { get; set; } = null!;
    public bool IsFullyPaid { get; set; }
    public ICollection<OrderWithoutTableIdDto> Orders { get; set; } = Array.Empty<OrderWithoutTableIdDto>();

    public static TableWithOrdersDto FromModels(Table table, IEnumerable<Order> orders)
        => new()
        {
            Id = table.Id,
            Accountable = table.Accountable,
            IsFullyPaid = table.IsFullyPaid,
            Orders = orders.Select(OrderWithoutTableIdDto.FromModel).ToArray()
        };
}