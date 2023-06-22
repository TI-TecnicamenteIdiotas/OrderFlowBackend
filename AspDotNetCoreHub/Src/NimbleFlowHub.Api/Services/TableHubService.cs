using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Microsoft.AspNetCore.SignalR;
using NimbleFlowHub.Api.Extensions;
using NimbleFlowHub.Api.Hubs;
using NimbleFlowHub.Contracts;
using TableHubPublisherBase = NimbleFlowHub.Contracts.TableHubPublisher.TableHubPublisherBase;

namespace NimbleFlowHub.Api.Services;

public class TableHubService : TableHubPublisherBase
{
    private readonly IHubContext<MainHub> _hubContext;

    public TableHubService(IHubContext<MainHub> hubContext)
    {
        _hubContext = hubContext;
    }

    public override async Task<Empty> PublishTableCreated(PublishTableValue request, ServerCallContext context)
    {
        await _hubContext.Clients.All.SendJsonAsync("TableCreated", request);
        return new Empty();
    }

    public override async Task<Empty> PublishTableUpdated(PublishTableValue request, ServerCallContext context)
    {
        await _hubContext.Clients.All.SendJsonAsync("TableUpdated", request);
        return new Empty();
    }

    public override async Task<Empty> PublishManyTablesDeleted(PublishTableIds request, ServerCallContext context)
    {
        await _hubContext.Clients.All.SendJsonAsync("ManyTablesDeleted", request);
        return new Empty();
    }

    public override async Task<Empty> PublishTableDeleted(PublishTableId request, ServerCallContext context)
    {
        await _hubContext.Clients.All.SendJsonAsync("TableDeleted", request);
        return new Empty();
    }
}