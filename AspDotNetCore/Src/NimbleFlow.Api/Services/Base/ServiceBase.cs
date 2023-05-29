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

    public async Task<HttpStatusCode> DeleteEntityById(Guid tableId)
    {
        var tableEntity = await _repository.GetEntityById(tableId);
        if (tableEntity is null)
            return HttpStatusCode.NotFound;

        if (tableEntity.DeletedAt is not null)
            return HttpStatusCode.NotFound;

        tableEntity.DeletedAt = DateTime.UtcNow;

        if (await _repository.UpdateEntity(tableEntity))
            return HttpStatusCode.InternalServerError;

        return HttpStatusCode.OK;
    }
}