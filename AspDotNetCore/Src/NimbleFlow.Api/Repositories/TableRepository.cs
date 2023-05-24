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

    public Task<Table?> GetTableById(Guid tableId, bool includeDeleted)
    {
        if (includeDeleted)
            return DbEntities
                .Include(x => x.Orders)
                .ThenInclude(x => x.OrderProducts)
                .ThenInclude(x => x.Product)
                .ThenInclude(x => x.Category)
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == tableId);

        return DbEntities
            .Include(x => x.Orders)
            .ThenInclude(x => x.OrderProducts)
            .ThenInclude(x => x.Product)
            .ThenInclude(x => x.Category)
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.DeletedAt == null && x.Id == tableId);
    }
}