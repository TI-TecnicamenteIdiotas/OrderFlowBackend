using System.Net;
using Microsoft.EntityFrameworkCore;
using NimbleFlow.Api.Repositories.Base;
using NimbleFlow.Data.Partials.Interfaces;

namespace NimbleFlow.Api.Services.Base;

public abstract class ServiceBase<TDbContext, TEntity>
    where TDbContext : DbContext
    where TEntity : class, IIdentifiable<Guid>, ICreatedAtDeletedAt
{
    private readonly RepositoryBase<TDbContext, TEntity> _repository;

    protected ServiceBase(RepositoryBase<TDbContext, TEntity> repository)
    {
        _repository = repository;
    }

    public Task<bool> ExistsById(Guid entityId)
        => _repository.ExistsById(entityId);

    public async Task<HttpStatusCode> DeleteEntityById(Guid entityId)
    {
        var entity = await _repository.GetEntityById(entityId);
        if (entity is null)
            return HttpStatusCode.NotFound;

        if (entity.DeletedAt is not null)
            return HttpStatusCode.NotFound;

        entity.DeletedAt = DateTime.UtcNow;

        if (!await _repository.UpdateEntity(entity))
            return HttpStatusCode.InternalServerError;

        return HttpStatusCode.OK;
    }
}