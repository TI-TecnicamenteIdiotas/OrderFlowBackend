using System.Net;
using Microsoft.EntityFrameworkCore;
using NimbleFlow.Api.Extensions;
using NimbleFlow.Api.Repositories;
using NimbleFlow.Api.Services.Base;
using NimbleFlow.Contracts.DTOs.Categories;
using NimbleFlow.Data.Context;
using NimbleFlow.Data.Models;
using NimbleFlow.Data.Partials.DTOs;

namespace NimbleFlow.Api.Services;

public class CategoryService : ServiceBase<CreateCategoryDto, CategoryDto, NimbleFlowContext, Category>
{
    private readonly CategoryRepository _categoryRepository;

    public CategoryService(CategoryRepository categoryRepository) : base(categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<(HttpStatusCode, CategoryDto?)> UpdateCategoryById(Guid categoryId, UpdateCategoryDto categoryDto)
    {
        var categoryEntity = await _categoryRepository.GetEntityById(categoryId);
        if (categoryEntity is null)
            return (HttpStatusCode.NotFound, null);

        var shouldUpdate = false;
        if (categoryDto.Title.IsNotNullAndNotEquals(categoryEntity.Title))
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

        try
        {
            var updatedCategory = await _categoryRepository.UpdateEntity(categoryEntity);
            if (updatedCategory is null)
                return (HttpStatusCode.NotModified, null);

            return (HttpStatusCode.OK, updatedCategory.ToDto());
        }
        catch (DbUpdateException)
        {
            return (HttpStatusCode.Conflict, null);
        }
    }
}