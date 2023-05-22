namespace NimbleFlow.Data.Models
{
    public partial class Order
    {
        public Order()
        {
            OrderProducts = new HashSet<OrderProduct>();
        }

        public Guid Id { get; set; }
        public Guid TableId { get; set; }
        /// <summary>
        /// 0 - Pending
        /// 1 - Preparing
        /// 2 - Ready
        /// 3 - Delivered
        /// </summary>
        public short Status { get; set; }
        public decimal? Discount { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        public virtual Table Table { get; set; } = null!;
        public virtual ICollection<OrderProduct> OrderProducts { get; set; }
    }
}
