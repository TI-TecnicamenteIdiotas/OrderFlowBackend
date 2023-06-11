using Microsoft.AspNetCore.Mvc;
using NimbleFlow.Api.Controllers;
using NimbleFlow.Contracts.DTOs.Categories;
using NimbleFlow.Data.Partials.DTOs;

namespace NimbleFlow.Tests.Helpers;

internal static class CategoryTestHelper
{
    internal static async Task<CategoryDto> CreateCategoryTestHelper(
        this CategoryController categoryController,
        string categoryTitle
    )
    {
        var categoryDto = new CreateCategoryDto(categoryTitle);
        var createCategoryResponse = await categoryController.CreateCategory(categoryDto);
        var createdCategory = ((createCategoryResponse as CreatedResult)!.Value as CategoryDto)!;
        return createdCategory;
    }
}