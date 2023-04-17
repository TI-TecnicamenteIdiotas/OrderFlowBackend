using OrderFlow.Data.Models;

namespace OrderFlow.Business.Interfaces.Services;

public interface IProductsService
{
    Task< IEnumerable<Product>> GetAll();
    Task<Product> AddProduct(Product value);
    Task<bool> DeleteProduct(int value);
    Task<Product> UpdateProduct(Product value);
    Task<Product> GetById(int id);
}