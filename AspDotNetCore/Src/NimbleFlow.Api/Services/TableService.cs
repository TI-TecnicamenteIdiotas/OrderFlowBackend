using System.Net;
using Microsoft.EntityFrameworkCore;
using NimbleFlow.Api.Extensions;
using NimbleFlow.Api.Repositories;
using NimbleFlow.Api.Services.Base;
using NimbleFlow.Contracts.DTOs.Tables;
using NimbleFlow.Data.Context;
using NimbleFlow.Data.Models;
using NimbleFlow.Data.Partials.DTOs;

namespace NimbleFlow.Api.Services;

public class TableService : ServiceBase<CreateTableDto, TableDto, NimbleFlowContext, Table>
{
    private readonly TableRepository _tableRepository;

    public TableService(TableRepository tableRepository) : base(tableRepository)
    {
        _tableRepository = tableRepository;
    }

    public async Task<(HttpStatusCode, TableDto?)> UpdateTableById(Guid tableId, UpdateTableDto tableDto)
    {
        var tableEntity = await _tableRepository.GetEntityById(tableId);
        if (tableEntity is null)
            return (HttpStatusCode.NotFound, null);

        var shouldUpdate = false;
        if (tableDto.Accountable.IsNotNullAndNotEquals(tableEntity.Accountable))
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
            return (HttpStatusCode.NotModified, null);

        var updatedTable = await _tableRepository.UpdateEntity(tableEntity);
        if (updatedTable is null)
            return (HttpStatusCode.NotModified, null);

        return (HttpStatusCode.OK, updatedTable.ToDto());
    }
}