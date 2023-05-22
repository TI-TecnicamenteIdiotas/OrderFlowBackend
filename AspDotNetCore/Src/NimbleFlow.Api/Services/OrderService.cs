using System.Net;
using NimbleFlow.Api.Repositories;
using NimbleFlow.Api.Services.Base;
using NimbleFlow.Contracts.DTOs.Orders;
using NimbleFlow.Data.Context;
using NimbleFlow.Data.Models;

namespace NimbleFlow.Api.Services;

public class OrderService : ServiceBase<NimbleFlowContext, Order>
{
    private readonly OrderRepository _orderRepository;

    public OrderService(OrderRepository orderRepository) : base(orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task<OrderDto?> CreateOrder(CreateOrderDto orderDto)
    {
        var response = await _orderRepository.CreateEntity(orderDto.ToModel());
        if (response is null)
            return null;

        return OrderDto.FromModel(response);
    }

    public async Task<IEnumerable<OrderDto>> GetAllOrdersPaginated(int page, int limit, bool includeDeleted)
    {
        var response = await _orderRepository.GetAllEntitiesPaginated(page, limit, includeDeleted);
        return response.Select(OrderDto.FromModel);
    }

    public async Task<OrderDto?> GetOrderById(Guid orderId)
    {
        var response = await _orderRepository.GetEntityById(orderId);
        if (response is null)
            return null;

        return OrderDto.FromModel(response);
    }

    public async Task<(HttpStatusCode, OrderDto?)> UpdateOrderById(Guid orderId, UpdateOrderDto orderDto)
    {
        var orderEntity = await _orderRepository.GetEntityById(orderId);
        if (orderEntity is null)
            return (HttpStatusCode.NotFound, null);

        var shouldUpdate = false;
        if (orderDto.TableId is not null && orderDto.TableId != Guid.Empty && orderDto.TableId != orderEntity.TableId)
        {
            orderEntity.TableId = orderDto.TableId.Value;
            shouldUpdate = true;
        }

        if (orderDto.Status is not null && (short)orderDto.Status != orderEntity.Status)
        {
            orderEntity.Status = (short)orderDto.Status;
            shouldUpdate = true;
        }

        if (orderDto.Discount != orderEntity.Discount)
        {
            orderEntity.Discount = orderDto.Discount;
            shouldUpdate = true;
        }

        if (!shouldUpdate)
            return (HttpStatusCode.NotModified, null);

        var response = await _orderRepository.UpdateEntity(orderEntity);
        if (response is null)
            return (HttpStatusCode.InternalServerError, null);

        return (HttpStatusCode.OK, OrderDto.FromModel(response));
    }
}