using OrderFlow.Data.Models;

namespace OrderFlow.Business.Interfaces.Services;

public interface IItemsService
{
    Task< IEnumerable<Item>> GetAll();
    Task<Item> AddItem(Item value);
    Task<bool> DeleteItem(int value);
    Task<Item> UpdateItem(Item value);
    Task<Item> GetById(int id);

    Task<IEnumerable<Item>> GetTableItems(int tableId);
}