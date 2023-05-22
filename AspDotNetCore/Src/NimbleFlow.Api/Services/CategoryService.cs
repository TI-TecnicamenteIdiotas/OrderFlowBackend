using System.Net;
using NimbleFlow.Api.Extensions;
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

    public async Task<CategoryDto?> CreateCategory(CreateCategoryDto categoryDto)
    {
        var response = await _categoryRepository.CreateEntity(categoryDto.ToModel());
        if (response is null)
            return null;

        return CategoryDto.FromModel(response);
    }

    public async Task<IEnumerable<CategoryDto>> GetAllCategoriesPaginated(int page, int limit, bool includeDeleted)
    {
        var response = await _categoryRepository.GetAllEntitiesPaginated(page, limit, includeDeleted);
        return response.Select(CategoryDto.FromModel);
    }

    public async Task<CategoryDto?> GetCategoryById(Guid categoryId)
    {
        var response = await _categoryRepository.GetEntityById(categoryId);
        if (response is null)
            return null;

        return CategoryDto.FromModel(response);
    }

    public async Task<(HttpStatusCode, CategoryDto?)> UpdateCategoryById(Guid categoryId, UpdateCategoryDto categoryDto)
    {
        var categoryEntity = await _categoryRepository.GetEntityById(categoryId);
        if (categoryEntity is null)
            return (HttpStatusCode.NotFound, null);

        var shouldUpdate = false;
        if (categoryDto.Title.IsNotNullAndEquals(categoryEntity.Title))
        {
            categoryEntity.Title = categoryDto.Title ?? throw new NullReferenceException();
            shouldUpdate = true;
        }

        if (categoryDto.ColorTheme != categoryEntity.ColorTheme)
        {
            categoryEntity.ColorTheme = categoryDto.ColorTheme;
            shouldUpdate = true;
        }

        if (categoryDto.CategoryIcon != categoryEntity.CategoryIcon)
        {
            categoryEntity.CategoryIcon = categoryDto.CategoryIcon;
            shouldUpdate = true;
        }

        if (!shouldUpdate)
            return (HttpStatusCode.NotModified, null);

        var response = await _categoryRepository.UpdateEntity(categoryEntity);
        if (response is null)
            return (HttpStatusCode.InternalServerError, null);

        return (HttpStatusCode.OK, CategoryDto.FromModel(response));
    }
}