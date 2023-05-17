namespace NimbleFlow.Data.Models
{
    public partial class Table
    {
        public Table()
        {
            Orders = new HashSet<Order>();
        }

        public Guid Id { get; set; }
        public string Accountable { get; set; } = null!;
        public bool IsFullyPaid { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
