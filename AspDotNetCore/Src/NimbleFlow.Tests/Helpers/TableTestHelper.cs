using Microsoft.AspNetCore.Mvc;
using NimbleFlow.Api.Controllers;
using NimbleFlow.Contracts.DTOs.Tables;
using NimbleFlow.Data.Partials.DTOs;

namespace NimbleFlow.Tests.Helpers;

internal static class TableTestHelper
{
    internal static async Task<TableDto> CreateTableTestHelper(
        this TableController tableController,
        string tableAccountable
    )
    {
        var tableDto = new CreateTableDto(tableAccountable)
        {
            IsFullyPaid = false
        };

        var createTableResponse = await tableController.CreateTable(tableDto);
        var createdTable = ((createTableResponse as CreatedResult)!.Value as TableDto)!;
        return createdTable;
    }

    internal static async Task<TableDto[]> CreateManyTablesTestHelper(
        this TableController tableController,
        params string[] tableAccountabilities
    )
    {
        var tablesDto = tableAccountabilities.Select(x => new CreateTableDto(x)).ToArray();
        var createTableResponses = new List<IActionResult>();
        foreach (var tableDto in tablesDto)
        {
            var response = await tableController.CreateTable(tableDto);
            createTableResponses.Add(response);
        }

        return createTableResponses.Select(x => ((x as CreatedResult)!.Value as TableDto)!).ToArray();
    }
}