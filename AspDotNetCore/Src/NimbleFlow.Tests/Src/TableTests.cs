using Microsoft.AspNetCore.Mvc;
using NimbleFlow.Contracts.DTOs.Tables;
using NimbleFlow.Tests.Base;
using NimbleFlow.Tests.Helpers;

namespace NimbleFlow.Tests;

public class TableTests : TestBase
{
    #region Create

    [Fact]
    public async Task Create_Table_ShouldReturnCreatedResult()
    {
        // Arrange
        var tableDto = new CreateTableDto("Accountable A")
        {
            IsFullyPaid = false
        };

        // Act
        var actionResult = await TableController.CreateTable(tableDto);

        // Assert
        Assert.True(actionResult is CreatedResult);
    }

    #endregion

    #region Get All Paginated

    [Fact]
    public async Task GetAll_TablesPaginated_ShouldReturnOkObjectResult()
    {
        // Arrange
        _ = await TableController.CreateTableTestHelper("Table A");

        // Act
        var actionResult = await TableController.GetAllTablesPaginated();

        // Assert
        Assert.True(actionResult is OkObjectResult);
    }

    [Fact]
    public async Task GetAll_TablesPaginated_ShouldReturnNoContentResult()
    {
        // Arrange
        // Act
        var actionResult = await TableController.GetAllTablesPaginated();

        // Assert
        Assert.True(actionResult is NoContentResult);
    }

    #endregion

    #region By Id

    [Fact]
    public async Task Get_TableById_ShouldReturnOkObjectResult()
    {
        // Arrange
        var createdTable = await TableController.CreateTableTestHelper("Table A");

        // Act
        var actionResult = await TableController.GetTableById(createdTable.Id);

        // Assert
        Assert.True(actionResult is OkObjectResult);
    }

    [Fact]
    public async Task Get_TableById_ShouldReturnNotFound()
    {
        // Arrange
        var tableId = Guid.NewGuid();

        // Act
        var actionResult = await TableController.GetTableById(tableId);

        // Assert
        Assert.True(actionResult is NotFoundResult);
    }

    #endregion

    #region Update By Id

    [Fact]
    public async Task Update_TableById_ShouldReturnOkResult()
    {
        // Arrange
        var createdTable = await TableController.CreateTableTestHelper("Table A");
        var updateTableDto = new UpdateTableDto
        {
            Accountable = "Table A Updated"
        };

        // Act
        var actionResult = await TableController.UpdateTableById(createdTable.Id, updateTableDto);

        // Assert
        Assert.True(actionResult is OkResult);
    }

    #endregion

    #region Delete By Id

    [Fact]
    public async Task Delete_TableById_ShouldReturnOkResult()
    {
        // Arrange
        var createdTable = await TableController.CreateTableTestHelper("Table A");

        // Act
        var actionResult = await TableController.DeleteTableById(createdTable.Id);

        // Assert
        Assert.True(actionResult is OkResult);
    }

    [Fact]
    public async Task Delete_TableById_ShouldReturnNotFoundResult()
    {
        // Arrange
        var tableId = Guid.NewGuid();

        // Act
        var actionResult = await TableController.DeleteTableById(tableId);

        // Assert
        Assert.True(actionResult is NotFoundResult);
    }

    #endregion
}