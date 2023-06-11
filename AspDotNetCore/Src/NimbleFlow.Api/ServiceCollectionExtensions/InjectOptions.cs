using Amazon;
using Amazon.Runtime;
using Amazon.S3;
using NimbleFlow.Api.Options;

namespace NimbleFlow.Api.ServiceCollectionExtensions;

public static partial class ServiceCollectionExtensions
{
    public static void InjectOptions(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<HubServiceOptions>(x =>
        {
            x.GrpcConnectionUrl = configuration["HUB_SERVER_URL"];
        });

        services.Configure<AmazonOptions>(x =>
        {
            x.Credentials = new BasicAWSCredentials(
                configuration["AWS_ACCESS_KEY_ID"],
                configuration["AWS_SECRET_ACCESS_KEY"]
            );
        });

        services.Configure<AmazonS3Options>(x =>
        {
            var isProduction = configuration["ASPNETCORE_ENVIRONMENT"] is "Production";
            var regionEndpoint = RegionEndpoint.GetBySystemName(configuration["AWS_REGION"]);

            x.AmazonS3Config = isProduction
                ? new AmazonS3Config
                {
                    RegionEndpoint = regionEndpoint
                }
                : new AmazonS3Config
                {
                    ServiceURL = configuration["AWS_S3_SERVICE_URL"],
                    ForcePathStyle = true
                };
            x.BucketName = configuration["AWS_S3_BUCKET_NAME"];
            x.IsProductionEnvironment = isProduction;
        });
    }
}