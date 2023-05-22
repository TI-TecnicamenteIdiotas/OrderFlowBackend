using Microsoft.AspNetCore.Mvc;
using NimbleFlow.Contracts.DTOs.Tables;

namespace NimbleFlow.Api.Helpers;

public static class TableHelper
{
    public static IActionResult? Validate(this CreateTableDto tableDto)
    {
        if (tableDto.Accountable.Length > 50)
            return new BadRequestObjectResult($"{nameof(tableDto.Accountable)} length must be under 51 characters");

        return null;
    }

    public static IActionResult? Validate(this UpdateTableDto tableDto)
    {
        if (tableDto.Accountable?.Length > 50)
            return new BadRequestObjectResult($"{nameof(tableDto.Accountable)} length must be under 51 characters");

        return null;
    }
}