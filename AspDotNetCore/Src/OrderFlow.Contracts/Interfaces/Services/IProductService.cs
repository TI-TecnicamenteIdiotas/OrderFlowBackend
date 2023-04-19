using OrderFlow.Contracts.DTOs.Products;

namespace OrderFlow.Contracts.Interfaces.Services;

public interface IProductService
{
    Task<IEnumerable<GetProduct>> GetAllPaginated();
    Task<uint?> AddProduct(PostProduct product);
    Task<bool> DeleteProductById(uint productId);
    Task<bool> UpdateProductById(uint productId, PutProduct product);
    Task<GetProduct?> GetProductById(uint productId);
}