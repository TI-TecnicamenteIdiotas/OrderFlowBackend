using Microsoft.AspNetCore.Mvc;
using NimbleFlow.Api.Controllers;
using NimbleFlow.Contracts.DTOs.Categories;
using NimbleFlow.Tests.Base;
using NimbleFlow.Tests.Helpers;

namespace NimbleFlow.Tests;

public class CategoryTests : TestBase
{
    #region Create

    [Fact]
    public async Task Create_Category_ShouldReturnCreatedResult()
    {
        // Arrange
        var categoryDto = new CreateCategoryDto("Category A");

        // Act
        var actionResult = await CategoryController.CreateCategory(categoryDto);

        // Assert
        Assert.True(actionResult is CreatedResult);
    }

    #endregion

    #region Get All Paginated

    [Fact]
    public async Task GetAll_CategoriesPaginated_ShouldReturnOkObjectResult()
    {
        // Arrange
        _ = await CategoryController.CreateCategoryTestHelper("Category A");

        // Act
        var actionResult = await CategoryController.GetAllCategoriesPaginated();

        // Assert
        Assert.True(actionResult is OkObjectResult);
    }

    [Fact]
    public async Task GetAll_CategoriesPaginated_ShouldReturnNoContentResult()
    {
        // Arrange
        // Act
        var actionResult = await CategoryController.GetAllCategoriesPaginated();

        // Assert
        Assert.True(actionResult is NoContentResult);
    }

    #endregion

    #region Get By Ids

    [Fact]
    public async Task Get_CategoriesByIds_ShouldReturnOkObjectResult()
    {
        // Arrange
        var createdCategories = await CategoryController.CreateManyCategoriesTestHelper(
            "Category A",
            "Category B",
            "Category C"
        );
        var categoriesIds = createdCategories.Select(x => x.Id).ToArray();

        // Act
        var actionResult = await CategoryController.GetCategoriesByIds(categoriesIds);

        // Assert
        Assert.True(actionResult is OkObjectResult);
    }

    [Fact]
    public async Task Get_CategoriesByIds_ShouldReturnNotFoundResult()
    {
        // Arrange
        var categoriesIds = new[]
        {
            Guid.NewGuid(),
            Guid.NewGuid()
        };

        // Act
        var actionResult = await CategoryController.GetCategoriesByIds(categoriesIds);

        // Assert
        Assert.True(actionResult is NotFoundResult);
    }

    #endregion

    #region Get By Id

    [Fact]
    public async Task Get_CategoryById_ShouldReturnOkObjectResult()
    {
        // Arrange
        var createdCategory = await CategoryController.CreateCategoryTestHelper("Category A");

        // Act
        var actionResult = await CategoryController.GetCategoryById(createdCategory.Id);

        // Assert
        Assert.True(actionResult is OkObjectResult);
    }

    [Fact]
    public async Task Get_CategoryById_ShouldReturnNotFound()
    {
        // Arrange
        var categoryId = Guid.NewGuid();

        // Act
        var actionResult = await CategoryController.GetCategoryById(categoryId);

        // Assert
        Assert.True(actionResult is NotFoundResult);
    }

    #endregion

    #region Update By Id

    [Fact]
    public async Task Update_CategoryById_ShouldReturnOkResult()
    {
        // Arrange
        var createdCategory = await CategoryController.CreateCategoryTestHelper("Category A");
        var updateCategoryDto = new UpdateCategoryDto
        {
            Title = "Category A Updated"
        };

        // Act
        var actionResult = await CategoryController.UpdateCategoryById(createdCategory.Id, updateCategoryDto);

        // Assert
        Assert.True(actionResult is OkResult);
    }

    [Fact]
    public async Task Update_CategoryById_ShouldReturnConflictResult()
    {
        // Arrange
        _ = await CategoryController.CreateCategoryTestHelper("Category A");
        var createdCategory = await CategoryController.CreateCategoryTestHelper("Category B");
        var updateCategoryDto = new UpdateCategoryDto
        {
            Title = "Category A"
        };

        // Act
        var actionResult = await CategoryController.UpdateCategoryById(createdCategory.Id, updateCategoryDto);

        // Assert
        Assert.True(actionResult is ConflictResult);
    }

    #endregion

    #region Delete By Ids

    [Fact]
    public async Task Delete_CategoriesByIds_ShouldReturnOkResult()
    {
        // Arrange
        var createdCategories = await CategoryController.CreateManyCategoriesTestHelper(
            "Category A",
            "Category B",
            "Category C"
        );
        var categoriesIds = createdCategories.Select(x => x.Id).ToArray();

        // Act
        var actionResult = await CategoryController.DeleteCategoriesByIds(categoriesIds);

        // Assert
        Assert.True(actionResult is OkObjectResult);
    }

    [Fact]
    public async Task Delete_CategoriesByIds_ShouldReturnNotFoundResult()
    {
        // Arrange
        var categoriesIds = new[]
        {
            Guid.NewGuid(),
            Guid.NewGuid()
        };

        // Act
        var actionResult = await CategoryController.DeleteCategoriesByIds(categoriesIds);

        // Assert
        Assert.True(actionResult is NotFoundResult);
    }

    #endregion

    #region Delete By Id

    [Fact]
    public async Task Delete_CategoryById_ShouldReturnOkResult()
    {
        // Arrange
        var createdCategory = await CategoryController.CreateCategoryTestHelper("Category A");

        // Act
        var actionResult = await CategoryController.DeleteCategoryById(createdCategory.Id);

        // Assert
        Assert.True(actionResult is OkResult);
    }

    [Fact]
    public async Task Delete_CategoryById_ShouldReturnNotFoundResult()
    {
        // Arrange
        var categoryId = Guid.NewGuid();

        // Act
        var actionResult = await CategoryController.DeleteCategoryById(categoryId);

        // Assert
        Assert.True(actionResult is NotFoundResult);
    }

    #endregion
}