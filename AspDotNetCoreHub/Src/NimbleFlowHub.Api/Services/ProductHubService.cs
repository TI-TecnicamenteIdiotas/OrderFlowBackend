using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Microsoft.AspNetCore.SignalR;
using NimbleFlowHub.Api.Extensions;
using NimbleFlowHub.Api.Hubs;
using NimbleFlowHub.Contracts;
using ProductHubPublisherBase = NimbleFlowHub.Contracts.ProductHubPublisher.ProductHubPublisherBase;

namespace NimbleFlowHub.Api.Services;

public class ProductHubService : ProductHubPublisherBase
{
    private readonly IHubContext<MainHub> _hubContext;

    public ProductHubService(IHubContext<MainHub> hubContext)
    {
        _hubContext = hubContext;
    }

    public override async Task<Empty> PublishProductCreated(PublishProductValue request, ServerCallContext context)
    {
        await _hubContext.Clients.All.SendJsonAsync("ProductCreated", request);
        return new Empty();
    }

    public override async Task<Empty> PublishProductUpdated(PublishProductValue request, ServerCallContext context)
    {
        await _hubContext.Clients.All.SendJsonAsync("ProductUpdated", request);
        return new Empty();
    }

    public override async Task<Empty> PublishManyProductsDeleted(PublishProductIds request,
        ServerCallContext context)
    {
        await _hubContext.Clients.All.SendJsonAsync("ManyProductsDeleted", request);
        return new Empty();
    }

    public override async Task<Empty> PublishProductDeleted(PublishProductId request, ServerCallContext context)
    {
        await _hubContext.Clients.All.SendJsonAsync("ProductDeleted", request);
        return new Empty();
    }
}