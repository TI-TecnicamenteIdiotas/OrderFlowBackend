using System.Globalization;
using Grpc.Net.Client;
using Microsoft.Extensions.Options;
using NimbleFlow.Api.Options;
using NimbleFlow.Data.Partials.DTOs;
using NimbleFlowHub.Contracts;
using ProductHubPublisherClient = NimbleFlowHub.Contracts.ProductHubPublisher.ProductHubPublisherClient;

namespace NimbleFlow.Api.Services;

public class ProductHubService
{
    private readonly HubServiceOptions _hubServiceOptions;

    public ProductHubService(IOptions<HubServiceOptions> hubServiceOptions)
    {
        _hubServiceOptions = hubServiceOptions.Value;
    }

    public async Task PublishProductCreatedAsync(ProductDto message)
    {
        using var channel = GrpcChannel.ForAddress(_hubServiceOptions.GrpcConnectionUrl);
        var grpcClient = new ProductHubPublisherClient(channel);
        _ = await grpcClient.PublishProductCreatedAsync(new PublishProductValue
        {
            Id = message.Id.ToString(),
            Title = message.Title,
            Description = message.Description ?? string.Empty,
            Price = float.Parse(message.Price.ToString(CultureInfo.InvariantCulture)),
            ImageUrl = message.ImageUrl ?? string.Empty,
            IsFavorite = message.IsFavorite,
            CategoryId = message.CategoryId.ToString(),
        });
    }

    public async Task PublishProductUpdatedAsync(ProductDto message)
    {
        using var channel = GrpcChannel.ForAddress(_hubServiceOptions.GrpcConnectionUrl);
        var grpcClient = new ProductHubPublisherClient(channel);
        _ = await grpcClient.PublishProductUpdatedAsync(new PublishProductValue
        {
            Id = message.Id.ToString(),
            Title = message.Title,
            Description = message.Description ?? string.Empty,
            Price = float.Parse(message.Price.ToString(CultureInfo.InvariantCulture)),
            ImageUrl = message.ImageUrl ?? string.Empty,
            IsFavorite = message.IsFavorite,
            CategoryId = message.CategoryId.ToString(),
        });
    }

    public async Task PublishManyProductsDeletedAsync(IEnumerable<Guid> message)
    {
        using var channel = GrpcChannel.ForAddress(_hubServiceOptions.GrpcConnectionUrl);
        var grpcClient = new ProductHubPublisherClient(channel);
        _ = await grpcClient.PublishManyProductsDeletedAsync(new PublishProductIds
        {
            Ids = { message.Select(x => x.ToString()) }
        });
    }

    public async Task PublishProductDeletedAsync(Guid message)
    {
        using var channel = GrpcChannel.ForAddress(_hubServiceOptions.GrpcConnectionUrl);
        var grpcClient = new ProductHubPublisherClient(channel);
        _ = await grpcClient.PublishProductDeletedAsync(new PublishProductId
        {
            Id = message.ToString()
        });
    }
}