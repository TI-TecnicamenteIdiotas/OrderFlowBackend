using Microsoft.AspNetCore.Mvc;
using NimbleFlow.Api.Helpers;
using NimbleFlow.Api.Services;
using NimbleFlow.Contracts.DTOs.Products;

namespace NimbleFlow.Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class ProductController : ControllerBase
{
    private readonly ProductService _productService;

    public ProductController(ProductService productService)
    {
        _productService = productService;
    }

    /// <summary>Creates a product</summary>
    /// <param name="requestBody"></param>
    /// <response code="400">Bad Request</response>
    /// <response code="500">Internal Server Error</response>
    [HttpPost]
    [ProducesResponseType(typeof(ProductDto), StatusCodes.Status201Created)]
    public async Task<IActionResult> CreateProduct([FromBody] CreateProductDto requestBody)
    {
        var validationError = requestBody.Validate();
        if (validationError is not null)
            return validationError;

        var response = await _productService.CreateProduct(requestBody);
        if (response is null)
            return Problem();

        return Created(string.Empty, response);
    }

    /// <summary>Gets all products paginated</summary>
    /// <param name="page"></param>
    /// <param name="limit"></param>
    /// <param name="includeDeleted"></param>
    /// <response code="204">No Content</response>
    [HttpGet]
    [ProducesResponseType(typeof(ProductDto[]), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllProductsPaginated(
        [FromQuery] int page = 0,
        [FromQuery] int limit = 12,
        [FromQuery] bool includeDeleted = false
    )
    {
        var response = await _productService.GetAllProductsPaginated(page, limit, includeDeleted);
        if (!response.Any())
            return NoContent();

        return Ok(response);
    }

    /// <summary>Gets a product by id</summary>
    /// <param name="productId"></param>
    /// <response code="404">Not Found</response>
    [HttpGet("{productId:guid}")]
    [ProducesResponseType(typeof(ProductDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetProductById([FromRoute] Guid productId)
    {
        var response = await _productService.GetProductById(productId);
        if (response is null)
            return NotFound();

        return Ok(response);
    }

    /// <summary>Updates a product by id</summary>
    /// <param name="productId"></param>
    /// <param name="requestBody"></param>
    /// <response code="200">Ok</response>
    /// <response code="304">Not Modified</response>
    /// <response code="400">Bad Request</response>
    /// <response code="404">Not Found</response>
    /// <response code="500">Internal Server Error</response>
    [HttpPut("{productId:guid}")]
    public async Task<IActionResult> UpdateProductById(
        [FromRoute] Guid productId,
        [FromBody] UpdateProductDto requestBody
    )
    {
        var validationError = requestBody.Validate();
        if (validationError is not null)
            return validationError;

        var responseStatus = await _productService.UpdateProductById(productId, requestBody);
        return StatusCode((int)responseStatus);
    }

    /// <summary>Deletes a product by id</summary>
    /// <param name="productId"></param>
    /// <response code="200">Ok</response>
    /// <response code="404">Not Found</response>
    /// <response code="500">Internal Server Error</response>
    [HttpDelete("{productId:guid}")]
    public async Task<IActionResult> DeleteProductById([FromRoute] Guid productId)
    {
        var responseStatus = await _productService.DeleteEntityById(productId);
        return StatusCode((int)responseStatus);
    }
}