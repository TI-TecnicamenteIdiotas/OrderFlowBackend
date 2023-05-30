using System.Net;
using Microsoft.EntityFrameworkCore;
using NimbleFlow.Api.Extensions;
using NimbleFlow.Api.Repositories;
using NimbleFlow.Api.Services.Base;
using NimbleFlow.Contracts.DTOs.Categories;
using NimbleFlow.Data.Context;
using NimbleFlow.Data.Models;
using NimbleFlow.Data.Partials.Dtos;

namespace NimbleFlow.Api.Services;

public class CategoryService : ServiceBase<CreateCategoryDto, CategoryDto, NimbleFlowContext, Category>
{
    private readonly CategoryRepository _categoryRepository;

    public CategoryService(CategoryRepository categoryRepository) : base(categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<HttpStatusCode> UpdateCategoryById(Guid categoryId, UpdateCategoryDto categoryDto)
    {
        var categoryEntity = await _categoryRepository.GetEntityById(categoryId);
        if (categoryEntity is null)
            return HttpStatusCode.NotFound;

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
            return HttpStatusCode.NotModified;

        try
        {
            if (!await _categoryRepository.UpdateEntity(categoryEntity))
                return HttpStatusCode.NotModified;
        }
        catch (DbUpdateException)
        {
            return HttpStatusCode.Conflict;
        }

        return HttpStatusCode.OK;
    }
}