using Amazon;
using Amazon.Runtime;
using Amazon.S3;
using NimbleFlow.Api.Options;

namespace NimbleFlow.Api.ServiceCollectionExtensions;

public static partial class ServiceCollectionExtensions
{
    public static void InjectOptions(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<AmazonOptions>(x =>
        {
            x.Credentials = new BasicAWSCredentials(
                configuration["AWS_ACCESS_KEY_ID"],
                configuration["AWS_SECRET_ACCESS_KEY"]
            );
        });

        services.Configure<AmazonS3Options>(x =>
        {
            var isDevelopmentEnvironment = configuration["ASPNETCORE_ENVIRONMENT"] is "Development";
            var isContainerEnvironment = configuration["DOTNET_RUNNING_IN_CONTAINER"] is "true";
            var regionEndpoint = RegionEndpoint.GetBySystemName(configuration["AWS_REGION"]);

            x.AmazonS3Config = isDevelopmentEnvironment || isContainerEnvironment
                ? new AmazonS3Config
                {
                    ServiceURL = configuration["AWS_S3_SERVICE_URL"],
                    ForcePathStyle = true
                }
                : new AmazonS3Config
                {
                    RegionEndpoint = regionEndpoint
                };
            x.RegionEndpoint = regionEndpoint;
            x.BucketName = configuration["AWS_S3_BUCKET_NAME"];
            x.ServiceUrl = configuration["AWS_S3_SERVICE_URL"];
            x.IsDevelopmentEnvironment = isDevelopmentEnvironment;
            x.IsContainerEnvironment = isContainerEnvironment;
        });
    }
}