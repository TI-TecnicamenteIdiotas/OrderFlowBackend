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

    public Task<Product[]> GetAllProductsPaginatedByCategoryId(
        int page,
        int limit,
        bool includeDeleted,
        Guid categoryId
    )
    {
        Task<Product[]> QueryEntities(IQueryable<Product> entities)
            => entities
                .OrderBy(x => x.CreatedAt)
                .Skip(page * limit)
                .Take(limit)
                .Where(x => x.CategoryId == categoryId)
                .AsNoTracking()
                .ToArrayAsync();

        if (includeDeleted)
            return QueryEntities(DbEntities);

        return QueryEntities(DbEntities.Where(x => x.DeletedAt == null));
    }
}