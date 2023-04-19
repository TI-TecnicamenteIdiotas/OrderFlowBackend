using OrderFlow.Contracts.DTOs.Tables;
using OrderFlow.Contracts.Interfaces.Services;

namespace OrderFlow.Api.Services;

public class TableService : ITableService
{
    public TableService()
    {
    }

    // public async Task<Table> AddTable(Table value)
    // {
    //     if (!IsValid(value)) return value;
    //     return await _repository.Add(value);
    // }
    //
    // private bool IsValid(Table table)
    // {
    //     Regex regex = new(@"^[\w\s\-à-úÀ-Ú]*$");
    //     if (table.Name.Length > 50)
    //     {
    //         AddError("O nome deve possuir menos de 50 caracteres!");
    //     }
    //
    //     if (!regex.IsMatch(table.Name))
    //     {
    //         AddError("Não é permitido adicionar caracteres especiais ao Titulo!");
    //     }
    //
    //     if (table.PaidValue < 0)
    //     {
    //         AddError("O preço pago não pode ser valor negativo!");
    //     }
    //
    //     if (table.Items != null &&
    //         table.Items.Any(item => (item.Product.Price * item.Count) + item.Additional - item.Discount < 0))
    //         AddError("Não é permitido salvar um item com valor total menor que zero!");
    //
    //     return !HasError();
    // }
    //
    // public async Task<IEnumerable<Table>> GetAll()
    // {
    //     return await _repository.GetQueryable()
    //         .Include(x => x.Items).ThenInclude(i => i.Product)
    //         .ToListAsync();
    // }
    //
    // public async Task<Table> GetById(int id)
    // {
    //     return await _repository.GetQueryable().Where(x => x.Id == id).Include(x => x.Items)
    //         .ThenInclude(i => i.Product).ThenInclude(p => p.Category).FirstOrDefaultAsync();
    // }
    //
    // public async Task<bool> DeleteTable(int value)
    // {
    //     var p = await _repository.GetById(value);
    //     if (p == null) AddError($"Mesa com ID {value} não existe");
    //     if (HasError()) return false;
    //     await _repository.Remove(value);
    //     return !HasError();
    // }
    //
    // public async Task<Table> UpdateTable(Table value)
    // {
    //     Table result = null;
    //     if (!IsValid(value)) return value;
    //
    //     result = await _repository.Update(value);
    //     if (result == null)
    //         return result;
    //
    //     var oldItems = await _itemsService.GetTableItems(result.Id);
    //     var newItems = result.Items;
    //     if (oldItems.Count() <= newItems.Count)
    //         return result;
    //
    //     var extraItems = oldItems.Where(p => !newItems.Any(p2 => p2.Id == p.Id));
    //     foreach (var item in extraItems)
    //     {
    //         await _itemsService.DeleteItem(item.Id);
    //     }
    //
    //     return result;
    // }
    public Task<IEnumerable<GetTable>> GetAllPaginated()
    {
        throw new NotImplementedException();
    }

    public Task<uint?> AddTable(PostTable table)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteTableById(uint tableId)
    {
        throw new NotImplementedException();
    }

    public Task<bool> UpdateTableById(uint tableId, PutTable table)
    {
        throw new NotImplementedException();
    }

    public Task<GetTable?> GetTableById(uint tableId)
    {
        throw new NotImplementedException();
    }
}