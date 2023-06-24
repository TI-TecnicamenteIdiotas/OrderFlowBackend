using Grpc.Net.Client;
using Microsoft.Extensions.Options;
using NimbleFlow.Api.Options;
using NimbleFlow.Data.Partials.DTOs;
using NimbleFlowHub.Contracts;
using TableHubPublisherClient = NimbleFlowHub.Contracts.TableHubPublisher.TableHubPublisherClient;

namespace NimbleFlow.Api.Services;

public class TableHubService
{
    private readonly HubServiceOptions _hubServiceOptions;

    public TableHubService(IOptions<HubServiceOptions> hubServiceOptions)
    {
        _hubServiceOptions = hubServiceOptions.Value;
    }

    public async Task PublishTableCreatedAsync(TableDto message)
    {
        using var channel = GrpcChannel.ForAddress(_hubServiceOptions.GrpcConnectionUrl);
        var grpcClient = new TableHubPublisherClient(channel);
        _ = await grpcClient.PublishTableCreatedAsync(new PublishTableValue
        {
            Id = message.Id.ToString(),
            Accountable = message.Accountable,
            IsFullyPaid = message.IsFullyPaid
        });
    }

    public async Task PublishTableUpdatedAsync(TableDto message)
    {
        using var channel = GrpcChannel.ForAddress(_hubServiceOptions.GrpcConnectionUrl);
        var grpcClient = new TableHubPublisherClient(channel);
        _ = await grpcClient.PublishTableUpdatedAsync(new PublishTableValue
        {
            Id = message.Id.ToString(),
            Accountable = message.Accountable,
            IsFullyPaid = message.IsFullyPaid
        });
    }

    public async Task PublishManyTablesDeletedAsync(IEnumerable<Guid> message)
    {
        using var channel = GrpcChannel.ForAddress(_hubServiceOptions.GrpcConnectionUrl);
        var grpcClient = new TableHubPublisherClient(channel);
        _ = await grpcClient.PublishManyTablesDeletedAsync(new PublishTableIds
        {
            Ids = { message.Select(x => x.ToString()) }
        });
    }

    public async Task PublishTableDeletedAsync(Guid message)
    {
        using var channel = GrpcChannel.ForAddress(_hubServiceOptions.GrpcConnectionUrl);
        var grpcClient = new TableHubPublisherClient(channel);
        _ = await grpcClient.PublishTableDeletedAsync(new PublishTableId
        {
            Id = message.ToString()
        });
    }
}