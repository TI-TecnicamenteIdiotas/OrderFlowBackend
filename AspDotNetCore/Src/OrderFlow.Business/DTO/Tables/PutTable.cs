using OrderFlow.Data.Models;

namespace OrderFlow.Business.DTO.Tables;

public class PutTable
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public decimal PaidValue { get; set; }
    public virtual List<Item> Items { get; set; }
}