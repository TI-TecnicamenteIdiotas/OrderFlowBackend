using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Microsoft.AspNetCore.SignalR;
using NimbleFlowHub.Api.Extensions;
using NimbleFlowHub.Api.Hubs;
using NimbleFlowHub.Contracts;
using CategoryHubPublisherBase = NimbleFlowHub.Contracts.CategoryHubPublisher.CategoryHubPublisherBase;

namespace NimbleFlowHub.Api.Services;

public class CategoryHubService : CategoryHubPublisherBase
{
    private readonly IHubContext<MainHub> _hubContext;

    public CategoryHubService(IHubContext<MainHub> hubContext)
    {
        _hubContext = hubContext;
    }

    public override async Task<Empty> PublishCategoryCreated(PublishCategoryValue request, ServerCallContext context)
    {
        await _hubContext.Clients.All.SendJsonAsync("CategoryCreated", request);
        return new Empty();
    }

    public override async Task<Empty> PublishCategoryUpdated(PublishCategoryValue request, ServerCallContext context)
    {
        await _hubContext.Clients.All.SendJsonAsync("CategoryUpdated", request);
        return new Empty();
    }

    public override async Task<Empty> PublishManyCategoriesDeleted(PublishCategoryIds request, ServerCallContext context)
    {
        await _hubContext.Clients.All.SendJsonAsync("ManyCategoriesDeleted", request);
        return new Empty();
    }

    public override async Task<Empty> PublishCategoryDeleted(PublishCategoryId request, ServerCallContext context)
    {
        await _hubContext.Clients.All.SendJsonAsync("CategoryDeleted", request);
        return new Empty();
    }
}