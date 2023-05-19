using NimbleFlow.Data.Base;

namespace NimbleFlow.Data.Models;

public class Table : ModelBase
{
    public Table()
    {
        Orders = new HashSet<Order>();
    }

    public string Accountable { get; set; } = null!;
    public bool IsFullyPaid { get; set; }

    public virtual ICollection<Order> Orders { get; set; }
}