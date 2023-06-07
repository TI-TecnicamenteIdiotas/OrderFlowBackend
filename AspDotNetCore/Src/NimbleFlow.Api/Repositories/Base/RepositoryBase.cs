using Microsoft.EntityFrameworkCore;
using NimbleFlow.Data.Partials.Interfaces;

namespace NimbleFlow.Api.Repositories.Base;

public abstract class RepositoryBase<TDbContext, TEntity>
    where TDbContext : DbContext
    where TEntity : class, IIdentifiable<Guid>, ICreatedAtDeletedAt
{
    private readonly TDbContext _dbContext;
    protected readonly DbSet<TEntity> DbEntities;

    protected RepositoryBase(TDbContext dbContext)
    {
        _dbContext = dbContext;
        DbEntities = _dbContext.Set<TEntity>();
    }

    public async Task<TEntity?> CreateEntity(TEntity entity)
    {
        var entityEntry = await DbEntities.AddAsync(entity);
        if (await _dbContext.SaveChangesAsync() != 1)
            return null;

        return entityEntry.Entity;
    }

    public Task<TEntity[]> GetAllEntitiesPaginated(int page, int limit, bool includeDeleted)
    {
        Task<TEntity[]> QueryEntities(IQueryable<TEntity> entities)
            => entities
                .OrderByDescending(x => x.CreatedAt)
                .Skip(page * limit)
                .Take(limit)
                .AsNoTracking()
                .ToArrayAsync();

        if (includeDeleted)
            return QueryEntities(DbEntities);

        return QueryEntities(DbEntities.Where(x => x.DeletedAt == null));
    }

    public Task<bool> ExistsById(Guid entityId)
        => DbEntities.AnyAsync(x => x.Id == entityId);

    public Task<TEntity?> GetEntityById(Guid entityId)
        => DbEntities.FirstOrDefaultAsync(x => x.Id == entityId);

    public async Task<bool> UpdateEntity(TEntity entity)
    {
        _ = _dbContext.Update(entity);
        return await _dbContext.SaveChangesAsync() == 1;
    }
}