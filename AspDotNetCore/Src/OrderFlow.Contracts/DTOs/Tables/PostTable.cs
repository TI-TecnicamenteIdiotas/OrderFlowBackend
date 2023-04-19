using OrderFlow.Data.Models;

namespace OrderFlow.Contracts.DTOs.Tables;

public class PostTable
{
    public string Name { get; set; } = null!;

    public Table ToModel()
        => new()
        {
            Name = Name
        };
}