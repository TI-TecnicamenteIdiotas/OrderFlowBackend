using System.Net;
using NimbleFlow.Api.Extensions;
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

    public async Task<TableDto?> CreateTable(CreateTableDto tableDto)
    {
        var response = await _tableRepository.CreateEntity(tableDto.ToModel());
        if (response is null)
            return null;

        return TableDto.FromModel(response);
    }

    public async Task<IEnumerable<TableDto>> GetAllTablesPaginated(int page, int limit, bool includeDeleted)
    {
        var response = await _tableRepository.GetAllEntitiesPaginated(page, limit, includeDeleted);
        return response.Select(TableDto.FromModel);
    }

    public async Task<TableDto?> GetTableById(Guid tableId)
    {
        var response = await _tableRepository.GetEntityById(tableId);
        if (response is null)
            return null;

        return TableDto.FromModel(response);
    }

    public async Task<HttpStatusCode> UpdateTableById(Guid tableId, UpdateTableDto tableDto)
    {
        var tableEntity = await _tableRepository.GetEntityById(tableId);
        if (tableEntity is null)
            return HttpStatusCode.NotFound;

        var shouldUpdate = false;
        if (tableDto.Accountable.IsNotNullAndNotEquals(tableDto.Accountable))
        {
            tableEntity.Accountable = tableDto.Accountable ?? throw new NullReferenceException();
            shouldUpdate = true;
        }

        if (tableDto.IsFullyPaid is not null && tableEntity.IsFullyPaid != tableDto.IsFullyPaid)
        {
            tableEntity.IsFullyPaid = tableDto.IsFullyPaid.Value;
            shouldUpdate = true;
        }

        if (!shouldUpdate)
            return HttpStatusCode.NotModified;

        if (!await _tableRepository.UpdateEntity(tableEntity))
            return HttpStatusCode.InternalServerError;

        return HttpStatusCode.OK;
    }

    public async Task<TableWithRelationsDto?> GetTableWithRelationsById(Guid tableId, bool includeDeleted)
    {
        var response = await _tableRepository.GetTableWithRelationsById(tableId, includeDeleted);
        if (response is null)
            return null;

        return TableWithRelationsDto.FromModels(response);
    }
}