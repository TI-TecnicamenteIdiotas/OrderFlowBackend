using System.Net;
using Amazon;
using Amazon.Runtime;
using Amazon.S3;
using Amazon.S3.Transfer;

namespace NimbleFlow.Api.Services;

public class UploadService
{
    private const string BucketName = "nimbleflow";

    public async Task<(HttpStatusCode, string)> UploadFileAsync(Stream stream)
    {
        var credentials = new EnvironmentVariablesAWSCredentials();

        // Virtual-hosted–style access
        // https://bucket-name.s3.region-code.amazonaws.com/key-name
        // Path-style access should be used to work with minio
        var isDevelopmentEnvironment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") is "Development"
                                       || Environment.GetEnvironmentVariable("DOTNET_RUNNING_IN_CONTAINER") == "true";
        AmazonS3Config config;
        if (isDevelopmentEnvironment)
            config = new AmazonS3Config
            {
                ServiceURL = Environment.GetEnvironmentVariable("AWS_S3_SERVICE_URL")!,
                ForcePathStyle = true
            };
        else
            config = new AmazonS3Config
            {
                RegionEndpoint = RegionEndpoint.GetBySystemName(Environment.GetEnvironmentVariable("AWS_REGION")!)
            };

        using var client = new AmazonS3Client(credentials, config);
        var fileTransferUtility = new TransferUtility(client);
        var objectKey = Guid.NewGuid().ToString();
        var fileTransferUtilityRequest = new TransferUtilityUploadRequest
        {
            BucketName = BucketName,
            InputStream = stream,
            StorageClass = S3StorageClass.Standard,
            PartSize = stream.Length,
            Key = objectKey,
            CannedACL = S3CannedACL.PublicRead,
            ContentType = "image/jpeg"
        };
        await fileTransferUtility.UploadAsync(fileTransferUtilityRequest);
        return (HttpStatusCode.Created, $"{BucketName}/{objectKey}");
    }
}