using NimbleFlow.Contracts.Interfaces;
using NimbleFlow.Data.Models;

namespace NimbleFlow.Contracts.DTOs.Tables;

public class CreateTableDto : IToModel<Table>
{
    public string Accountable { get; init; }
    public bool IsFullyPaid { get; init; }

    public CreateTableDto(string accountable)
    {
        Accountable = accountable;
    }

    public Table ToModel()
        => new()
        {
            Accountable = Accountable,
            IsFullyPaid = IsFullyPaid
        };
}