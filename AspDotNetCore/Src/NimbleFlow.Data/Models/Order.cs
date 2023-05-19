using NimbleFlow.Data.Base;

namespace NimbleFlow.Data.Models;

public class Order : ModelBase
{
    public Order()
    {
        OrderProducts = new HashSet<OrderProduct>();
    }

    public Guid TableId { get; set; }
    public short Status { get; set; }

    public virtual OrderStatus StatusNavigation { get; set; } = null!;
    public virtual Table Table { get; set; } = null!;
    public virtual ICollection<OrderProduct> OrderProducts { get; set; }
}