using NimbleFlow.Contracts.Enums;
using NimbleFlow.Data.Models;

namespace NimbleFlow.Contracts.DTOs.Orders;

public class CreateOrderDto
{
    public Guid TableId { get; set; }
    public OrderStatusEnum Status { get; set; }
    public decimal? Discount { get; set; }

    public Order ToModel() => new()
    {
        TableId = TableId,
        Status = (short)Status,
        Discount = Discount
    };
}