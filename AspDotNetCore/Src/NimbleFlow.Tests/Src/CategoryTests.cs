using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NimbleFlow.Api.Controllers;
using NimbleFlow.Contracts.DTOs.Categories;
using NimbleFlow.Tests.Base;
using NimbleFlow.Tests.Helpers;

namespace NimbleFlow.Tests;

public class CategoryTests : IClassFixture<TestBase>
{
    private readonly CategoryController _categoryController;

    public CategoryTests(TestBase testBase)
    {
        _categoryController = testBase.CategoryController;
    }

    [Theory]
    [InlineData("Category A")]
    [InlineData("Category B")]
    [InlineData("Category C")]
    public async Task Create_Category_ShouldReturnCreatedResult(string categoryTitle)
    {
        // Arrange
        var categoryDto = new CreateCategoryDto
        {
            Title = categoryTitle
        };

        // Act
        var actionResult = await _categoryController.CreateCategory(categoryDto);

        // Assert
        Assert.True(actionResult is CreatedResult);
    }

    [Fact]
    public async Task GetAll_CategoriesPaginated_ShouldReturnOkObjectResult()
    {
        // Arrange
        // Act
        var actionResult = await _categoryController.GetAllCategoriesPaginated();

        // Assert
        Assert.True(actionResult is OkObjectResult);
    }

    [Fact]
    public async Task Get_CategoryById_ShouldReturnOkObjectResult()
    {
        // Arrange
        var createdCategory = await _categoryController.CreateCategoryTestHelper("Category D");

        // Act
        var actionResult = await _categoryController.GetCategoryById(createdCategory.Id);

        // Assert
        Assert.True(actionResult is OkObjectResult);
    }

    [Fact]
    public async Task Update_CategoryById_ShouldReturnStatusCodeResult_WithStatusCode200()
    {
        // Arrange
        var createdCategory = await _categoryController.CreateCategoryTestHelper("Category E");
        var updateCategoryDto = new UpdateCategoryDto
        {
            Title = "Category E Updated"
        };

        // Act
        var actionResult = await _categoryController.UpdateCategoryById(createdCategory.Id, updateCategoryDto);
        var statusCodeResult = actionResult as StatusCodeResult;

        // Assert
        Assert.True(statusCodeResult?.StatusCode is StatusCodes.Status200OK);
    }

    [Fact]
    public async Task Delete_CategoryById_ShouldReturnStatusCodeResult_WithStatusCode200()
    {
        // Arrange
        var createdCategory = await _categoryController.CreateCategoryTestHelper("Category F");

        // Act
        var actionResult = await _categoryController.DeleteCategoryById(createdCategory.Id);
        var statusCodeResult = actionResult as StatusCodeResult;

        // Assert
        Assert.True(statusCodeResult?.StatusCode is StatusCodes.Status200OK);
    }
}