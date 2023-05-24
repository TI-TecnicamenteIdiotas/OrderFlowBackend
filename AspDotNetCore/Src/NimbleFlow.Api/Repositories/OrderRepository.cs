﻿using Microsoft.EntityFrameworkCore;
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

    public async Task<OrderProduct?> AddProductToOrder(OrderProduct entity)
    {
        var entityEntry = await _orderProductEntities.AddAsync(entity);
        if (await DbContext.SaveChangesAsync() != 1)
            return null;

        return entityEntry.Entity;
    }

    public Task<Order[]> GetOrdersByTableId(Guid tableId, bool includeDeleted)
    {
        if (includeDeleted)
            return DbEntities.Include(x => x.OrderProducts).Where(x => x.TableId == tableId).ToArrayAsync();

        return DbEntities
            .Include(x => x.OrderProducts)
            .Where(x => x.DeletedAt == null && x.TableId == tableId)
            .ToArrayAsync();
    }
}