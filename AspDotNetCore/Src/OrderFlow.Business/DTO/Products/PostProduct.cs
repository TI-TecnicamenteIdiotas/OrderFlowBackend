namespace OrderFlow.Business.DTO.Products;

public class PostProduct
{
    public string Title { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public string ImageURL { get; set; }
    public bool IsFavorite { get; set; }
    public int CategoryId { get; set; }
}