using OrderFlow.Data.Models;

namespace OrderFlow.Contracts.DTOs.Tables;

public class PutTable
{
    public string? Name { get; set; }
    public decimal? PaidValue { get; set; }
    public virtual Item[]? Items { get; set; }
}