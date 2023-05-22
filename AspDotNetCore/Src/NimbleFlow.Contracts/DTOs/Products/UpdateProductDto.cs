namespace NimbleFlow.Contracts.DTOs.Products;

public class UpdateProductDto
{
    public string? Title { get; set; }
    public string? Description { get; set; }
    public decimal? Price { get; set; }
    public string? ImageUrl { get; set; }
    public bool? IsFavorite { get; set; }
    public Guid? CategoryId { get; set; }
}