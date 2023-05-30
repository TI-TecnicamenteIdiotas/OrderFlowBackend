using Microsoft.AspNetCore.Mvc;
using NimbleFlow.Api.Controllers;
using NimbleFlow.Contracts.DTOs.Categories;
using NimbleFlow.Data.Partials.Dtos;

namespace NimbleFlow.Tests.Helpers;

internal static class CategoryTestHelper
{
    public static async Task<CategoryDto> CreateCategoryTestHelper(
        this CategoryController categoryController,
        string categoryName
    )
    {
        var categoryDto = new CreateCategoryDto
        {
            Title = categoryName
        };

        var createCategoryResponse = await categoryController.CreateCategory(categoryDto);
        var createdCategory = ((createCategoryResponse as CreatedResult)!.Value as CategoryDto)!;
        return createdCategory;
    }
}