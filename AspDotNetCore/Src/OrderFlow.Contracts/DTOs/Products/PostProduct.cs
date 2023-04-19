using OrderFlow.Data.Models;

namespace OrderFlow.Contracts.DTOs.Products;

public class PostProduct
{
    public string Title { get; set; } = null!;
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public string ImageURL { get; set; } = null!;
    public bool IsFavorite { get; set; }
    public Guid CategoryId { get; set; }

    public Product ToModel()
        => new()
        {
            Title = Title,
            Description = Description,
            Price = Price,
            ImageUrl = ImageURL,
            IsFavorite = IsFavorite,
            CategoryId = CategoryId
        };
}