using Microsoft.AspNetCore.Mvc;
using NimbleFlow.Api.Services;
using NimbleFlow.Contracts.DTOs.Orders;

namespace NimbleFlow.Api.Helpers;

public static class OrderHelper
{
    public static IActionResult? Validate(this CreateOrderDto orderDto)
    {
        return null;
    }

    public static IActionResult? Validate(this UpdateOrderDto orderDto)
    {
        return null;
    }

    public static async Task<IActionResult?> Validate(ProductService productService, Guid productId)
    {
        if (!await productService.ExistsById(productId))
            return new NotFoundResult();

        return null;
    }
}