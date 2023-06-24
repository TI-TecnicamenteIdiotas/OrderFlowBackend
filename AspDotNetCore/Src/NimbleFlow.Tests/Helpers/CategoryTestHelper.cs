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

    internal static async Task<CategoryDto[]> CreateManyCategoriesTestHelper(
        this CategoryController categoryController,
        params string[] categoryTitles
    )
    {
        var categoriesDto = categoryTitles.Select(x => new CreateCategoryDto(x)).ToArray();
        var createCategoryResponses = new List<IActionResult>();
        foreach (var categoryDto in categoriesDto)
        {
            var response = await categoryController.CreateCategory(categoryDto);
            createCategoryResponses.Add(response);
        }

        return createCategoryResponses.Select(x => ((x as CreatedResult)!.Value as CategoryDto)!).ToArray();
    }
}