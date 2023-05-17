using NimbleFlow.Contracts.DTOs.Products;
using NimbleFlow.Contracts.Interfaces.Repositories;
using NimbleFlow.Contracts.Interfaces.Services;

namespace NimbleFlow.Api.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;

    public ProductService(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public Task<IEnumerable<GetProduct>> GetAllProductsPaginated()
    {
        throw new NotImplementedException();
    }

    public Task<Guid?> CreateProduct(PostProduct product)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteProductById(Guid productId)
    {
        throw new NotImplementedException();
    }

    public Task<bool> UpdateProductById(Guid productId, PutProduct product)
    {
        throw new NotImplementedException();
    }

    public Task<GetProduct?> GetProductById(Guid productId)
    {
        throw new NotImplementedException();
    }
}