namespace NimbleFlow.Data.Models;

public partial class Item
{
    public Guid Id { get; set; }
    public Guid ProductId { get; set; }
    public Guid TableId { get; set; }
    public int Count { get; set; }
    public decimal Discount { get; set; }
    public decimal Additional { get; set; }
    public int Status { get; set; }
    public bool Paid { get; set; }
    public string? Note { get; set; } = null;
    public virtual Product Product { get; set; } = null!;
    public virtual Table Table { get; set; } = null!;
}