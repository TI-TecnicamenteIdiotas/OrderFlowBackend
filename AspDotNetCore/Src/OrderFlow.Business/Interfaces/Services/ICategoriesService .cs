using OrderFlow.Data.Models;

namespace OrderFlow.Business.Interfaces.Services;

public interface ICategoriesService
{
    Task< IEnumerable<Category>> GetAll();
    Task<Category> AddCategory(Category value);
    Task<bool> DeleteCategory(int value);
    Task<Category> UpdateCategory(Category value);
    Task<Category> GetById(int id);
}