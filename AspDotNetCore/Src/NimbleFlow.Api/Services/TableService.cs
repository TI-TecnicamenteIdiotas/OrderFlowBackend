using System.Net;
using NimbleFlow.Api.Repositories;
using NimbleFlow.Api.Services.Base;
using NimbleFlow.Contracts.DTOs.Tables;
using NimbleFlow.Data.Context;
using NimbleFlow.Data.Models;

namespace NimbleFlow.Api.Services;

public class TableService : ServiceBase<NimbleFlowContext, Table>
{
    private readonly TableRepository _tableRepository;

    public TableService(TableRepository tableRepository) : base(tableRepository)
    {
        _tableRepository = tableRepository;
    }

    public async Task<GetTable?> CreateTable(PostTable tableDto)
    {
        var response = await _tableRepository.CreateEntity(tableDto.ToModel());
        if (response is null)
            return null;

        return GetTable.FromModel(response);
    }

    public async Task<IEnumerable<GetTable>> GetAllTablesPaginated(int page, int limit, bool includeDeleted)
    {
        var response = await _tableRepository.GetAllEntitiesPaginated(page, limit, includeDeleted);
        return response.Select(GetTable.FromModel);
    }

    public async Task<GetTable?> GetTableById(Guid tableId)
    {
        var response = await _tableRepository.GetEntityById(tableId);
        if (response is null)
            return null;

        return GetTable.FromModel(response);
    }

    public async Task<(HttpStatusCode, GetTable?)> UpdateTableById(Guid tableId, PutTable tableDto)
    {
        var tableEntity = await _tableRepository.GetEntityById(tableId);
        if (tableEntity is null)
            return (HttpStatusCode.NotFound, null);

        var shouldUpdate = false;
        if (!string.IsNullOrWhiteSpace(tableDto.Accountable)
            && !tableEntity.Accountable.Equals(tableDto.Accountable, StringComparison.InvariantCultureIgnoreCase))
        {
            tableEntity.Accountable = tableDto.Accountable;
            shouldUpdate = true;
        }

        if (tableDto.IsFullyPaid is not null && tableEntity.IsFullyPaid != tableDto.IsFullyPaid)
        {
            tableEntity.IsFullyPaid = tableDto.IsFullyPaid.Value;
            shouldUpdate = true;
        }

        if (!shouldUpdate)
            return (HttpStatusCode.NotModified, null);

        var response = await _tableRepository.UpdateEntity(tableEntity);
        if (response is null)
            return (HttpStatusCode.InternalServerError, null);

        return (HttpStatusCode.OK, GetTable.FromModel(response));
    }
}