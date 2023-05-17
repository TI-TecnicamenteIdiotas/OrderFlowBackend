namespace NimbleFlow.Data.Models
{
    public partial class Category
    {
        public Category()
        {
            Products = new HashSet<Product>();
        }

        public Guid Id { get; set; }
        public string Title { get; set; } = null!;
        public int? ColorTheme { get; set; }
        public int? CategoryIcon { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
