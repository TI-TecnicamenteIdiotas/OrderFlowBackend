using System.Net;
using Microsoft.AspNetCore.Mvc;
using NimbleFlow.Api.Services;
using NimbleFlow.Contracts.DTOs;
using NimbleFlow.Contracts.DTOs.Products;
using NimbleFlow.Data.Partials.DTOs;

namespace NimbleFlow.Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class ProductController : ControllerBase
{
    private const int MaxTitleLength = 64;
    private const int MaxDescriptionLength = 512;
    private readonly ProductService _productService;
    private readonly ProductHubService? _hubService;

    public ProductController(ProductService productService, ProductHubService? hubService)
    {
        _productService = productService;
        _hubService = hubService;
    }

    /// <summary>Creates a product</summary>
    /// <param name="requestBody"></param>
    /// <response code="400">Bad Request</response>
    /// <response code="409">Conflict</response>
    /// <response code="500">Internal Server Error</response>
    [HttpPost]
    [ProducesResponseType(typeof(ProductDto), StatusCodes.Status201Created)]
    public async Task<IActionResult> CreateProduct([FromBody] CreateProductDto requestBody)
    {
        if (string.IsNullOrWhiteSpace(requestBody.Title))
            return BadRequest($"{nameof(requestBody.Title)} must not be null or composed by white spaces only");

        if (requestBody.Title.Length > MaxTitleLength)
            return BadRequest($"{nameof(requestBody.Title)} must be under {MaxTitleLength + 1} characters");

        if (requestBody.Description?.Length > MaxDescriptionLength)
            return BadRequest($"{nameof(requestBody.Description)} must be under {MaxDescriptionLength + 1} characters");

        if (requestBody.Price < 0)
            return BadRequest($"{nameof(requestBody.Price)} must not be negative");

        var (responseStatus, response) = await _productService.Create(requestBody);
        switch (responseStatus)
        {
            case HttpStatusCode.Created when response is not null:
            {
                if (_hubService is not null)
                    await _hubService.PublishProductCreatedAsync(response);
                return Created(string.Empty, response);
            }
            case HttpStatusCode.BadRequest:
                return BadRequest();
            case HttpStatusCode.Conflict:
                return Conflict();
            default:
                return Problem();
        }
    }

    /// <summary>Gets all products paginated or filter all products by category id</summary>
    /// <param name="page"></param>
    /// <param name="limit"></param>
    /// <param name="includeDeleted"></param>
    /// <param name="categoryId"></param>
    /// <response code="204">No Content</response>
    [HttpGet]
    [ProducesResponseType(typeof(PaginatedDto<ProductDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllProductsPaginated(
        [FromQuery] int page = 0,
        [FromQuery] int limit = 12,
        [FromQuery] bool includeDeleted = false,
        [FromQuery] Guid? categoryId = null
    )
    {
        var (totalAmount, products) = categoryId switch
        {
            not null => await _productService.GetAllProductsPaginatedByCategoryId(
                page,
                limit,
                includeDeleted,
                categoryId.Value
            ),
            _ => await _productService.GetAllPaginated(page, limit, includeDeleted)
        };
        if (totalAmount == 0)
            return NoContent();

        var response = new PaginatedDto<ProductDto>(totalAmount, products);
        return Ok(response);
    }

    /// <summary>Gets products by ids</summary>
    /// <param name="productsIds"></param>
    /// <param name="includeDeleted"></param>
    /// <response code="404">Not Found</response>
    [HttpGet("by-ids")]
    [ProducesResponseType(typeof(ProductDto[]), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetProductsByIds(
        [FromBody] Guid[] productsIds,
        [FromQuery] bool includeDeleted = false
    )
    {
        var response = await _productService.GetManyById(productsIds, includeDeleted);
        if (!response.Any())
            return NotFound();

        return Ok(response);
    }

    /// <summary>Gets a product by id</summary>
    /// <param name="productId"></param>
    /// <response code="404">Not Found</response>
    [HttpGet("{productId:guid}")]
    [ProducesResponseType(typeof(ProductDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetProductById([FromRoute] Guid productId)
    {
        var response = await _productService.GetById(productId);
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
    /// <response code="409">Conflict</response>
    /// <response code="500">Internal Server Error</response>
    [HttpPut("{productId:guid}")]
    public async Task<IActionResult> UpdateProductById(
        [FromRoute] Guid productId,
        [FromBody] UpdateProductDto requestBody
    )
    {
        if (requestBody.Title is not null && string.IsNullOrWhiteSpace(requestBody.Title))
            return BadRequest($"{nameof(requestBody.Title)} must not be composed by white spaces only");

        if (requestBody.Title is not null && requestBody.Title?.Length > MaxTitleLength)
            return BadRequest($"{nameof(requestBody.Title)} must be under {MaxTitleLength + 1} characters");

        if (requestBody.Description?.Length > MaxDescriptionLength)
            return BadRequest($"{nameof(requestBody.Description)} must be under {MaxDescriptionLength + 1} characters");

        if (requestBody.Price < 0)
            return BadRequest($"{nameof(requestBody.Price)} must not be negative");

        var (responseStatus, response) = await _productService.UpdateProductById(productId, requestBody);
        switch (responseStatus)
        {
            case HttpStatusCode.OK:
            {
                if (_hubService is not null && response is not null)
                    await _hubService.PublishProductUpdatedAsync(response);
                return Ok();
            }
            case HttpStatusCode.NotModified:
                return StatusCode((int)HttpStatusCode.NotModified);
            case HttpStatusCode.NotFound:
                return NotFound();
            case HttpStatusCode.Conflict:
                return Conflict();
            default:
                return Problem();
        }
    }

    /// <summary>Deletes products by ids</summary>
    /// <param name="productsIds"></param>
    /// <response code="200">Ok</response>
    /// <response code="404">Not Found</response>
    [HttpDelete("by-ids")]
    public async Task<IActionResult> DeleteProductsByIds([FromBody] Guid[] productsIds)
    {
        var response = await _productService.DeleteManyByIds(productsIds);
        if (!response)
            return NotFound();

        if (_hubService is not null)
            await _hubService.PublishManyProductsDeletedAsync(productsIds);

        return Ok();
    }

    /// <summary>Deletes a product by id</summary>
    /// <param name="productId"></param>
    /// <response code="200">Ok</response>
    /// <response code="404">Not Found</response>
    /// <response code="500">Internal Server Error</response>
    [HttpDelete("{productId:guid}")]
    public async Task<IActionResult> DeleteProductById([FromRoute] Guid productId)
    {
        var responseStatus = await _productService.DeleteById(productId);
        switch (responseStatus)
        {
            case HttpStatusCode.OK:
            {
                if (_hubService is not null)
                    await _hubService.PublishProductDeletedAsync(productId);
                return Ok();
            }
            case HttpStatusCode.NotFound:
                return NotFound();
            default:
                return Problem();
        }
    }
}