using System.Net;
using Microsoft.EntityFrameworkCore;
using NimbleFlow.Api.Repositories.Base;
using NimbleFlow.Contracts.Interfaces;
using NimbleFlow.Data.Partials.Interfaces;

namespace NimbleFlow.Api.Services.Base;

public abstract class ServiceBase<TCreateDto, TDto, TDbContext, TEntity>
    where TCreateDto : IToModel<TEntity>
    where TDto : class
    where TDbContext : DbContext
    where TEntity : class, IIdentifiable<Guid>, ICreatedAtDeletedAt, IToDto<TDto>
{
    private readonly RepositoryBase<TDbContext, TEntity> _repository;

    protected ServiceBase(RepositoryBase<TDbContext, TEntity> repository)
    {
        _repository = repository;
    }

    public async Task<(HttpStatusCode, TDto?)> Create(TCreateDto createDto)
    {
        try
        {
            var response = await _repository.CreateEntity(createDto.ToModel());
            if (response is null)
                return (HttpStatusCode.InternalServerError, null);

            return (HttpStatusCode.Created, response.ToDto());
        }
        catch (DbUpdateException)
        {
            return (HttpStatusCode.Conflict, null);
        }
    }

    public async Task<IEnumerable<TDto>> GetAllPaginated(int page, int limit, bool includeDeleted)
    {
        var response = await _repository.GetAllEntitiesPaginated(page, limit, includeDeleted);
        return response.Select(x => x.ToDto());
    }

    public async Task<TDto?> GetById(Guid entityId)
    {
        var response = await _repository.GetEntityById(entityId);
        return response?.ToDto();
    }

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