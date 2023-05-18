using NimbleFlow.Data.Models;

namespace NimbleFlow.Contracts.Interfaces.Repositories;

public interface ITableRepository
{
    public Task<Table?> CreateTable(Table tableModel);
    public Task<Table[]> GetAllTablesPaginated(int page, int limit, bool includeDeleted);
    public Task<Table?> GetTableById(Guid tableId);
    public Task<Table?> UpdateTable(Table tableEntity);
}