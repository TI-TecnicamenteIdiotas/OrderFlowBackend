using System.Text.Json;
using Microsoft.AspNetCore.SignalR;

namespace NimbleFlowHub.Api.Extensions;

public static partial class GeneralExtensions
{
    public static Task SendJsonAsync(
        this IClientProxy clientProxy,
        string method,
        object? value,
        CancellationToken cancellationToken = default
    ) => clientProxy.SendAsync(
        method,
        JsonSerializer.Serialize(
            value,
            options: new JsonSerializerOptions(JsonSerializerDefaults.Web)
        ),
        cancellationToken
    );
}