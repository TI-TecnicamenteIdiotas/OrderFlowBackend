using System.Net;
using NimbleFlow.Api.Repositories;
using NimbleFlow.Api.Services.Base;
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
    
    // public Task<GetOrder?> CreateOrder(PostOrder orderDto)
    // {
    //     throw new NotImplementedException();
    // }
    //
    // public Task<IEnumerable<GetOrder>> GetAllOrdersPaginated(int page, int limit, bool includeDeleted)
    // {
    //     throw new NotImplementedException();
    // }
    //
    // public Task<GetOrder?> GetOrderById(Guid orderId)
    // {
    //     throw new NotImplementedException();
    // }
    //
    // public Task<(HttpStatusCode, GetOrder?)> UpdateOrderById(Guid orderId, PutOrder orderDto)
    // {
    //     throw new NotImplementedException();
    // }
}