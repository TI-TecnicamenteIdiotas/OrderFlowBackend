using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Microsoft.AspNetCore.SignalR;
using NimbleFlowHub.Api.Extensions;
using NimbleFlowHub.Contracts;
using HubPublisherBase = NimbleFlowHub.Contracts.HubPublisher.HubPublisherBase;

namespace NimbleFlowHub.Api.Services;

public class HubService : HubPublisherBase
{
    private readonly IHubContext<Hubs.MainHub> _hubContext;

    public HubService(IHubContext<Hubs.MainHub> hubContext)
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
}