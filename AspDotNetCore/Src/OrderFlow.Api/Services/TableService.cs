using OrderFlow.Contracts.DTOs.Tables;
using OrderFlow.Contracts.Interfaces.Repositories;
using OrderFlow.Contracts.Interfaces.Services;

namespace OrderFlow.Api.Services;

public class TableService : ITableService
{
    private readonly ITableRepository _tableRepository;

    public TableService(ITableRepository tableRepository)
    {
        _tableRepository = tableRepository;
    }

    // public async Task<Table> GetById(int id)
    // {
    //     return await _repository.GetQueryable().Where(x => x.Id == id).Include(x => x.Items)
    //         .ThenInclude(i => i.Product).ThenInclude(p => p.Category).FirstOrDefaultAsync();
    // }
    //
    // public async Task<bool> DeleteTable(int value)
    // {
    //     var p = await _repository.GetById(value);
    //     if (p == null) AddError($"Mesa com ID {value} não existe");
    //     if (HasError()) return false;
    //     await _repository.Remove(value);
    //     return !HasError();
    // }
    //
    // public async Task<Table> UpdateTable(Table value)
    // {
    //     Table result = null;
    //     if (!IsValid(value)) return value;
    //
    //     result = await _repository.Update(value);
    //     if (result == null)
    //         return result;
    //
    //     var oldItems = await _itemsService.GetTableItems(result.Id);
    //     var newItems = result.Items;
    //     if (oldItems.Count() <= newItems.Count)
    //         return result;
    //
    //     var extraItems = oldItems.Where(p => !newItems.Any(p2 => p2.Id == p.Id));
    //     foreach (var item in extraItems)
    //     {
    //         await _itemsService.DeleteItem(item.Id);
    //     }
    //
    //     return result;
    // }
    public Task<IEnumerable<GetTable>> GetAllPaginated()
    {
        throw new NotImplementedException();
    }

    public Task<Guid?> AddTable(PostTable table)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteTableById(Guid tableId)
    {
        throw new NotImplementedException();
    }

    public Task<bool> UpdateTableById(Guid tableId, PutTable table)
    {
        throw new NotImplementedException();
    }

    public Task<GetTable?> GetTableById(Guid tableId)
    {
        throw new NotImplementedException();
    }
}