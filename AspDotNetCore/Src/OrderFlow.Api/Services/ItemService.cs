using OrderFlow.Contracts.Interfaces.Services;
using OrderFlow.Data.Models;

namespace OrderFlow.Api.Services;

public class ItemService : IItemService
{
    public ItemService()
    {
    }

    // public async Task<Item> AddItem(Item value)
    // {
    //     if (!IsValid(value)) return value;
    //     return await _repository.Add(value);
    // }
    //
    // private bool IsValid(Item value)
    // {
    //     if (value.Note.Length > 255)
    //         AddError("A observação deve possuir menos de 50 caracteres!");
    //
    //     if (value.Additional < 0)
    //         AddError("O valor adicional não pode ser negativo!");
    //
    //     if (value.Discount < 0)
    //         AddError("O valor de desconto não pode ser negativo!");
    //
    //     if (value.Count < 0)
    //         AddError("A quantidade não pode ser negativa!");
    //
    //     return !HasError();
    // }
    //
    // public async Task<IEnumerable<Item>> GetTableItems(int tableId)
    // {
    //     return await _repository.Get(x => x.TableId == tableId);
    // }
    //
    // public async Task<IEnumerable<Item>> GetAll()
    // {
    //     return await _repository.GetQueryable()
    //         .Include(x => x.Product).ThenInclude(p => p.Category)
    //         .ToListAsync();
    // }
    //
    // public async Task<Item> GetById(int id)
    // {
    //     return await _repository.GetQueryable().Where(x => x.Id == id).Include(x => x.Product)
    //         .ThenInclude(i => i.Category).FirstOrDefaultAsync();
    // }
    //
    // public async Task<bool> DeleteItem(int value)
    // {
    //     var p = await _repository.GetById(value);
    //     if (p == null) AddError($"Item com ID {value} não existe");
    //     if (HasError()) return false;
    //     await _repository.Remove(value);
    //     return !HasError();
    // }
    //
    // public async Task<Item> UpdateItem(Item value)
    // {
    //     Item result = null;
    //     if (!IsValid(value)) return value;
    //
    //     result = await _repository.Update(value);
    //
    //     return result;
    // }

    public Task<IEnumerable<Item>> GetAllItemsPaginated()
    {
        throw new NotImplementedException();
    }

    public Task<Guid?> CreateItem(Item item)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteItemById(Guid itemId)
    {
        throw new NotImplementedException();
    }

    public Task<bool> UpdateItemById(Item item)
    {
        throw new NotImplementedException();
    }

    public Task<Item> GetItemById(Guid itemId)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Item>> GetTableItems(Guid tableId)
    {
        throw new NotImplementedException();
    }
}