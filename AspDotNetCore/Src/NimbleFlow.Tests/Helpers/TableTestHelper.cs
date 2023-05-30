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
        var tableDto = new CreateTableDto
        {
            Accountable = tableAccountable,
            IsFullyPaid = false
        };

        var createTableResponse = await tableController.CreateTable(tableDto);
        var createdTable = ((createTableResponse as CreatedResult)!.Value as TableDto)!;
        return createdTable;
    }
}