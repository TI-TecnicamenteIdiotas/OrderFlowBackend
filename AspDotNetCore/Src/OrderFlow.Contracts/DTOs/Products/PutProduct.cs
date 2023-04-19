namespace OrderFlow.Contracts.DTOs.Products;

public class PutProduct
{
    public string? Title { get; set; }
    public string? Description { get; set; }
    public decimal? Price { get; set; }
    public string? ImageURL { get; set; }
    public bool? IsFavorite { get; set; }
    public uint? CategoryId { get; set; }
}