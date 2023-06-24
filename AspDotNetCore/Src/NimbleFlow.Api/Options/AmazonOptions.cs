using Amazon.Runtime;

namespace NimbleFlow.Api.Options;

public class AmazonOptions
{
    public AWSCredentials Credentials { get; set; } = null!;
}