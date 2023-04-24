using Microsoft.AspNetCore.Mvc;
using NimbleFlow.Api.Helpers;
using NimbleFlow.Contracts.DTOs.Categories;
using NimbleFlow.Contracts.Interfaces.Services;

namespace NimbleFlow.Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class CategoryController : ControllerBase
{
    private readonly ICategoryService _categoryService;

    public CategoryController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    /// <summary>Gets all categories paginated</summary>
    /// <param name="page"></param>
    /// <param name="limit"></param>
    /// <response code="204">No Content</response>
    [HttpGet]
    [ProducesResponseType(typeof(GetCategory[]), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllCategoriesPaginated([FromQuery] int page = 0, [FromQuery] int limit = 12)
    {
        var categories = await _categoryService.GetAllCategoriesPaginated();
        if (!categories.Any())
            return NoContent();

        return Ok(categories);
    }

    /// <summary>Gets a category by id</summary>
    /// <param name="categoryId"></param>
    /// <response code="404">Not Found</response>
    [HttpGet("{categoryId:guid}")]
    [ProducesResponseType(typeof(GetCategory), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetCategoryById([FromRoute] Guid categoryId)
    {
        var category = await _categoryService.GetCategoryById(categoryId);
        if (category is null)
            return NotFound();

        return Ok(category);
    }

    private readonly record struct AddCategoryResponseWrapper(Guid CategoryId);

    /// <summary>Creates a category</summary>
    /// <param name="requestBody"></param>
    /// <response code="400">Bad Request</response>
    /// <response code="500">Internal Server Error</response>
    [HttpPost]
    [ProducesResponseType(typeof(AddCategoryResponseWrapper), StatusCodes.Status201Created)]
    public async Task<IActionResult> CreateCategory([FromBody] PostCategory requestBody)
    {
        var requestBodyValidationError = requestBody.Validate();
        if (requestBodyValidationError is not null)
            return requestBodyValidationError;

        var categoryId = await _categoryService.CreateCategory(requestBody);
        if (categoryId is null)
            return Problem();

        return Created(string.Empty, new AddCategoryResponseWrapper
        {
            CategoryId = categoryId.Value
        });
    }

    /// <summary>Deletes a category by id</summary>
    /// <param name="categoryId"></param>
    /// <response code="200">Ok</response>
    /// <response code="404">Not Found</response>
    /// <response code="500">Internal Server Error</response>
    [HttpDelete("{categoryId:guid}")]
    public async Task<IActionResult> DeleteCategoryById([FromQuery] Guid categoryId)
    {
        var categoryExists = await _categoryService.GetCategoryById(categoryId);
        if (categoryExists is null)
            return NotFound();

        var wasCategoryDeleted = await _categoryService.DeleteById(categoryId);
        if (!wasCategoryDeleted)
            return Problem();

        return Ok();
    }

    /// <summary>Updates a category by id</summary>
    /// <param name="categoryId"></param>
    /// <param name="requestBody"></param>
    /// <response code="200">Ok</response>
    /// <response code="400">Bad Request</response>
    /// <response code="404">Not Found</response>
    /// <response code="500">Internal Server Error</response>
    [HttpPut("{categoryId:guid}")]
    public async Task<IActionResult> UpdateCategoryById([FromQuery] Guid categoryId, [FromBody] PutCategory requestBody)
    {
        var requestBodyValidationError = requestBody.Validate();
        if (requestBodyValidationError is not null)
            return requestBodyValidationError;

        var categoryExists = await _categoryService.GetCategoryById(categoryId);
        if (categoryExists is null)
            return NotFound();

        var wasCategoryUpdated = await _categoryService.UpdateCategoryById(categoryId, requestBody);
        if (!wasCategoryUpdated)
            return Problem();

        return Ok();
    }
}