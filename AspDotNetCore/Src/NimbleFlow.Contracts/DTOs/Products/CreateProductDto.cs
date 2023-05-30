using NimbleFlow.Contracts.Interfaces;
using NimbleFlow.Data.Models;

namespace NimbleFlow.Contracts.DTOs.Products;

public class CreateProductDto : IToModel<Product>
{
    public string Title { get; set; } = null!;
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public string? ImageUrl { get; set; }
    public bool IsFavorite { get; set; }
    public Guid CategoryId { get; set; }

    public Product ToModel()
        => new()
        {
            Title = Title,
            Description = Description,
            Price = Price,
            ImageUrl = ImageUrl,
            IsFavorite = IsFavorite,
            CategoryId = CategoryId
        };
}