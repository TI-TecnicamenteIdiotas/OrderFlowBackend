using System.Net;
using NimbleFlow.Contracts.DTOs.Tables;

namespace NimbleFlow.Contracts.Interfaces.Services;

public interface ITableService
{
    Task<GetTable?> CreateTable(PostTable tableDto);
    Task<IEnumerable<GetTable>> GetAllTablesPaginated(int page, int limit, bool includeDeleted);
    Task<GetTable?> GetTableById(Guid tableId);
    Task<(HttpStatusCode, GetTable?)> UpdateTableById(Guid tableId, PutTable tableDto);
    Task<HttpStatusCode> DeleteTableById(Guid tableId);
}