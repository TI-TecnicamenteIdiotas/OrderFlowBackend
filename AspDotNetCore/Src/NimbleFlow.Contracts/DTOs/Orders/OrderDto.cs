using NimbleFlow.Contracts.Enums;
using NimbleFlow.Data.Models;

namespace NimbleFlow.Contracts.DTOs.Orders;

public class OrderDto
{
    public Guid Id { get; set; }
    public Guid TableId { get; set; }
    public OrderStatusEnum Status { get; set; }
    public decimal? Discount { get; set; }

    public static OrderDto FromModel(Order order) => new()
    {
        Id = order.Id,
        TableId = order.TableId,
        Status = (OrderStatusEnum)order.Status,
        Discount = order.Discount
    };
}