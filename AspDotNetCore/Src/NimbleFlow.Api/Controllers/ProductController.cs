using System.Net;
using Microsoft.AspNetCore.Mvc;
using NimbleFlow.Api.Services;
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

    public ProductController(ProductService productService)
    {
        _productService = productService;
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
        return responseStatus switch
        {
            HttpStatusCode.Created => Created(string.Empty, response),
            HttpStatusCode.BadRequest => BadRequest(),
            HttpStatusCode.Conflict => Conflict(),
            _ => Problem()
        };
    }

    /// <summary>Gets all products paginated or filter all products by category id</summary>
    /// <param name="page"></param>
    /// <param name="limit"></param>
    /// <param name="includeDeleted"></param>
    /// <param name="categoryId"></param>
    /// <response code="204">No Content</response>
    [HttpGet]
    [ProducesResponseType(typeof(ProductDto[]), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllProductsPaginated(
        [FromQuery] int page = 0,
        [FromQuery] int limit = 12,
        [FromQuery] bool includeDeleted = false,
        [FromQuery] Guid? categoryId = null
    )
    {
        var response = categoryId switch
        {
            not null => await _productService.GetAllProductsPaginatedByCategoryId(
                page,
                limit,
                includeDeleted,
                categoryId.Value
            ),
            _ => await _productService.GetAllPaginated(page, limit, includeDeleted)
        };
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

        var responseStatus = await _productService.UpdateProductById(productId, requestBody);
        return responseStatus switch
        {
            HttpStatusCode.OK => Ok(),
            HttpStatusCode.NotModified => StatusCode((int)HttpStatusCode.NotModified),
            HttpStatusCode.NotFound => NotFound(),
            HttpStatusCode.Conflict => Conflict(),
            _ => Problem()
        };
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
        return responseStatus switch
        {
            HttpStatusCode.OK => Ok(),
            HttpStatusCode.NotFound => NotFound(),
            _ => Problem()
        };
    }
}