using NimbleFlow.Data.Base;

namespace NimbleFlow.Data.Models;

public class Product : ModelBase
{
    public Product()
    {
        OrderProducts = new HashSet<OrderProduct>();
    }

    public string Title { get; set; } = null!;
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public string? ImageUrl { get; set; }
    public bool IsFavorite { get; set; }
    public Guid CategoryId { get; set; }

    public virtual Category Category { get; set; } = null!;
    public virtual ICollection<OrderProduct> OrderProducts { get; set; }
}