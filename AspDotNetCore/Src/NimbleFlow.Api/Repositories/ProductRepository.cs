using NimbleFlow.Api.Repositories.Base;
using NimbleFlow.Data.Context;
using NimbleFlow.Data.Models;

namespace NimbleFlow.Api.Repositories;

public class ProductRepository : RepositoryBase<NimbleFlowContext, Product>
{
    public ProductRepository(NimbleFlowContext dbContext) : base(dbContext)
    {
    }
}