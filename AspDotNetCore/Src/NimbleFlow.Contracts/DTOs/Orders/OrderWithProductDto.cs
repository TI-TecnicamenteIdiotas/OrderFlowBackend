using NimbleFlow.Contracts.DTOs.Products;
using NimbleFlow.Contracts.Enums;
using NimbleFlow.Data.Models;

namespace NimbleFlow.Contracts.DTOs.Orders;

public class OrderWithProductDto
{
    public Guid Id { get; set; }
    public OrderStatusEnum Status { get; set; }
    public decimal? Discount { get; set; }
    public ProductWithAmountDto Product { get; set; } = new();

    public static OrderWithProductDto FromModels(OrderProduct orderProduct)
        => new()
        {
            Id = orderProduct.Order.Id,
            Status = (OrderStatusEnum)orderProduct.Order.Status,
            Discount = orderProduct.Order.Discount,
            Product = ProductWithAmountDto.FromModel(orderProduct)
        };
}