using NimbleFlow.Contracts.DTOs.Categories;
using NimbleFlow.Data.Models;

namespace NimbleFlow.Contracts.DTOs.Products;

public class ProductDto
{
    public Guid Id { get; set; }
    public string Title { get; set; } = null!;
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public string? ImageUrl { get; set; }
    public bool IsFavorite { get; set; }
    public CategoryDto Category { get; set; } = new();
    public Guid CategoryId { get; set; }

    public static ProductDto FromModel(Product product)
        => new()
        {
            Id = product.Id,
            Title = product.Title,
            Description = product.Description,
            Price = product.Price,
            ImageUrl = product.ImageUrl,
            IsFavorite = product.IsFavorite,
            Category = CategoryDto.FromModel(product.Category),
            CategoryId = product.CategoryId
        };
}