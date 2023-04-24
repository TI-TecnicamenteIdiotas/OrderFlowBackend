namespace NimbleFlow.Data.Models;

public partial class Table
{
    public Table()
    {
        Items = new HashSet<Item>();
    }

    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public decimal PaidValue { get; set; }
    public virtual ICollection<Item> Items { get; set; }
}