using NimbleFlow.Data.Models;

namespace NimbleFlow.Contracts.DTOs.Tables;

public class PostTable
{
    public string Name { get; set; } = null!;

    public Table ToModel()
        => new()
        {
            Name = Name
        };
}