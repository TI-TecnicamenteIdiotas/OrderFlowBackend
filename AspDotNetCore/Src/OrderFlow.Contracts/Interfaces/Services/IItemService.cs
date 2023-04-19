using OrderFlow.Data.Models;

namespace OrderFlow.Contracts.Interfaces.Services;

public interface IItemService
{
    Task<IEnumerable<Item>> GetAllPaginated();
    Task<uint?> AddItem(Item item);
    Task<bool> DeleteItemById(uint itemId);
    Task<bool> UpdateItemById(Item item);
    Task<Item> GetItemById(uint itemId);
    Task<IEnumerable<Item>> GetTableItems(uint tableId);
}