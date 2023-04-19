using OrderFlow.Contracts.DTOs.Categories;
using OrderFlow.Contracts.Interfaces.Services;

namespace OrderFlow.Api.Services;

public class CategoryService : ICategoryService
{
    public CategoryService()
    {
    }

    // public async Task<Category> AddCategory(Category value)
    // {
    //     if (!IsValid(value)) return value;
    //     return await _repository.Add(value);
    // }
    //
    // private bool IsValid(Category value)
    // {
    //     Regex regex = new Regex(@"^[\w\s\-à-úÀ-Ú]+$");
    //     if (value.Title.Length > 50)
    //         AddError("O titulo deve possuir menos de 50 caracteres!");
    //
    //     if (!regex.IsMatch(value.Title))
    //         AddError("Não é permitido adicionar caracteres especiais ao Titulo!");
    //
    //     if (value.ColorTheme < 0)
    //         AddError("A cor informada para a categoria é inválida!");
    //
    //     if (value.CategoryIcon < 0)
    //         AddError("O icone informado para a categoria é inválido!");
    //
    //     return !HasError();
    // }
    //
    //
    // public async Task<IEnumerable<Category>> GetAll()
    // {
    //     return await _repository.GetAll();
    // }
    //
    // public async Task<Category> GetById(int id)
    // {
    //     return await _repository.GetById(id);
    // }
    //
    // public async Task<bool> DeleteCategory(int value)
    // {
    //     var p = await _repository.GetById(value);
    //     if (p == null) AddError($"Categoria com ID {value} não existe");
    //     if (HasError()) return false;
    //     await _repository.Remove(value);
    //     return !HasError();
    // }
    //
    // public async Task<Category> UpdateCategory(Category value)
    // {
    //     if (!IsValid(value)) return value;
    //     return await _repository.Update(value);
    // }
    public Task<IEnumerable<GetCategory>> GetAllPaginated()
    {
        throw new NotImplementedException();
    }

    public Task<uint?> AddCategory(PostCategory category)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteById(uint categoryId)
    {
        throw new NotImplementedException();
    }

    public Task<bool> UpdateCategoryById(uint categoryId, PutCategory category)
    {
        throw new NotImplementedException();
    }

    public Task<GetCategory?> GetCategoryById(uint categoryId)
    {
        throw new NotImplementedException();
    }
}