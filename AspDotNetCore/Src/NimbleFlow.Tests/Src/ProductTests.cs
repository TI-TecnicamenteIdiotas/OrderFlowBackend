using Microsoft.AspNetCore.Mvc;
using NimbleFlow.Contracts.DTOs.Products;
using NimbleFlow.Tests.Base;
using NimbleFlow.Tests.Helpers;

namespace NimbleFlow.Tests;

public class ProductTests : TestBase
{
    #region Create

    [Fact]
    public async Task Create_Product_ShouldReturnCreatedResult()
    {
        // Arrange
        var createdCategory = await CategoryController.CreateCategoryTestHelper("Category A");
        var productDto = new CreateProductDto
        {
            Title = "Product A",
            Description = null,
            Price = new decimal(10.0),
            ImageUrl = null,
            IsFavorite = false,
            CategoryId = createdCategory.Id
        };

        // Act
        var actionResult = await ProductController.CreateProduct(productDto);

        // Assert
        Assert.True(actionResult is CreatedResult);
    }

    [Fact]
    public async Task Create_Product_ShouldReturnBadRequestResult()
    {
        // Arrange
        var productDto = new CreateProductDto
        {
            Title = "Product A",
            Description = null,
            Price = new decimal(10.0),
            ImageUrl = null,
            IsFavorite = false,
            CategoryId = Guid.NewGuid()
        };

        // Act
        var actionResult = await ProductController.CreateProduct(productDto);

        // Assert
        Assert.True(actionResult is BadRequestResult);
    }

    #endregion

    #region Get All Paginated

    [Fact]
    public async Task GetAll_ProductsPaginated_ShouldReturnOkObjectResult()
    {
        // Arrange
        var createdCategory = await CategoryController.CreateCategoryTestHelper("Category A");
        _ = await ProductController.CreateProductTestHelper("Product A", createdCategory);

        // Act
        var actionResult = await ProductController.GetAllProductsPaginated();

        // Assert
        Assert.True(actionResult is OkObjectResult);
    }

    [Fact]
    public async Task GetAll_ProductsPaginated_ShouldReturnNoContentResult()
    {
        // Arrange
        // Act
        var actionResult = await ProductController.GetAllProductsPaginated();

        // Assert
        Assert.True(actionResult is NoContentResult);
    }

    #endregion

    #region By Id

    [Fact]
    public async Task Get_ProductById_ShouldReturnOkObjectResult()
    {
        // Arrange
        var createdCategory = await CategoryController.CreateCategoryTestHelper("Category A");
        var createdProduct = await ProductController.CreateProductTestHelper("Product A", createdCategory);

        // Act
        var actionResult = await ProductController.GetProductById(createdProduct.Id);

        // Assert
        Assert.True(actionResult is OkObjectResult);
    }

    [Fact]
    public async Task Get_ProductById_ShouldReturnNotFound()
    {
        // Arrange
        var productId = Guid.NewGuid();

        // Act
        var actionResult = await ProductController.GetProductById(productId);

        // Assert
        Assert.True(actionResult is NotFoundResult);
    }

    #endregion

    #region Update By Id

    [Fact]
    public async Task Update_ProductById_ShouldReturnOkResult()
    {
        // Arrange
        var createdCategory = await CategoryController.CreateCategoryTestHelper("Category A");
        var createdProduct = await ProductController.CreateProductTestHelper("Product A", createdCategory);
        var updateProductDto = new UpdateProductDto
        {
            Title = "Product A Updated"
        };

        // Act
        var actionResult = await ProductController.UpdateProductById(createdProduct.Id, updateProductDto);

        // Assert
        Assert.True(actionResult is OkResult);
    }

    [Fact]
    public async Task Update_ProductById_ShouldReturnConflictResult()
    {
        // Arrange
        var createdCategory = await CategoryController.CreateCategoryTestHelper("Category A");
        _ = await ProductController.CreateProductTestHelper("Product A", createdCategory);
        var createdProduct = await ProductController.CreateProductTestHelper("Product B", createdCategory);
        var updateProductDto = new UpdateProductDto
        {
            Title = "Product A"
        };

        // Act
        var actionResult = await ProductController.UpdateProductById(createdProduct.Id, updateProductDto);

        // Assert
        Assert.True(actionResult is ConflictResult);
    }

    #endregion

    #region Delete By Id

    [Fact]
    public async Task Delete_ProductById_ShouldReturnOkResult()
    {
        // Arrange
        var createdCategory = await CategoryController.CreateCategoryTestHelper("Category A");
        var createdProduct = await ProductController.CreateProductTestHelper("Product A", createdCategory);

        // Act
        var actionResult = await ProductController.DeleteProductById(createdProduct.Id);

        // Assert
        Assert.True(actionResult is OkResult);
    }

    [Fact]
    public async Task Delete_ProductById_ShouldReturnNotFoundResult()
    {
        // Arrange
        var productId = Guid.NewGuid();

        // Act
        var actionResult = await ProductController.DeleteProductById(productId);

        // Assert
        Assert.True(actionResult is NotFoundResult);
    }

    #endregion
}