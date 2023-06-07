namespace NimbleFlow.Data.Partials.DTOs;

public class ProductDto
{
    public Guid Id { get; init; }
    public string Title { get; init; }
    public string? Description { get; init; }
    public decimal Price { get; init; }
    public string? ImageUrl { get; init; }
    public bool IsFavorite { get; init; }
    public Guid CategoryId { get; init; }

    public ProductDto(Guid id, string title, Guid categoryId)
    {
        Id = id;
        Title = title;
        CategoryId = categoryId;
    }
}