using NimbleFlow.Api.Repositories.Base;
using NimbleFlow.Data.Context;
using NimbleFlow.Data.Models;

namespace NimbleFlow.Api.Repositories;

public class TableRepository : RepositoryBase<NimbleFlowContext, Table>
{
    public TableRepository(NimbleFlowContext dbContext) : base(dbContext)
    {
    }
}