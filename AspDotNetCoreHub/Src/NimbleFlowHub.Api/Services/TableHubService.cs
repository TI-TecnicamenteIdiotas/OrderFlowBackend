using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Microsoft.AspNetCore.SignalR;
using NimbleFlowHub.Api.Extensions;
using NimbleFlowHub.Contracts;

namespace NimbleFlowHub.Api.Services;

public class TableHubService : TableHubPublisherService.TableHubPublisherServiceBase
{
    private readonly IHubContext<Hubs.TableHub> _hubContext;

    public TableHubService(IHubContext<Hubs.TableHub> hubContext)
    {
        _hubContext = hubContext;
    }

    public override async Task<Empty> NotifyCreated(Empty request, ServerCallContext context)
    {
        await _hubContext.Clients.All.SendJsonAsync("created", null);
        return new Empty();
    }
}