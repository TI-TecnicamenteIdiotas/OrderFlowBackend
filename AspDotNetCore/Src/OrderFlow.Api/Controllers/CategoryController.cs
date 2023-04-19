using Microsoft.AspNetCore.Mvc;
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
    public async Task<ActionResult> GetAllPaginated()
    {
        var categories = await _categoryService.GetAllPaginated();
        if (!categories.Any())
            return NoContent();

        return Ok(categories);
    }

    [HttpGet("{categoryId:guid}")]
    public async Task<ActionResult> GetCategoryById([FromRoute] Guid categoryId)
    {
        var category = await _categoryService.GetCategoryById(categoryId);
        if (category is null)
            return NotFound();

        return Ok(category);
    }

    private readonly record struct AddCategoryResponseWrapper(Guid CategoryId);

    [HttpPost]
    public async Task<ActionResult> AddCategory([FromBody] PostCategory requestBody)
    {
        var categoryId = await _categoryService.AddCategory(requestBody);
        if (categoryId is null)
            return Problem();

        return Created(string.Empty, new AddCategoryResponseWrapper
        {
            CategoryId = categoryId.Value
        });
    }

    [HttpDelete("{categoryId:guid}")]
    public async Task<ActionResult> DeleteCategoryById([FromQuery] Guid categoryId)
    {
        var wasDeleted = await _categoryService.DeleteById(categoryId);
        if (!wasDeleted)
            return Problem();

        return Ok();
    }

    [HttpPut("{categoryId:guid}")]
    public async Task<ActionResult> UpdateCategoryById([FromQuery] Guid categoryId, [FromBody] PutCategory requestBody)
    {
        var wasUpdated = await _categoryService.UpdateCategoryById(categoryId, requestBody);
        if (!wasUpdated)
            return Problem();

        return Ok();
    }
}