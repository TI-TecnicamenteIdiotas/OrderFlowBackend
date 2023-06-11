using Grpc.Net.Client;
using Microsoft.Extensions.Options;
using NimbleFlow.Api.Options;
using NimbleFlow.Data.Partials.DTOs;
using NimbleFlowHub.Contracts;
using HubPublisherServiceClient = NimbleFlowHub.Contracts.HubPublisher.HubPublisherClient;

namespace NimbleFlow.Api.Services;

public class HubService
{
    private readonly HubServiceOptions _hubServiceOptions;

    public HubService(IOptions<HubServiceOptions> hubServiceOptions)
    {
        _hubServiceOptions = hubServiceOptions.Value;
    }

    public async Task PublishTableCreatedAsync(TableDto message)
    {
        using var channel = GrpcChannel.ForAddress(_hubServiceOptions.GrpcConnectionUrl);
        var grpcClient = new HubPublisherServiceClient(channel);
        _ = await grpcClient.PublishTableCreatedAsync(new PublishTableValue
        {
            Id = message.Id.ToString(),
            Accountable = message.Accountable,
            IsFullyPaid = message.IsFullyPaid
        });
    }
}