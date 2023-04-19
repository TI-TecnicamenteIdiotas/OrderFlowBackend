using OrderFlow.Contracts.DTOs.Products;

namespace OrderFlow.Contracts.Interfaces.Services;

public interface IProductService
{
    Task<IEnumerable<GetProduct>> GetAllPaginated();
    Task<Guid?> AddProduct(PostProduct product);
    Task<bool> DeleteProductById(Guid productId);
    Task<bool> UpdateProductById(Guid productId, PutProduct product);
    Task<GetProduct?> GetProductById(Guid productId);
}