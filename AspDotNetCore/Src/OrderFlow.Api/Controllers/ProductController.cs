using Microsoft.AspNetCore.Mvc;
using OrderFlow.Api.Helpers;
using OrderFlow.Contracts.DTOs.Products;
using OrderFlow.Contracts.Interfaces.Services;

namespace OrderFlow.Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class ProductController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductController(IProductService productService)
    {
        _productService = productService;
    }

    /// <summary>Gets all products paginated</summary>
    /// <param name="page"></param>
    /// <param name="limit"></param>
    /// <response code="204">No Content</response>
    [HttpGet]
    [ProducesResponseType(typeof(GetProduct[]), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllProductsPaginated([FromQuery] int page = 0, [FromQuery] int limit = 12)
    {
        var products = await _productService.GetAllProductsPaginated();
        if (!products.Any())
            return NoContent();

        return Ok(products);
    }

    /// <summary>Gets a product by id</summary>
    /// <param name="productId"></param>
    /// <response code="404">Not Found</response>
    [HttpGet("{productId:guid}")]
    [ProducesResponseType(typeof(GetProduct), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetProductById([FromRoute] Guid productId)
    {
        var product = await _productService.GetProductById(productId);
        if (product is null)
            return NotFound();

        return Ok(product);
    }

    private readonly record struct AddProductResponseWrapper(Guid ProductId);

    /// <summary>Creates a product</summary>
    /// <param name="requestBody"></param>
    /// <response code="400">Bad Request</response>
    /// <response code="500">Internal Server Error</response>
    [HttpPost]
    [ProducesResponseType(typeof(AddProductResponseWrapper), StatusCodes.Status201Created)]
    public async Task<IActionResult> CreateProduct([FromBody] PostProduct requestBody)
    {
        var requestBodyValidationError = requestBody.Validate();
        if (requestBodyValidationError is not null)
            return requestBodyValidationError;

        var productId = await _productService.CreateProduct(requestBody);
        if (productId is null)
            return Problem();

        return Created(string.Empty, new AddProductResponseWrapper
        {
            ProductId = productId.Value
        });
    }

    /// <summary>Deletes a product by id</summary>
    /// <param name="productId"></param>
    /// <response code="200">Ok</response>
    /// <response code="404">Not Found</response>
    /// <response code="500">Internal Server Error</response>
    [HttpDelete("{productId:guid}")]
    public async Task<IActionResult> DeleteProductById([FromRoute] Guid productId)
    {
        var productExists = await _productService.GetProductById(productId);
        if (productExists is null)
            return NotFound();

        var wasProductDeleted = await _productService.DeleteProductById(productId);
        if (!wasProductDeleted)
            return Problem();

        return Ok();
    }

    /// <summary>Updates a product by id</summary>
    /// <param name="productId"></param>
    /// <param name="requestBody"></param>
    /// <response code="200">Ok</response>
    /// <response code="400">Bad Request</response>
    /// <response code="404">Not Found</response>
    /// <response code="500">Internal Server Error</response>
    [HttpPut("{productId:guid}")]
    public async Task<IActionResult> UpdateProductById([FromRoute] Guid productId, [FromBody] PutProduct requestBody)
    {
        var requestBodyValidationError = requestBody.Validate();
        if (requestBodyValidationError is not null)
            return requestBodyValidationError;

        var productExists = await _productService.GetProductById(productId);
        if (productExists is null)
            return NotFound();

        var wasProductUpdated = await _productService.UpdateProductById(productId, requestBody);
        if (!wasProductUpdated)
            return Problem();

        return Ok();
    }
}