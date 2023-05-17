namespace NimbleFlow.Data.Models
{
    public partial class OrderProduct
    {
        public Guid OrderId { get; set; }
        public Guid ProductId { get; set; }
        public int ProductAmount { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        public virtual Order Order { get; set; } = null!;
        public virtual Product Product { get; set; } = null!;
    }
}
