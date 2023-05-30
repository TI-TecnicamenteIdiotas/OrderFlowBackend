using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NimbleFlow.Api.Controllers;
using NimbleFlow.Contracts.DTOs.Categories;
using NimbleFlow.Tests.Base;
using NimbleFlow.Tests.Helpers;

namespace NimbleFlow.Tests;

public class CategoryTests : TestBase
{
    private readonly CategoryController _categoryController;

    public CategoryTests()
    {
        _categoryController = CategoryController;
    }

    #region Create

    [Theory]
    [InlineData("Category A")]
    [InlineData("Category B")]
    [InlineData("Category C")]
    public async Task Create_Category_ShouldReturnCreatedResult(string title)
    {
        // Arrange
        var categoryDto = new CreateCategoryDto
        {
            Title = title
        };

        // Act
        var actionResult = await _categoryController.CreateCategory(categoryDto);

        // Assert
        Assert.True(actionResult is CreatedResult);
    }

    #endregion

    #region Get All Paginated

    [Fact]
    public async Task GetAll_CategoriesPaginated_ShouldReturnOkObjectResult()
    {
        // Arrange
        _ = await _categoryController.CreateCategoryTestHelper("Category A");

        // Act
        var actionResult = await _categoryController.GetAllCategoriesPaginated();

        // Assert
        Assert.True(actionResult is OkObjectResult);
    }

    #endregion

    #region By Id

    [Fact]
    public async Task Get_CategoryById_ShouldReturnOkObjectResult()
    {
        // Arrange
        var createdCategory = await _categoryController.CreateCategoryTestHelper("Category A");

        // Act
        var actionResult = await _categoryController.GetCategoryById(createdCategory.Id);

        // Assert
        Assert.True(actionResult is OkObjectResult);
    }

    #endregion

    #region Update By Id

    [Fact]
    public async Task Update_CategoryById_ShouldReturnOkResult()
    {
        // Arrange
        var createdCategory = await _categoryController.CreateCategoryTestHelper("Category A");
        var updateCategoryDto = new UpdateCategoryDto
        {
            Title = "Category A Updated"
        };

        // Act
        var actionResult = await _categoryController.UpdateCategoryById(createdCategory.Id, updateCategoryDto);

        // Assert
        Assert.True(actionResult is OkResult);
    }

    [Fact]
    public async Task Update_CategoryById_ShouldReturnConflictResult()
    {
        // Arrange
        _ = await _categoryController.CreateCategoryTestHelper("Category A");
        var createdCategory = await _categoryController.CreateCategoryTestHelper("Category B");
        var updateCategoryDto = new UpdateCategoryDto
        {
            Title = "Category A"
        };

        // Act
        var actionResult = await _categoryController.UpdateCategoryById(createdCategory.Id, updateCategoryDto);

        // Assert
        Assert.True(actionResult is ConflictResult);
    }

    #endregion

    #region Delete By Id

    [Fact]
    public async Task Delete_CategoryById_ShouldReturnOkResult()
    {
        // Arrange
        var createdCategory = await _categoryController.CreateCategoryTestHelper("Category A");

        // Act
        var actionResult = await _categoryController.DeleteCategoryById(createdCategory.Id);

        // Assert
        Assert.True(actionResult is OkResult);
    }

    #endregion
}