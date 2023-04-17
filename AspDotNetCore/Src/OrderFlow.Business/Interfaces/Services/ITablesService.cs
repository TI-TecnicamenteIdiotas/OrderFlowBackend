using OrderFlow.Data.Models;

namespace OrderFlow.Business.Interfaces.Services;

public interface ITablesService
{
    Task< IEnumerable<Table>> GetAll();
    Task<Table> AddTable(Table value);
    Task<bool> DeleteTable(int value);
    Task<Table> UpdateTable(Table value);
    Task<Table> GetById(int id);
}