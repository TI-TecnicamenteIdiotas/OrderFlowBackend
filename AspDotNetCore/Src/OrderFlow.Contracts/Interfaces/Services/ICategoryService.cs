using OrderFlow.Contracts.DTOs.Categories;

namespace OrderFlow.Contracts.Interfaces.Services;

public interface ICategoryService
{
    Task<IEnumerable<GetCategory>> GetAllPaginated();
    Task<Guid?> AddCategory(PostCategory category);
    Task<bool> DeleteById(Guid categoryId);
    Task<bool> UpdateCategoryById(Guid categoryId, PutCategory category);
    Task<GetCategory?> GetCategoryById(Guid categoryId);
}