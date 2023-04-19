using OrderFlow.Data.Models;

namespace OrderFlow.Contracts.Interfaces.Services;

public interface IItemService
{
    Task<IEnumerable<Item>> GetAllPaginated();
    Task<Guid?> AddItem(Item item);
    Task<bool> DeleteItemById(Guid itemId);
    Task<bool> UpdateItemById(Item item);
    Task<Item> GetItemById(Guid itemId);
    Task<IEnumerable<Item>> GetTableItems(Guid tableId);
}