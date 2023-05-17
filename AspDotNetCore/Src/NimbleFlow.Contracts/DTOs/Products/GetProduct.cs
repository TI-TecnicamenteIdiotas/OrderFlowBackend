using NimbleFlow.Contracts.DTOs.Categories;
using NimbleFlow.Data.Models;

namespace NimbleFlow.Contracts.DTOs.Products;

public class GetProduct
{
    public Guid Id { get; set; }
    public string Title { get; set; } = null!;
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public string? ImageUrl { get; set; }
    public bool IsFavorite { get; set; }
    public GetCategory Category { get; set; } = new();
    public Guid CategoryId { get; set; }

    public static GetProduct FromModel(Product product)
        => new()
        {
            Id = product.Id,
            Title = product.Title,
            Description = product.Description,
            Price = product.Price,
            ImageUrl = product.ImageUrl,
            IsFavorite = product.IsFavorite,
            Category = GetCategory.FromModel(product.Category),
            CategoryId = product.CategoryId
        };
}