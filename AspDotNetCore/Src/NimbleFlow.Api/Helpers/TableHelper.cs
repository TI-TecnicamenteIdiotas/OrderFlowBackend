using Microsoft.AspNetCore.Mvc;
using NimbleFlow.Contracts.DTOs.Tables;

namespace NimbleFlow.Api.Helpers;

public static class TableHelper
{
    public static IActionResult? Validate(this PostTable table)
    {
        if (table.Name.Length > 50)
            return new BadRequestObjectResult($"{nameof(table.Name)} length must be under 51 characters");

        return null;
    }

    public static IActionResult? Validate(this PutTable table)
    {
        if (table.Name?.Length > 50)
            return new BadRequestObjectResult($"{nameof(table.Name)} length must be under 51 characters");

        if (table.PaidValue < 0)
            return new BadRequestObjectResult($"{nameof(table.PaidValue)} must not be negative");

        if (table.Items?.Any(item => item.Product.Price * item.Count + item.Additional - item.Discount < 0) ?? false)
            return new BadRequestObjectResult($"{nameof(table.Items)} must not contain a negative total value");

        return null;
    }
}