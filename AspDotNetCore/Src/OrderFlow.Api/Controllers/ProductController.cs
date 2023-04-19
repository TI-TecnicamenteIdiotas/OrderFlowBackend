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

    [HttpGet]
    public async Task<IActionResult> GetAllPaginated()
    {
        var products = await _productService.GetAllPaginated();
        if (!products.Any())
            return NoContent();

        return Ok(products);
    }

    [HttpGet("{productId:guid}")]
    public async Task<IActionResult> GetProductById([FromRoute] Guid productId)
    {
        var product = await _productService.GetProductById(productId);
        if (product is null)
            return NotFound();

        return Ok(product);
    }

    private readonly record struct AddProductResponseWrapper(Guid ProductId);

    [HttpPost]
    public async Task<IActionResult> AddProduct([FromBody] PostProduct requestBody)
    {
        var requestBodyValidationError = requestBody.Validate();
        if (requestBodyValidationError is not null)
            return requestBodyValidationError;

        var productId = await _productService.AddProduct(requestBody);
        if (productId is null)
            return Problem();

        return Created(string.Empty, new AddProductResponseWrapper
        {
            ProductId = productId.Value
        });
    }

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