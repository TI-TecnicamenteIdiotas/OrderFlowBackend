using Microsoft.AspNetCore.Mvc;
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
}