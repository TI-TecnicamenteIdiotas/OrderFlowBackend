using Microsoft.EntityFrameworkCore;
using NimbleFlow.Contracts.Interfaces.Repositories;
using NimbleFlow.Data.Context;
using NimbleFlow.Data.Models;

namespace NimbleFlow.Api.Repositories;

public class TableRepository : ITableRepository
{
    private readonly NimbleFlowContext _dbContext;
    private readonly DbSet<Table> _tablesEntities;

    public TableRepository(NimbleFlowContext dbContext)
    {
        _dbContext = dbContext;
        _tablesEntities = dbContext.Tables;
    }

    public async Task<Table?> CreateTable(Table tableModel)
    {
        var tableEntityEntry = await _tablesEntities.AddAsync(tableModel);
        if (await _dbContext.SaveChangesAsync() != 1)
            return null;

        return tableEntityEntry.Entity;
    }

    public Task<Table[]> GetAllTablesPaginated(int page, int limit, bool includeDeleted)
    {
        Task<Table[]> QueryTables(IQueryable<Table> entities)
            => entities
                .Skip(page * limit)
                .Take(limit)
                .AsNoTracking()
                .ToArrayAsync();

        if (includeDeleted)
            return QueryTables(_tablesEntities);

        return QueryTables(_tablesEntities.Where(x => x.DeletedAt == null));
    }

    public Task<Table?> GetTableById(Guid tableId)
        => _tablesEntities.FirstOrDefaultAsync(x => x.Id == tableId);

    public async Task<Table?> UpdateTable(Table tableEntity)
    {
        _dbContext.Update(tableEntity);
        if (await _dbContext.SaveChangesAsync() != 1)
            return null;

        return tableEntity;
    }
}