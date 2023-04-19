using OrderFlow.Contracts.DTOs.Products;
using OrderFlow.Contracts.Interfaces.Services;

namespace OrderFlow.Api.Services;

public class ProductService : IProductService
{
    public ProductService()
    {
    }

    // public async Task<Product> AddProduct(Product value)
    // {
    //     if (!IsValid(value)) return value;
    //     return await _repository.Add(value);
    // }
    //
    // private bool IsValid(Product value)
    // {
    //     if (value.Title.Length > 50)
    //         AddError("O titulo deve possuir menos de 50 caracteres!");
    //
    //     var regex = new Regex(@"^[\w\s\-à-úÀ-Ú]+$");
    //     if (!regex.IsMatch(value.Title))
    //         AddError("Não é permitido adicionar caracteres especiais ao Titulo!");
    //
    //     if (value.Description.Length > 255)
    //         AddError("A descrição deve possuir menos de 255 caracteres!");
    //
    //     if (value.ImageURL.Length > 255)
    //         AddError("A URL da imagem deve possuir menos de 255 caracteres!");
    //
    //     if (value.Price < 0)
    //         AddError("O preço não pode ser negativo!");
    //
    //     return !HasError();
    // }
    //
    // public async Task<IEnumerable<Product>> GetAll()
    // {
    //     return await _repository.GetQueryable().Include(x => x.Category).ToListAsync();
    // }
    //
    // public async Task<Product> GetById(int id)
    // {
    //     return await _repository.GetQueryable().Where(x => x.Id == id).Include(x => x.Category).FirstOrDefaultAsync();
    // }
    //
    // public async Task<bool> DeleteProduct(int value)
    // {
    //     var p = await _repository.GetById(value);
    //     if (p == null) AddError($"Produto com ID {value} não existe");
    //     if (HasError()) return false;
    //     await _repository.Remove(value);
    //     return !HasError();
    // }
    //
    // public async Task<Product> UpdateProduct(Product value)
    // {
    //     if (!IsValid(value)) return value;
    //     return await _repository.Update(value);
    // }
    public Task<IEnumerable<GetProduct>> GetAllPaginated()
    {
        throw new NotImplementedException();
    }

    public Task<uint?> AddProduct(PostProduct product)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteProductById(uint productId)
    {
        throw new NotImplementedException();
    }

    public Task<bool> UpdateProductById(uint productId, PutProduct product)
    {
        throw new NotImplementedException();
    }

    public Task<GetProduct?> GetProductById(uint productId)
    {
        throw new NotImplementedException();
    }
}