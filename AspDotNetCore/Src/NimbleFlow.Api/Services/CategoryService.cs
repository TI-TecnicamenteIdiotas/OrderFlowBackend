using NimbleFlow.Contracts.DTOs.Categories;
using NimbleFlow.Contracts.Interfaces.Repositories;
using NimbleFlow.Contracts.Interfaces.Services;

namespace NimbleFlow.Api.Services;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _categoryRepository;

    public CategoryService(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public Task<IEnumerable<GetCategory>> GetAllCategoriesPaginated()
    {
        throw new NotImplementedException();
    }

    public Task<Guid?> CreateCategory(PostCategory category)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteById(Guid categoryId)
    {
        throw new NotImplementedException();
    }

    public Task<bool> UpdateCategoryById(Guid categoryId, PutCategory category)
    {
        throw new NotImplementedException();
    }

    public Task<GetCategory?> GetCategoryById(Guid categoryId)
    {
        throw new NotImplementedException();
    }
}