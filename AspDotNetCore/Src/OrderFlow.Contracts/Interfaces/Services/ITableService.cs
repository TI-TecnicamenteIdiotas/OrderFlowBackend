using OrderFlow.Contracts.DTOs.Tables;

namespace OrderFlow.Contracts.Interfaces.Services;

public interface ITableService
{
    Task<IEnumerable<GetTable>> GetAllTablesPaginated();
    Task<Guid?> CreateTable(PostTable table);
    Task<bool> DeleteTableById(Guid tableId);
    Task<bool> UpdateTableById(Guid tableId, PutTable table);
    Task<GetTable?> GetTableById(Guid tableId);
}