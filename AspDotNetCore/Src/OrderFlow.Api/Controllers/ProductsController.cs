using Microsoft.AspNetCore.Mvc;
using OrderFlow.Business.DTO.Products;
using OrderFlow.Business.Interfaces.Services;
using OrderFlow.Data.Models;

namespace OrderFlow.Api.Controllers;

[ApiController]
[Route("api/products")]
public class ProductsController : ControllerBase
{
    private readonly IProductsService _service;
    public ProductsController(IProductsService productsService)
    {
        _service = productsService;
    }

    [HttpGet("all")]
    public async Task<ActionResult<IEnumerable<Product>>> GetAll()
    {
        // var Produtos = await _service.GetAll();
        // var _products = _mapper.Map<List<GetProduct>>(Produtos);
        // return CustomResponse(_products);

        return Ok();
    }

    [HttpGet]
    public async Task<ActionResult<bool>> GetProductById([FromQuery] int productId)
    {
        var product = await _service.GetById(productId);
        return Ok();
    }

    [HttpPost]
    public async Task<ActionResult<Product>> AddProduct([FromBody] PostProduct product)
    {
        // var _product = _mapper.Map<Product>(product);
        // var p = await _service.AddProduct(_product);
        // return CustomResponse(p);

        return Ok();
    }

    [HttpDelete]
    public async Task<ActionResult<Product>> DeleteProduct([FromQuery] int productID)
    {

        var result = await _service.DeleteProduct(productID);
        return Ok();

    }

    [HttpPut]
    public async Task<ActionResult<Product>> UpdateProduct([FromQuery] int productID, [FromBody] PutProduct product)
    {
        // var _product = _mapper.Map<Product>(product);
        // if (productID != _product.Id) _responseService.DivergentId(productID, _product.Id);
        // if (HasError()) return CustomResponse(_product);
        // var result = await _service.UpdateProduct(_product);
        // return CustomResponse(result);

        return Ok();
    }
}