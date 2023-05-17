using NimbleFlow.Data.Models;

namespace NimbleFlow.Contracts.Interfaces.Services;

public interface IItemService
{
    Task<IEnumerable<Item>> GetAllItemsPaginated();
    Task<Guid?> CreateItem(Item item);
    Task<bool> DeleteItemById(Guid itemId);
    Task<bool> UpdateItemById(Item item);
    Task<Item> GetItemById(Guid itemId);
    Task<IEnumerable<Item>> GetTableItems(Guid tableId);
}