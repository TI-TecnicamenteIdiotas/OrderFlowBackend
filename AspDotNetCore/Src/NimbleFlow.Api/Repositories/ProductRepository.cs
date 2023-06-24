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

    public new Task<(int totalAmount, Product[])> GetAllEntitiesPaginated(int page, int limit, bool includeDeleted)
    {
        async Task<(int totalAmount, Product[])> QueryEntities(IQueryable<Product> entities)
        {
            var totalQuery = await entities.CountAsync();
            var entitiesQuery = await entities
                .Include(x => x.Category)
                .OrderBy(x => x.CreatedAt)
                .Skip(page * limit)
                .Take(limit)
                .AsNoTracking()
                .ToArrayAsync();

            return (totalQuery, entitiesQuery);
        }

        if (includeDeleted)
            return QueryEntities(DbEntities);

        return QueryEntities(DbEntities.Where(x => x.DeletedAt == null && x.Category.DeletedAt == null));
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
                .Include(x => x.Category)
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

        return QueryEntities(DbEntities.Where(x => x.DeletedAt == null && x.Category.DeletedAt == null));
    }
}