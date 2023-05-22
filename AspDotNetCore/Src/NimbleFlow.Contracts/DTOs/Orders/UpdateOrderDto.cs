using NimbleFlow.Contracts.Enums;

namespace NimbleFlow.Contracts.DTOs.Orders;

public class UpdateOrderDto
{
    public Guid? TableId { get; set; }
    public OrderStatusEnum? Status { get; set; }
    public decimal? Discount { get; set; }
}