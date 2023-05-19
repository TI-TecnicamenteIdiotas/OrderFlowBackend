using Microsoft.EntityFrameworkCore;
using NimbleFlow.Data.Base;

namespace NimbleFlow.Api.Repositories.Base;

public abstract class RepositoryBase<TDbContext, TEntity>
    where TDbContext : DbContext
    where TEntity : ModelBase
{
    private readonly TDbContext _dbContext;
    private readonly DbSet<TEntity> _dbEntities;

    protected RepositoryBase(TDbContext dbContext)
    {
        _dbContext = dbContext;
        _dbEntities = _dbContext.Set<TEntity>();
    }

    public async Task<TEntity?> CreateEntity(TEntity entity)
    {
        var entityEntry = await _dbEntities.AddAsync(entity);
        if (await _dbContext.SaveChangesAsync() != 1)
            return null;

        return entityEntry.Entity;
    }

    public Task<TEntity[]> GetAllEntitiesPaginated(int page, int limit, bool includeDeleted)
    {
        Task<TEntity[]> QueryEntities(IQueryable<TEntity> entities)
            => entities
                .Skip(page * limit)
                .Take(limit)
                .AsNoTracking()
                .ToArrayAsync();

        if (includeDeleted)
            return QueryEntities(_dbEntities);

        return QueryEntities(_dbEntities.Where(x => x.DeletedAt == null));
    }

    public Task<TEntity?> GetEntityById(Guid entityId)
        => _dbEntities.FirstOrDefaultAsync(x => x.Id == entityId);

    public async Task<TEntity?> UpdateEntity(TEntity entity)
    {
        _dbContext.Update(entity);
        if (await _dbContext.SaveChangesAsync() != 1)
            return null;

        return entity;
    }
}