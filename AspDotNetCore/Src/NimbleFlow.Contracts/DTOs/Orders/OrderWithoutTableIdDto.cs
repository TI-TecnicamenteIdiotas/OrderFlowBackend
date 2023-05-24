using NimbleFlow.Contracts.Enums;
using NimbleFlow.Data.Models;

namespace NimbleFlow.Contracts.DTOs.Orders;

public class OrderWithoutTableIdDto
{
    public Guid Id { get; set; }
    public OrderStatusEnum Status { get; set; }
    public decimal? Discount { get; set; }

    public static OrderWithoutTableIdDto FromModel(Order order) => new()
    {
        Id = order.Id,
        Status = (OrderStatusEnum)order.Status,
        Discount = order.Discount
    };
}