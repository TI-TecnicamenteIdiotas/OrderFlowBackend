using Microsoft.AspNetCore.Mvc;
using NimbleFlow.Contracts.DTOs.Products;

namespace NimbleFlow.Api.Helpers;

public static class ProductHelper
{
    public static IActionResult? Validate(this PostProduct product)
    {
        if (string.IsNullOrWhiteSpace(product.Title))
            return new BadRequestObjectResult(
                $"{nameof(product.Title)} must not be null or composed by white spaces only");

        if (product.Title.Length > 50)
            return new BadRequestObjectResult($"{nameof(product.Title)} length must be under 51 characters");

        if (product.Description?.Length > 255)
            return new BadRequestObjectResult($"{nameof(product.Description)} length must be under 256 characters");

        if (product.Price < 0)
            return new BadRequestObjectResult($"{nameof(product.Price)} must not be negative");

        return null;
    }

    public static IActionResult? Validate(this PutProduct product)
    {
        if (product.Title is not null && string.IsNullOrWhiteSpace(product.Title))
            return new BadRequestObjectResult(
                $"{nameof(product.Title)} must not be null or composed by white spaces only");

        if (product.Title?.Length > 50)
            return new BadRequestObjectResult($"{nameof(product.Title)} length must be under 51 characters");

        if (product.Description?.Length > 255)
            return new BadRequestObjectResult($"{nameof(product.Description)} length must be under 256 characters");

        if (product.Price < 0)
            return new BadRequestObjectResult($"{nameof(product.Price)} must not be negative");

        return null;
    }
}