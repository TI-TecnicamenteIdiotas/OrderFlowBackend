using NimbleFlow.Contracts.DTOs.Categories;

namespace NimbleFlow.Contracts.Interfaces.Services;

public interface ICategoryService
{
    Task<IEnumerable<GetCategory>> GetAllCategoriesPaginated();
    Task<Guid?> CreateCategory(PostCategory category);
    Task<bool> DeleteById(Guid categoryId);
    Task<bool> UpdateCategoryById(Guid categoryId, PutCategory category);
    Task<GetCategory?> GetCategoryById(Guid categoryId);
}