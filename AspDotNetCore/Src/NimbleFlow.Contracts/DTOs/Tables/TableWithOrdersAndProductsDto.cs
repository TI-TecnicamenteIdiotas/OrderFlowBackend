using NimbleFlow.Contracts.DTOs.Orders;
using NimbleFlow.Data.Models;

namespace NimbleFlow.Contracts.DTOs.Tables;

public class TableWithOrdersAndProductsDto
{
    public Guid Id { get; set; }
    public string Accountable { get; set; } = null!;
    public bool IsFullyPaid { get; set; }
    public ICollection<OrderWithProductDto> Orders { get; set; } = Array.Empty<OrderWithProductDto>();

    public static TableWithOrdersAndProductsDto FromModels(Table table, IEnumerable<OrderProduct> orders)
        => new()
        {
            Id = table.Id,
            Accountable = table.Accountable,
            IsFullyPaid = table.IsFullyPaid,
            Orders = orders.Select(OrderWithProductDto.FromModels).ToArray()
        };
}