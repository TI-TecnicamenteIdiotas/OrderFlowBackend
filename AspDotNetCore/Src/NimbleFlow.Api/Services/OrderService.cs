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

    public async Task<HttpStatusCode> UpdateOrderById(Guid orderId, UpdateOrderDto orderDto)
    {
        var orderEntity = await _orderRepository.GetEntityById(orderId);
        if (orderEntity is null)
            return HttpStatusCode.NotFound;

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
            return HttpStatusCode.NotModified;

        if (!await _orderRepository.UpdateEntity(orderEntity))
            return HttpStatusCode.InternalServerError;

        return HttpStatusCode.OK;
    }

    public async Task<OrderWithRelationsDto?> GetOrderWithRelationsById(Guid tableId, bool includeDeleted)
    {
        var response = await _orderRepository.GetOrderWithRelationsById(tableId, includeDeleted);
        if (response is null)
            return null;

        return OrderWithRelationsDto.FromModel(response);
    }

    public async Task<bool> AddProductToOrderByOrderId(Guid orderId, CreateOrderProductDto createOrderProductDto)
    {
        var order = await _orderRepository.GetOrderWithRelationsById(orderId, true);
        var orderProduct = order?.OrderProducts.FirstOrDefault(x => x.Product.Id == createOrderProductDto.ProductId);
        if (orderProduct is not null)
        {
            var orderProductModel = createOrderProductDto.ToModel(orderId);
            orderProductModel.CreatedAt = orderProduct.CreatedAt;
            orderProductModel.ProductAmount = createOrderProductDto.ProductAmount;
            return await _orderRepository.ReactivateProductInOrder(orderProductModel);
        }

        return await _orderRepository.AddProductToOrder(createOrderProductDto.ToModel(orderId));
    }

    public async Task<bool> RemoveProductFromOrderByIds(Guid orderId, Guid productId)
        => await _orderRepository.RemoveProductFromOrder(orderId, productId);
}