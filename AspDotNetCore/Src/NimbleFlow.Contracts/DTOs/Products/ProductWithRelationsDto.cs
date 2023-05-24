using NimbleFlow.Contracts.DTOs.Categories;
using NimbleFlow.Data.Models;

namespace NimbleFlow.Contracts.DTOs.Products;

public class ProductWithRelationsDto
{
    public Guid Id { get; set; }
    public int Amount { get; set; }
    public string Title { get; set; } = null!;
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public string? ImageUrl { get; set; }
    public bool IsFavorite { get; set; }
    public CategoryDto Category { get; set; } = new();

    public static ProductWithRelationsDto FromModel(OrderProduct orderProduct)
        => new()
        {
            Id = orderProduct.Product.Id,
            Amount = orderProduct.ProductAmount,
            Title = orderProduct.Product.Title,
            Description = orderProduct.Product.Description,
            Price = orderProduct.Product.Price,
            ImageUrl = orderProduct.Product.ImageUrl,
            IsFavorite = orderProduct.Product.IsFavorite,
            Category = CategoryDto.FromModel(orderProduct.Product.Category)
        };
}