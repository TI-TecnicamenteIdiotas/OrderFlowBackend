using NimbleFlow.Data.Models;
using NimbleFlow.Data.Partials.Dtos;

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

    public static ProductWithRelationsDto FromModel(Product product, int productAmount)
        => new()
        {
            Id = product.Id,
            Amount = productAmount,
            Title = product.Title,
            Description = product.Description,
            Price = product.Price,
            ImageUrl = product.ImageUrl,
            IsFavorite = product.IsFavorite,
            Category = product.Category.ToDto()
        };
}