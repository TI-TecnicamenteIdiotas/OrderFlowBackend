using Microsoft.AspNetCore.Mvc;
using NimbleFlow.Contracts.DTOs.Products;

namespace NimbleFlow.Api.Helpers;

public static class ProductHelper
{
    public static IActionResult? Validate(this CreateProductDto productDto)
    {
        if (string.IsNullOrWhiteSpace(productDto.Title))
            return new BadRequestObjectResult(
                $"{nameof(productDto.Title)} must not be null or composed by white spaces only");

        if (productDto.Title.Length > 50)
            return new BadRequestObjectResult($"{nameof(productDto.Title)} length must be under 51 characters");

        if (productDto.Description?.Length > 255)
            return new BadRequestObjectResult($"{nameof(productDto.Description)} length must be under 256 characters");

        if (productDto.Price < 0)
            return new BadRequestObjectResult($"{nameof(productDto.Price)} must not be negative");

        return null;
    }

    public static IActionResult? Validate(this UpdateProductDto productDto)
    {
        if (productDto.Title is not null && string.IsNullOrWhiteSpace(productDto.Title))
            return new BadRequestObjectResult(
                $"{nameof(productDto.Title)} must not be null or composed by white spaces only");

        if (productDto.Title?.Length > 50)
            return new BadRequestObjectResult($"{nameof(productDto.Title)} length must be under 51 characters");

        if (productDto.Description?.Length > 255)
            return new BadRequestObjectResult($"{nameof(productDto.Description)} length must be under 256 characters");

        if (productDto.Price < 0)
            return new BadRequestObjectResult($"{nameof(productDto.Price)} must not be negative");

        return null;
    }
}