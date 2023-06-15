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

    public Task<(int totalAmount, TEntity[])> GetAllEntitiesPaginated(int page, int limit, bool includeDeleted)
    {
        async Task<(int totalAmount, TEntity[])> QueryEntities(IQueryable<TEntity> entities)
        {
            var totalQuery = await entities.CountAsync();
            var entitiesQuery = await entities
                .OrderBy(x => x.CreatedAt)
                .Skip(page * limit)
                .Take(limit)
                .AsNoTracking()
                .ToArrayAsync();

            return (totalQuery, entitiesQuery);
        }

        if (includeDeleted)
            return QueryEntities(DbEntities);

        return QueryEntities(DbEntities.Where(x => x.DeletedAt == null));
    }

    public Task<TEntity[]> GetManyEntitiesByIds(Guid[] entityIds, bool includeDeleted)
    {
        if (includeDeleted)
            return DbEntities.Where(x => entityIds.Contains(x.Id)).ToArrayAsync();

        return DbEntities.Where(x => x.DeletedAt == null && entityIds.Contains(x.Id)).ToArrayAsync();
    }

    public Task<TEntity?> GetEntityById(Guid entityId)
        => DbEntities.FirstOrDefaultAsync(x => x.Id == entityId);

    public async Task<bool> UpdateManyEntities(TEntity[] entities)
    {
        DbEntities.UpdateRange(entities);
        return await _dbContext.SaveChangesAsync() != 0;
    }

    public async Task<TEntity?> UpdateEntity(TEntity entity)
    {
        var updatedEntity = _dbContext.Update(entity);
        if (await _dbContext.SaveChangesAsync() != 1)
            return null;

        return updatedEntity.Entity;
    }
}