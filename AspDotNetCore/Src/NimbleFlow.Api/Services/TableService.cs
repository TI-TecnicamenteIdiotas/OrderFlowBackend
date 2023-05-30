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

    public async Task<HttpStatusCode> UpdateTableById(Guid tableId, UpdateTableDto tableDto)
    {
        var tableEntity = await _tableRepository.GetEntityById(tableId);
        if (tableEntity is null)
            return HttpStatusCode.NotFound;

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
            return HttpStatusCode.NotModified;

        try
        {
            if (!await _tableRepository.UpdateEntity(tableEntity))
                return HttpStatusCode.InternalServerError;
        }
        catch (DbUpdateException)
        {
            return HttpStatusCode.Conflict;
        }

        return HttpStatusCode.OK;
    }
}