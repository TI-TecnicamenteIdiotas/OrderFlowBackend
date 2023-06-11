using Microsoft.EntityFrameworkCore;
using NimbleFlow.Api.Repositories.Base;
using NimbleFlow.Data.Context;
using NimbleFlow.Data.Models;

namespace NimbleFlow.Api.Repositories;

public class ProductRepository : RepositoryBase<NimbleFlowContext, Product>
{
    public ProductRepository(NimbleFlowContext dbContext) : base(dbContext)
    {
    }

    public Task<(int totalAmount, Product[])> GetAllProductsPaginatedByCategoryId(
        int page,
        int limit,
        bool includeDeleted,
        Guid categoryId
    )
    {
        async Task<(int totalAmount, Product[])> QueryEntities(IQueryable<Product> entities)
        {
            var totalQuery = await entities.CountAsync();
            var entitiesQuery = await entities
                .OrderBy(x => x.CreatedAt)
                .Skip(page * limit)
                .Take(limit)
                .Where(x => x.CategoryId == categoryId)
                .AsNoTracking()
                .ToArrayAsync();

            return (totalQuery, entitiesQuery);
        }

        if (includeDeleted)
            return QueryEntities(DbEntities);

        return QueryEntities(DbEntities.Where(x => x.DeletedAt == null));
    }
}