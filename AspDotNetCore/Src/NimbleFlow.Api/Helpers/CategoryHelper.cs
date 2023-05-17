using Microsoft.AspNetCore.Mvc;
using NimbleFlow.Contracts.DTOs.Categories;

namespace NimbleFlow.Api.Helpers;

public static class CategoryHelper
{
    public static IActionResult? Validate(this PostCategory category)
    {
        if (string.IsNullOrWhiteSpace(category.Title))
            return new BadRequestObjectResult(
                $"{nameof(category.Title)} must not be null or composed by white spaces only");

        if (category.Title.Length > 50)
            return new BadRequestObjectResult($"{nameof(category.Title)} length must be under 51 characters");

        if (category.ColorTheme < 0)
            return new BadRequestObjectResult($"{nameof(category.ColorTheme)} must be positive");

        if (category.CategoryIcon < 0)
            return new BadRequestObjectResult($"{nameof(category.CategoryIcon)} must be positive");

        return null;
    }

    public static IActionResult? Validate(this PutCategory category)
    {
        if (category.Title is not null && string.IsNullOrWhiteSpace(category.Title))
            return new BadRequestObjectResult(
                $"{nameof(category.Title)} must not be null or composed by white spaces only");

        if (category.Title?.Length > 50)
            return new BadRequestObjectResult($"{nameof(category.Title)} length must be under 51 characters");

        if (category.ColorTheme < 0)
            return new BadRequestObjectResult($"{nameof(category.ColorTheme)} must be positive");

        if (category.CategoryIcon < 0)
            return new BadRequestObjectResult($"{nameof(category.CategoryIcon)} must be positive");

        return null;
    }
}