using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using NimbleFlow.Api.Helpers;
using NimbleFlow.Api.Services;
using NimbleFlow.Contracts.DTOs.Orders;

namespace NimbleFlow.Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class OrderController : ControllerBase
{
    private readonly OrderService _orderService;
    private readonly ProductService _productService;

    public OrderController(OrderService orderService, ProductService productService)
    {
        _orderService = orderService;
        _productService = productService;
    }

    /// <summary>Creates a order</summary>
    /// <param name="requestBody"></param>
    /// <response code="400">Bad Request</response>
    /// <response code="500">Internal Server Error</response>
    [HttpPost]
    [ProducesResponseType(typeof(OrderDto), StatusCodes.Status201Created, MediaTypeNames.Application.Json)]
    public async Task<IActionResult> CreateOrder([FromBody] CreateOrderDto requestBody)
    {
        var validationError = requestBody.Validate();
        if (validationError is not null)
            return validationError;

        var response = await _orderService.CreateOrder(requestBody);
        if (response is null)
            return Problem();

        return Created(string.Empty, response);
    }

    /// <summary>Gets all orders paginated</summary>
    /// <param name="page"></param>
    /// <param name="limit"></param>
    /// <param name="includeDeleted"></param>
    /// <response code="204">No Content</response>
    [HttpGet]
    [ProducesResponseType(typeof(OrderDto[]), StatusCodes.Status200OK, MediaTypeNames.Application.Json)]
    public async Task<IActionResult> GetAllOrdersPaginated(
        [FromQuery] int page = 0,
        [FromQuery] int limit = 12,
        [FromQuery] bool includeDeleted = false
    )
    {
        var response = await _orderService.GetAllOrdersPaginated(page, limit, includeDeleted);
        if (!response.Any())
            return NoContent();

        return Ok(response);
    }

    /// <summary>Gets a order by id</summary>
    /// <param name="orderId"></param>
    /// <param name="includeDeleted"></param>
    /// <response code="404">Not Found</response>
    [HttpGet("{orderId:guid}")]
    [ProducesResponseType(typeof(OrderWithRelationsDto), StatusCodes.Status200OK, MediaTypeNames.Application.Json)]
    public async Task<IActionResult> GetOrderById([FromRoute] Guid orderId, [FromQuery] bool includeDeleted = false)
    {
        var response = await _orderService.GetOrderWithRelationsById(orderId, includeDeleted);
        if (response is null)
            return NotFound();

        return Ok(response);
    }

    /// <summary>Updates a order by id</summary>
    /// <param name="orderId"></param>
    /// <param name="requestBody"></param>
    /// <response code="200">Ok</response>
    /// <response code="304">Not Modified</response>
    /// <response code="400">Bad Request</response>
    /// <response code="404">Not Found</response>
    /// <response code="500">Internal Server Error</response>
    [HttpPut("{orderId:guid}")]
    [Consumes(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(OrderDto), StatusCodes.Status200OK, MediaTypeNames.Application.Json)]
    public async Task<IActionResult> UpdateOrderById([FromRoute] Guid orderId, [FromBody] UpdateOrderDto requestBody)
    {
        var validationError = requestBody.Validate();
        if (validationError is not null)
            return validationError;

        var responseStatus = await _orderService.UpdateOrderById(orderId, requestBody);
        return StatusCode((int)responseStatus);
    }

    /// <summary>Deletes a order by id</summary>
    /// <param name="orderId"></param>
    /// <response code="200">Ok</response>
    /// <response code="404">Not Found</response>
    /// <response code="500">Internal Server Error</response>
    [HttpDelete("{orderId:guid}")]
    public async Task<IActionResult> DeleteOrderById([FromRoute] Guid orderId)
    {
        var responseStatus = await _orderService.DeleteEntityById(orderId);
        return StatusCode((int)responseStatus);
    }

    /// <summary>Adds a product to a order by order id</summary>
    /// <param name="orderId"></param>
    /// <param name="requestBody"></param>
    /// <response code="200">Ok</response>
    /// <response code="409">Conflict</response>
    [HttpPost("{orderId:guid}")]
    public async Task<IActionResult> AddProductToOrder(
        [FromRoute] Guid orderId,
        [FromBody] CreateOrderProductDto requestBody
    )
    {
        var response = await _orderService.AddProductToOrderByOrderId(orderId, requestBody);
        if (!response)
            return Conflict();

        return Ok();
    }

    /// <summary>Removes a product from a order by ids</summary>
    /// <param name="orderId"></param>
    /// <param name="productId"></param>
    /// <response code="200">Ok</response>
    /// <response code="404">Not Found</response>
    /// <response code="409">Conflict</response>
    [HttpDelete("{orderId:guid}/{productId:guid}")]
    public async Task<IActionResult> RemoveProductFromOrder([FromRoute] Guid orderId, [FromRoute] Guid productId)
    {
        var validationError = await OrderHelper.Validate(_productService, productId);
        if (validationError is not null)
            return validationError;

        var response = await _orderService.RemoveProductFromOrderByIds(orderId, productId);
        if (!response)
            return NotFound();

        return Ok();
    }
}