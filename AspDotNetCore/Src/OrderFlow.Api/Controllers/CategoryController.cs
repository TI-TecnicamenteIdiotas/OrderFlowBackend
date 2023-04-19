using Microsoft.AspNetCore.Mvc;
using OrderFlow.Api.Helpers;
using OrderFlow.Contracts.DTOs.Categories;
using OrderFlow.Contracts.Interfaces.Services;

namespace OrderFlow.Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class CategoryController : ControllerBase
{
    private readonly ICategoryService _categoryService;

    public CategoryController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllPaginated()
    {
        var categories = await _categoryService.GetAllPaginated();
        if (!categories.Any())
            return NoContent();

        return Ok(categories);
    }

    [HttpGet("{categoryId:guid}")]
    public async Task<IActionResult> GetCategoryById([FromRoute] Guid categoryId)
    {
        var category = await _categoryService.GetCategoryById(categoryId);
        if (category is null)
            return NotFound();

        return Ok(category);
    }

    private readonly record struct AddCategoryResponseWrapper(Guid CategoryId);

    [HttpPost]
    public async Task<IActionResult> AddCategory([FromBody] PostCategory requestBody)
    {
        var requestBodyValidationError = requestBody.Validate();
        if (requestBodyValidationError is not null)
            return requestBodyValidationError;

        var categoryId = await _categoryService.AddCategory(requestBody);
        if (categoryId is null)
            return Problem();

        return Created(string.Empty, new AddCategoryResponseWrapper
        {
            CategoryId = categoryId.Value
        });
    }

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