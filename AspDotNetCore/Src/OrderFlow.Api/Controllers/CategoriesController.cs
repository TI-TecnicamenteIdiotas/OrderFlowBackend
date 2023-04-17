using Microsoft.AspNetCore.Mvc;
using OrderFlow.Business.DTO.Categories;
using OrderFlow.Business.Interfaces.Services;
using OrderFlow.Data.Models;

namespace OrderFlow.Api.Controllers;

[ApiController]
[Route("api/categories")]
public class CategoriesController : ControllerBase
{
	private readonly ICategoriesService _service;

	public CategoriesController(ICategoriesService categoriesService)
	{
		_service = categoriesService;
	}

	[HttpGet("all")]
	public async Task<ActionResult<IEnumerable<Category>>> GetAll()
	{
		// var Categories = await _service.GetAll();
		// var _categories = _mapper.Map<List<GetCategory>>(Categories);
		// return CustomResponse(_categories);

		return Ok();
	}

	[HttpGet]
	public async Task<ActionResult<bool>> GetCategoryById([FromQuery] int categoryId)
	{
		var category = await _service.GetById(categoryId);
		return Ok();
	}

	[HttpPost]
	public async Task<ActionResult<Category>> AddCategory([FromBody] PostCategory category)
	{
		// var _category = _mapper.Map<Category>(category);
		// var p = await _service.AddCategory(_category);
		// return CustomResponse(p);

		return Ok();
	}

	[HttpDelete]
	public async Task<ActionResult<Category>> DeleteCategory([FromQuery] int categoryID)
	{
		var result = await _service.DeleteCategory(categoryID);
		return Ok();
	}

	[HttpPut]
	public async Task<ActionResult<Category>> UpdateCategory([FromQuery] int categoryID,
		[FromBody] PutCategory category)
	{
		// var _category = _mapper.Map<Category>(category);
		// if (categoryID != _category.Id) _responseService.DivergentId(categoryID, _category.Id);
		// if (HasError()) return CustomResponse(_category);
		// var result = await _service.UpdateCategory(_category);
		// return CustomResponse(result);

		return Ok();
	}
}