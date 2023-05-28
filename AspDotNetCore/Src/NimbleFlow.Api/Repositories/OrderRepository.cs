using Microsoft.EntityFrameworkCore;
using NimbleFlow.Api.Repositories.Base;
using NimbleFlow.Data.Context;
using NimbleFlow.Data.Models;

namespace NimbleFlow.Api.Repositories;

public class OrderRepository : RepositoryBase<NimbleFlowContext, Order>
{
    private readonly DbSet<OrderProduct> _orderProductEntities;

    public OrderRepository(NimbleFlowContext dbContext) : base(dbContext)
    {
        _orderProductEntities = dbContext.OrderProducts;
    }

    public async Task<Order?> GetOrderWithRelationsById(Guid orderId, bool includeDeleted, bool asTracking = false)
    {
        var query = DbEntities
            .Include(x => x.OrderProducts)
            .ThenInclude(x => x.Product)
            .ThenInclude(x => x.Category);

        if (includeDeleted)
        {
            if (asTracking)
                return await query.FirstOrDefaultAsync(x => x.Id == orderId);
            return await query.AsNoTracking().FirstOrDefaultAsync(x => x.Id == orderId);
        }

        Order? response;
        if (asTracking)
            response = await query.FirstOrDefaultAsync(x => x.DeletedAt == null && x.Id == orderId);
        else
            response = await query.AsNoTracking().FirstOrDefaultAsync(x => x.DeletedAt == null && x.Id == orderId);
        if (response is null)
            return null;

        foreach (var orderProduct in response.OrderProducts)
            if (orderProduct.Product is { DeletedAt: not null } or { Category.DeletedAt: not null })
                response.OrderProducts.Remove(orderProduct);

        return response;
    }

    public async Task<bool> AddProductToOrder(OrderProduct entity)
    {
        _ = await _orderProductEntities.AddAsync(entity);
        return await DbContext.SaveChangesAsync() == 1;
    }

    public async Task<bool> ReactivateProductInOrder(OrderProduct entity)
    {
        entity.DeletedAt = null;
        _ = _orderProductEntities.Update(entity);
        return await DbContext.SaveChangesAsync() == 1;
    }

    public async Task<bool> RemoveProductFromOrder(OrderProduct entity)
    {
        entity.DeletedAt = DateTime.UtcNow;
        _ = _orderProductEntities.Update(entity);
        return await DbContext.SaveChangesAsync() != 0;
    }
}