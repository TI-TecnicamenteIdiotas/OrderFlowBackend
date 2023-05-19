using NimbleFlow.Api.Repositories.Base;
using NimbleFlow.Data.Context;
using NimbleFlow.Data.Models;

namespace NimbleFlow.Api.Repositories;

public class OrderRepository : RepositoryBase<NimbleFlowContext, Order>
{
    public OrderRepository(NimbleFlowContext dbContext) : base(dbContext)
    {
    }
}