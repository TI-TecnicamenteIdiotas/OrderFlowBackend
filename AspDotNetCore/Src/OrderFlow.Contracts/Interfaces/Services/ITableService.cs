using OrderFlow.Contracts.DTOs.Tables;

namespace OrderFlow.Contracts.Interfaces.Services;

public interface ITableService
{
    Task<IEnumerable<GetTable>> GetAllPaginated();
    Task<uint?> AddTable(PostTable table);
    Task<bool> DeleteTableById(uint tableId);
    Task<bool> UpdateTableById(uint tableId, PutTable table);
    Task<GetTable?> GetTableById(uint tableId);
}