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
    private readonly OrderRepository _orderRepository;

    public TableService(
        TableRepository tableRepository,
        OrderRepository orderRepository
    ) : base(tableRepository)
    {
        _tableRepository = tableRepository;
        _orderRepository = orderRepository;
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

    public async Task<(HttpStatusCode, TableDto?)> UpdateTableById(Guid tableId, UpdateTableDto tableDto)
    {
        var tableEntity = await _tableRepository.GetEntityById(tableId);
        if (tableEntity is null)
            return (HttpStatusCode.NotFound, null);

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
            return (HttpStatusCode.NotModified, null);

        var response = await _tableRepository.UpdateEntity(tableEntity);
        if (response is null)
            return (HttpStatusCode.InternalServerError, null);

        return (HttpStatusCode.OK, TableDto.FromModel(response));
    }

    public async Task<TableWithOrdersDto?> GetTableWithOrdersByTableId(Guid tableId, bool includeDeleted)
    {
        var tableEntity = await _tableRepository.GetEntityById(tableId);
        if (tableEntity is null)
            return null;

        var ordersResponse = await _orderRepository.GetOrdersByTableId(tableId, includeDeleted);
        return TableWithOrdersDto.FromModels(tableEntity, ordersResponse);
    }

    public async Task<TableWithOrdersAndProductsDto?> GetTableWithOrdersAndProductsByTableId(Guid tableId, bool includeDeleted)
    {
        var tableEntity = await _tableRepository.GetEntityById(tableId);
        if (tableEntity is null)
            return null;

        var ordersResponse = await _orderRepository.GetOrdersWithProductsByTableId(tableId, includeDeleted);
        return TableWithOrdersAndProductsDto.FromModels(tableEntity, ordersResponse);
    }
}