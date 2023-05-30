using Microsoft.AspNetCore.Mvc;
using NimbleFlow.Api.Controllers;
using NimbleFlow.Contracts.DTOs.Products;
using NimbleFlow.Data.Partials.DTOs;

namespace NimbleFlow.Tests.Helpers;

internal static class ProductTestHelper
{
    internal static async Task<ProductDto> CreateProductTestHelper(
        this ProductController productController,
        string productName,
        CategoryDto categoryDto
    )
    {
        var productDto = new CreateProductDto
        {
            Title = productName,
            Description = null,
            Price = new decimal(10.0),
            ImageUrl = null,
            IsFavorite = false,
            CategoryId = categoryDto.Id
        };

        var createProductResponse = await productController.CreateProduct(productDto);
        var createdProduct = ((createProductResponse as CreatedResult)!.Value as ProductDto)!;
        return createdProduct;
    }
}