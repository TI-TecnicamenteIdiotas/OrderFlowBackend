﻿using Microsoft.AspNetCore.Mvc;
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
    public async Task<ActionResult> GetAllPaginated()
    {
        var products = await _productService.GetAllPaginated();
        if (!products.Any())
            return NoContent();

        return Ok(products);
    }

    [HttpGet("{productId:guid}")]
    public async Task<ActionResult> GetProductById([FromRoute] Guid productId)
    {
        var product = await _productService.GetProductById(productId);
        if (product is null)
            return NotFound();

        return Ok(product);
    }

    private readonly record struct AddProductResponseWrapper(Guid ProductId);

    [HttpPost]
    public async Task<ActionResult> AddProduct([FromBody] PostProduct requestBody)
    {
        var productId = await _productService.AddProduct(requestBody);
        if (productId is null)
            return Problem();

        return Created(string.Empty, new AddProductResponseWrapper
        {
            ProductId = productId.Value
        });
    }

    [HttpDelete("{productId:guid}")]
    public async Task<ActionResult> DeleteProductById([FromRoute] Guid productId)
    {
        var wasDeleted = await _productService.DeleteProductById(productId);
        if (!wasDeleted)
            return Problem();

        return Ok();
    }

    [HttpPut("{productId:guid}")]
    public async Task<ActionResult> UpdateProductById([FromRoute] Guid productId, [FromBody] PutProduct requestBody)
    {
        var wasUpdated = await _productService.UpdateProductById(productId, requestBody);
        if (!wasUpdated)
            return Problem();

        return Ok();
    }
}