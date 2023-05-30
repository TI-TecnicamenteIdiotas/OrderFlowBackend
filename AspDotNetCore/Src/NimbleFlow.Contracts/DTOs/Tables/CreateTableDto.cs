using NimbleFlow.Contracts.Interfaces;
using NimbleFlow.Data.Models;

namespace NimbleFlow.Contracts.DTOs.Tables;

public class CreateTableDto : IToModel<Table>
{
    public string Accountable { get; set; } = null!;
    public bool IsFullyPaid { get; set; }

    public Table ToModel()
        => new()
        {
            Accountable = Accountable,
            IsFullyPaid = IsFullyPaid
        };
}