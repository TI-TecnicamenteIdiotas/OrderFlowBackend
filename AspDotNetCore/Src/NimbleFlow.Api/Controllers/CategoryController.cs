using System.Net;
using Microsoft.AspNetCore.Mvc;
using NimbleFlow.Api.Services;
using NimbleFlow.Contracts.DTOs;
using NimbleFlow.Contracts.DTOs.Categories;
using NimbleFlow.Data.Partials.DTOs;

namespace NimbleFlow.Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class CategoryController : ControllerBase
{
    private const int MaxTitleLength = 32;
    private readonly CategoryService _categoryService;

    public CategoryController(CategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    /// <summary>Creates a category</summary>
    /// <param name="requestBody"></param>
    /// <response code="400">Bad Request</response>
    /// <response code="409">Conflict</response>
    /// <response code="500">Internal Server Error</response>
    [HttpPost]
    [ProducesResponseType(typeof(CategoryDto), StatusCodes.Status201Created)]
    public async Task<IActionResult> CreateCategory([FromBody] CreateCategoryDto requestBody)
    {
        if (string.IsNullOrWhiteSpace(requestBody.Title))
            return BadRequest($"{nameof(requestBody.Title)} must not be null or composed by white spaces only");

        if (requestBody.Title.Length > MaxTitleLength)
            return BadRequest($"{nameof(requestBody.Title)} must be under {MaxTitleLength + 1} characters");

        if (requestBody.ColorTheme < 0)
            return BadRequest($"{nameof(requestBody.ColorTheme)} must be positive");

        if (requestBody.CategoryIcon < 0)
            return BadRequest($"{nameof(requestBody.CategoryIcon)} must be positive");

        var (responseStatus, response) = await _categoryService.Create(requestBody);
        return responseStatus switch
        {
            HttpStatusCode.Created => Created(string.Empty, response),
            HttpStatusCode.Conflict => Conflict(),
            _ => Problem()
        };
    }

    /// <summary>Gets all categories paginated</summary>
    /// <param name="page"></param>
    /// <param name="limit"></param>
    /// <param name="includeDeleted"></param>
    /// <response code="204">No Content</response>
    [HttpGet]
    [ProducesResponseType(typeof(PaginatedDto<CategoryDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllCategoriesPaginated(
        [FromQuery] int page = 0,
        [FromQuery] int limit = 12,
        [FromQuery] bool includeDeleted = false
    )
    {
        var (totalAmount, categories) = await _categoryService.GetAllPaginated(page, limit, includeDeleted);
        if (totalAmount == 0)
            return NoContent();

        var response = new PaginatedDto<CategoryDto>(totalAmount, categories);
        return Ok(response);
    }

    /// <summary>Gets a category by id</summary>
    /// <param name="categoryId"></param>
    /// <response code="404">Not Found</response>
    [HttpGet("{categoryId:guid}")]
    [ProducesResponseType(typeof(CategoryDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetCategoryById([FromRoute] Guid categoryId)
    {
        var response = await _categoryService.GetById(categoryId);
        if (response is null)
            return NotFound();

        return Ok(response);
    }

    /// <summary>Updates a category by id</summary>
    /// <param name="categoryId"></param>
    /// <param name="requestBody"></param>
    /// <response code="200">Ok</response>
    /// <response code="304">Not Modified</response>
    /// <response code="400">Bad Request</response>
    /// <response code="404">Not Found</response>
    /// <response code="409">Conflict</response>
    /// <response code="500">Internal Server Error</response>
    [HttpPut("{categoryId:guid}")]
    public async Task<IActionResult> UpdateCategoryById(
        [FromRoute] Guid categoryId,
        [FromBody] UpdateCategoryDto requestBody
    )
    {
        if (string.IsNullOrWhiteSpace(requestBody.Title))
            return BadRequest($"{nameof(requestBody.Title)} must not be null or composed by white spaces only");

        if (requestBody.Title.Length > MaxTitleLength)
            return BadRequest($"{nameof(requestBody.Title)} length must be under {MaxTitleLength + 1} characters");

        if (requestBody.ColorTheme < 0)
            return BadRequest($"{nameof(requestBody.ColorTheme)} must be positive");

        if (requestBody.CategoryIcon < 0)
            return BadRequest($"{nameof(requestBody.CategoryIcon)} must be positive");

        var responseStatus = await _categoryService.UpdateCategoryById(categoryId, requestBody);
        return responseStatus switch
        {
            HttpStatusCode.OK => Ok(),
            HttpStatusCode.NotModified => StatusCode((int)HttpStatusCode.NotModified),
            HttpStatusCode.NotFound => NotFound(),
            HttpStatusCode.Conflict => Conflict(),
            _ => Problem()
        };
    }

    /// <summary>Deletes a category by id</summary>
    /// <param name="categoryId"></param>
    /// <response code="200">Ok</response>
    /// <response code="404">Not Found</response>
    /// <response code="500">Internal Server Error</response>
    [HttpDelete("{categoryId:guid}")]
    public async Task<IActionResult> DeleteCategoryById([FromRoute] Guid categoryId)
    {
        var responseStatus = await _categoryService.DeleteEntityById(categoryId);
        return responseStatus switch
        {
            HttpStatusCode.OK => Ok(),
            HttpStatusCode.NotFound => NotFound(),
            _ => Problem()
        };
    }
}