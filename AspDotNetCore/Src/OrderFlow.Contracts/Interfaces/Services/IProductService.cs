using OrderFlow.Contracts.DTOs.Products;

namespace OrderFlow.Contracts.Interfaces.Services;

public interface IProductService
{
    Task<IEnumerable<GetProduct>> GetAllProductsPaginated();
    Task<Guid?> CreateProduct(PostProduct product);
    Task<bool> DeleteProductById(Guid productId);
    Task<bool> UpdateProductById(Guid productId, PutProduct product);
    Task<GetProduct?> GetProductById(Guid productId);
}