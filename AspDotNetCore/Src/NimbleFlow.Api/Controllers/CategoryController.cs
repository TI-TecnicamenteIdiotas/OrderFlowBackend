using Microsoft.AspNetCore.Mvc;
using NimbleFlow.Api.Helpers;
using NimbleFlow.Api.Services;
using NimbleFlow.Contracts.DTOs.Categories;

namespace NimbleFlow.Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class CategoryController : ControllerBase
{
    private readonly CategoryService _categoryService;

    public CategoryController(CategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    /// <summary>Creates a category</summary>
    /// <param name="requestBody"></param>
    /// <response code="400">Bad Request</response>
    /// <response code="500">Internal Server Error</response>
    [HttpPost]
    [ProducesResponseType(typeof(CategoryDto), StatusCodes.Status201Created)]
    public async Task<IActionResult> CreateCategory([FromBody] CreateCategoryDto requestBody)
    {
        var validationError = requestBody.Validate();
        if (validationError is not null)
            return validationError;

        var response = await _categoryService.CreateCategory(requestBody);
        if (response is null)
            return Problem();

        return Created(string.Empty, response);
    }

    /// <summary>Gets all categories paginated</summary>
    /// <param name="page"></param>
    /// <param name="limit"></param>
    /// <param name="includeDeleted"></param>
    /// <response code="204">No Content</response>
    [HttpGet]
    [ProducesResponseType(typeof(CategoryDto[]), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllCategoriesPaginated(
        [FromQuery] int page = 0,
        [FromQuery] int limit = 12,
        [FromQuery] bool includeDeleted = false
    )
    {
        var response = await _categoryService.GetAllCategoriesPaginated(page, limit, includeDeleted);
        if (!response.Any())
            return NoContent();

        return Ok(response);
    }

    /// <summary>Gets a category by id</summary>
    /// <param name="categoryId"></param>
    /// <response code="404">Not Found</response>
    [HttpGet("{categoryId:guid}")]
    [ProducesResponseType(typeof(CategoryDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetCategoryById([FromRoute] Guid categoryId)
    {
        var response = await _categoryService.GetCategoryById(categoryId);
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
    /// <response code="500">Internal Server Error</response>
    [HttpPut("{categoryId:guid}")]
    public async Task<IActionResult> UpdateCategoryById(
        [FromRoute] Guid categoryId,
        [FromBody] UpdateCategoryDto requestBody
    )
    {
        var validationError = requestBody.Validate();
        if (validationError is not null)
            return validationError;

        var responseStatus = await _categoryService.UpdateCategoryById(categoryId, requestBody);
        return StatusCode((int)responseStatus);
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
        return StatusCode((int)responseStatus);
    }
}