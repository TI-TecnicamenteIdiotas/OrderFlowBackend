using System.Net;
using NimbleFlow.Api.Repositories;
using NimbleFlow.Api.Services.Base;
using NimbleFlow.Contracts.DTOs.Products;
using NimbleFlow.Data.Context;
using NimbleFlow.Data.Models;

namespace NimbleFlow.Api.Services;

public class ProductService : ServiceBase<NimbleFlowContext, Product>
{
    private readonly ProductRepository _productRepository;

    public ProductService(ProductRepository productRepository) : base(productRepository)
    {
        _productRepository = productRepository;
    }

    public Task<GetProduct?> CreateProduct(PostProduct productDto)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<GetProduct>> GetAllProductsPaginated(int page, int limit, bool includeDeleted)
    {
        throw new NotImplementedException();
    }

    public Task<GetProduct?> GetProductById(Guid productId)
    {
        throw new NotImplementedException();
    }

    public Task<(HttpStatusCode, GetProduct?)> UpdateProductById(Guid productId, PutProduct productDto)
    {
        throw new NotImplementedException();
    }
}