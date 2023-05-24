using NimbleFlow.Contracts.DTOs.Products;
using NimbleFlow.Contracts.Enums;
using NimbleFlow.Data.Models;

namespace NimbleFlow.Contracts.DTOs.Orders;

public class OrderWithRelationsDto
{
    public Guid Id { get; set; }
    public OrderStatusEnum Status { get; set; }
    public decimal? Discount { get; set; }
    public ProductWithRelationsDto Product { get; set; } = new();

    public static OrderWithRelationsDto FromModel(OrderProduct orderProduct)
        => new()
        {
            Id = orderProduct.Order.Id,
            Status = (OrderStatusEnum)orderProduct.Order.Status,
            Discount = orderProduct.Order.Discount,
            Product = ProductWithRelationsDto.FromModel(orderProduct)
        };
}