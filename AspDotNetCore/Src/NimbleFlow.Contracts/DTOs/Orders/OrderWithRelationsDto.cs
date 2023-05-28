using NimbleFlow.Contracts.DTOs.Products;
using NimbleFlow.Contracts.Enums;
using NimbleFlow.Data.Models;

namespace NimbleFlow.Contracts.DTOs.Orders;

public class OrderWithRelationsDto
{
    public Guid Id { get; set; }
    public OrderStatusEnum Status { get; set; }
    public decimal? Discount { get; set; }
    public ICollection<ProductWithRelationsDto> Products { get; set; } = Array.Empty<ProductWithRelationsDto>();

    public static OrderWithRelationsDto FromModel(Order order)
        => new()
        {
            Id = order.Id,
            Status = (OrderStatusEnum)order.Status,
            Discount = order.Discount,
            Products = order.OrderProducts
                .Where(x => x.OrderId == order.Id)
                .Select(x => ProductWithRelationsDto.FromModel(x.Product, x.ProductAmount))
                .ToArray()
        };
}