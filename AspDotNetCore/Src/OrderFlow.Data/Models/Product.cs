namespace OrderFlow.Data.Models;

public partial class Product
{
    public Product()
    {
        Items = new HashSet<Item>();
    }

    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;
    public decimal Price { get; set; }
    public string? ImageUrl { get; set; } = null;
    public bool IsFavorite { get; set; }
    public int CategoryId { get; set; }
    public virtual Category Category { get; set; } = null!;
    public virtual ICollection<Item> Items { get; set; }
}