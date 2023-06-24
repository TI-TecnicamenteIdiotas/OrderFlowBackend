using Microsoft.AspNetCore.Mvc;
using NimbleFlow.Api.Controllers;
using NimbleFlow.Contracts.DTOs.Products;
using NimbleFlow.Data.Partials.DTOs;

namespace NimbleFlow.Tests.Helpers;

internal static class ProductTestHelper
{
    internal static async Task<ProductDto> CreateProductTestHelper(
        this ProductController productController,
        string productTitle,
        CategoryDto categoryDto
    )
    {
        var productDto = new CreateProductDto(productTitle, categoryDto.Id)
        {
            Description = null,
            Price = new decimal(10.0),
            ImageUrl = null,
            IsFavorite = false
        };

        var createProductResponse = await productController.CreateProduct(productDto);
        var createdProduct = ((createProductResponse as CreatedResult)!.Value as ProductDto)!;
        return createdProduct;
    }

    internal static async Task<ProductDto[]> CreateManyProductsTestHelper(
        this ProductController categoryController,
        CategoryDto categoryDto,
        params string[] productTitles
    )
    {
        var productsDto = productTitles.Select(x => new CreateProductDto(x, categoryDto.Id)).ToArray();
        var createProductResponses = new List<IActionResult>();
        foreach (var productDto in productsDto)
        {
            var response = await categoryController.CreateProduct(productDto);
            createProductResponses.Add(response);
        }

        return createProductResponses.Select(x => ((x as CreatedResult)!.Value as ProductDto)!).ToArray();
    }
}