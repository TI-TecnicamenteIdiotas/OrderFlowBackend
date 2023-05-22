using NimbleFlow.Data.Models;

namespace NimbleFlow.Contracts.DTOs.Products;

public class CreateProductDto
{
    public string Title { get; set; } = null!;
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public string ImageUrl { get; set; } = null!;
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