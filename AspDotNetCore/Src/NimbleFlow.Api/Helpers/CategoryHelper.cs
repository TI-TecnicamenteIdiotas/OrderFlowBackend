using Microsoft.AspNetCore.Mvc;
using NimbleFlow.Contracts.DTOs.Categories;

namespace NimbleFlow.Api.Helpers;

public static class CategoryHelper
{
    public static IActionResult? Validate(this CreateCategoryDto categoryDto)
    {
        if (string.IsNullOrWhiteSpace(categoryDto.Title))
            return new BadRequestObjectResult(
                $"{nameof(categoryDto.Title)} must not be null or composed by white spaces only");

        if (categoryDto.Title.Length > 50)
            return new BadRequestObjectResult($"{nameof(categoryDto.Title)} length must be under 51 characters");

        if (categoryDto.ColorTheme < 0)
            return new BadRequestObjectResult($"{nameof(categoryDto.ColorTheme)} must be positive");

        if (categoryDto.CategoryIcon < 0)
            return new BadRequestObjectResult($"{nameof(categoryDto.CategoryIcon)} must be positive");

        return null;
    }

    public static IActionResult? Validate(this UpdateCategoryDto categoryDto)
    {
        if (categoryDto.Title is not null && string.IsNullOrWhiteSpace(categoryDto.Title))
            return new BadRequestObjectResult(
                $"{nameof(categoryDto.Title)} must not be null or composed by white spaces only");

        if (categoryDto.Title?.Length > 50)
            return new BadRequestObjectResult($"{nameof(categoryDto.Title)} length must be under 51 characters");

        if (categoryDto.ColorTheme < 0)
            return new BadRequestObjectResult($"{nameof(categoryDto.ColorTheme)} must be positive");

        if (categoryDto.CategoryIcon < 0)
            return new BadRequestObjectResult($"{nameof(categoryDto.CategoryIcon)} must be positive");

        return null;
    }
}