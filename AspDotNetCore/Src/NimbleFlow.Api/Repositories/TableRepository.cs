using Microsoft.EntityFrameworkCore;
using NimbleFlow.Api.Repositories.Base;
using NimbleFlow.Data.Context;
using NimbleFlow.Data.Models;

namespace NimbleFlow.Api.Repositories;

public class TableRepository : RepositoryBase<NimbleFlowContext, Table>
{
    public TableRepository(NimbleFlowContext dbContext) : base(dbContext)
    {
    }

    public async Task<Table?> GetTableWithRelationsById(Guid tableId, bool includeDeleted)
    {
        if (includeDeleted)
            return await DbEntities
                .Include(x => x.Orders)
                .ThenInclude(x => x.OrderProducts)
                .ThenInclude(x => x.Product)
                .ThenInclude(x => x.Category)
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == tableId);

        var response = await DbEntities
            .Include(x => x.Orders)
            .ThenInclude(x => x.OrderProducts)
            .ThenInclude(x => x.Product)
            .ThenInclude(x => x.Category)
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.DeletedAt == null && x.Id == tableId);
        if (response is null)
            return null;

        foreach (var order in response.Orders)
        {
            if (order.DeletedAt is not null)
                response.Orders.Remove(order);

            foreach (var orderProduct in order.OrderProducts)
                if (orderProduct.Product is { DeletedAt: not null } or { Category.DeletedAt: not null })
                    order.OrderProducts.Remove(orderProduct);
        }

        return response;
    }
}