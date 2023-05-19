namespace NimbleFlow.Data.Models;

public partial class OrderStatus
{
    public OrderStatus()
    {
        Orders = new HashSet<Order>();
    }

    public short Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }

    public virtual ICollection<Order> Orders { get; set; }
}