using NimbleFlow.Data.Models;

namespace NimbleFlow.Contracts.DTOs.Orders;

public class CreateOrderProductDto
{
    public Guid ProductId { get; set; }
    public int ProductAmount { get; set; }

    public OrderProduct ToModel(Guid orderId) => new()
    {
        OrderId = orderId,
        ProductId = ProductId,
        ProductAmount = ProductAmount
    };
}