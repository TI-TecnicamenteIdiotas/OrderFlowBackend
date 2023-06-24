using Grpc.Net.Client;
using Microsoft.Extensions.Options;
using NimbleFlow.Api.Options;
using NimbleFlow.Data.Partials.DTOs;
using NimbleFlowHub.Contracts;
using CategoryHubPublisherClient = NimbleFlowHub.Contracts.CategoryHubPublisher.CategoryHubPublisherClient;

namespace NimbleFlow.Api.Services;

public class CategoryHubService
{
    private readonly HubServiceOptions _hubServiceOptions;

    public CategoryHubService(IOptions<HubServiceOptions> hubServiceOptions)
    {
        _hubServiceOptions = hubServiceOptions.Value;
    }

    public async Task PublishCategoryCreatedAsync(CategoryDto message)
    {
        using var channel = GrpcChannel.ForAddress(_hubServiceOptions.GrpcConnectionUrl);
        var grpcClient = new CategoryHubPublisherClient(channel);
        _ = await grpcClient.PublishCategoryCreatedAsync(new PublishCategoryValue
        {
            Id = message.Id.ToString(),
            Title = message.Title,
            ColorTheme = message.ColorTheme ?? 0,
            CategoryIcon = message.CategoryIcon ?? 0
        });
    }

    public async Task PublishCategoryUpdatedAsync(CategoryDto message)
    {
        using var channel = GrpcChannel.ForAddress(_hubServiceOptions.GrpcConnectionUrl);
        var grpcClient = new CategoryHubPublisherClient(channel);
        _ = await grpcClient.PublishCategoryUpdatedAsync(new PublishCategoryValue
        {
            Id = message.Id.ToString(),
            Title = message.Title,
            ColorTheme = message.ColorTheme ?? 0,
            CategoryIcon = message.CategoryIcon ?? 0
        });
    }

    public async Task PublishManyCategoriesDeletedAsync(IEnumerable<Guid> message)
    {
        using var channel = GrpcChannel.ForAddress(_hubServiceOptions.GrpcConnectionUrl);
        var grpcClient = new CategoryHubPublisherClient(channel);
        _ = await grpcClient.PublishManyCategoriesDeletedAsync(new PublishCategoryIds
        {
            Ids = { message.Select(x => x.ToString()) }
        });
    }

    public async Task PublishCategoryDeletedAsync(Guid message)
    {
        using var channel = GrpcChannel.ForAddress(_hubServiceOptions.GrpcConnectionUrl);
        var grpcClient = new CategoryHubPublisherClient(channel);
        _ = await grpcClient.PublishCategoryDeletedAsync(new PublishCategoryId
        {
            Id = message.ToString()
        });
    }
}