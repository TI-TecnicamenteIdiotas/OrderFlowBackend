using Microsoft.AspNetCore.Mvc;
using NimbleFlow.Contracts.DTOs.Tables;

namespace NimbleFlow.Api.Helpers;

public static class TableHelper
{
    public static IActionResult? Validate(this PostTable table)
    {
        if (table.Accountable.Length > 50)
            return new BadRequestObjectResult($"{nameof(table.Accountable)} length must be under 51 characters");

        return null;
    }

    public static IActionResult? Validate(this PutTable table)
    {
        if (table.Accountable?.Length > 50)
            return new BadRequestObjectResult($"{nameof(table.Accountable)} length must be under 51 characters");

        return null;
    }
}