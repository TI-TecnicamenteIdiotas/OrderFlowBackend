using NimbleFlow.Contracts.Interfaces;
using NimbleFlow.Data.Models;

namespace NimbleFlow.Contracts.DTOs.Products;

public class CreateProductDto : IToModel<Product>
{
    public string Title { get; init; }
    public string? Description { get; init; }
    public decimal Price { get; init; }
    public string? ImageUrl { get; init; }
    public bool IsFavorite { get; init; }
    public Guid CategoryId { get; init; }

    public CreateProductDto(string title, Guid categoryId)
    {
        Title = title;
        CategoryId = categoryId;
    }

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