using System.Net;
using NimbleFlow.Api.Repositories;
using NimbleFlow.Api.Services.Base;
using NimbleFlow.Contracts.DTOs.Categories;
using NimbleFlow.Data.Context;
using NimbleFlow.Data.Models;

namespace NimbleFlow.Api.Services;

public class CategoryService : ServiceBase<NimbleFlowContext, Category>
{
    private readonly CategoryRepository _categoryRepository;

    public CategoryService(CategoryRepository categoryRepository) : base(categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public Task<GetCategory?> CreateCategory(PostCategory categoryDto)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<GetCategory>> GetAllCategoriesPaginated(int page, int limit, bool includeDeleted)
    {
        throw new NotImplementedException();
    }

    public Task<GetCategory?> GetCategoryById(Guid categoryId)
    {
        throw new NotImplementedException();
    }

    public Task<(HttpStatusCode, GetCategory?)> UpdateCategoryById(Guid categoryId, PutCategory categoryDto)
    {
        throw new NotImplementedException();
    }
}