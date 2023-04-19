using OrderFlow.Contracts.DTOs.Categories;

namespace OrderFlow.Contracts.Interfaces.Services;

public interface ICategoryService
{
    Task<IEnumerable<GetCategory>> GetAllPaginated();
    Task<uint?> AddCategory(PostCategory category);
    Task<bool> DeleteById(uint categoryId);
    Task<bool> UpdateCategoryById(uint categoryId, PutCategory category);
    Task<GetCategory?> GetCategoryById(uint categoryId);
}