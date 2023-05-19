using NimbleFlow.Data.Base;

namespace NimbleFlow.Data.Models;

public class Category : ModelBase
{
    public Category()
    {
        Products = new HashSet<Product>();
    }

    public string Title { get; set; } = null!;
    public int? ColorTheme { get; set; }
    public int? CategoryIcon { get; set; }

    public virtual ICollection<Product> Products { get; set; }
}