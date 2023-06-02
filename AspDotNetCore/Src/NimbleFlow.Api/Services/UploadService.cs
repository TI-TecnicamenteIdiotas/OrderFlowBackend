using System.Net;
using Amazon;
using Amazon.Runtime;
using Amazon.S3;
using Amazon.S3.Transfer;

namespace NimbleFlow.Api.Services;

public class UploadService
{
    public async Task<(HttpStatusCode, string)> UploadFileAsync(Stream stream, string contentType, string fileExtension)
    {
        var credentials = new EnvironmentVariablesAWSCredentials();
        var isDevelopmentEnvironment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") is "Development";
        var isContainerEnvironment = Environment.GetEnvironmentVariable("DOTNET_RUNNING_IN_CONTAINER") == "true";
        var objectKey = string.Join(string.Empty, Guid.NewGuid(), fileExtension);
        var bucketName = Environment.GetEnvironmentVariable("AWS_S3_BUCKET_NAME");
        var serviceUrl = Environment.GetEnvironmentVariable("AWS_S3_SERVICE_URL");
        var awsRegion = Environment.GetEnvironmentVariable("AWS_REGION");
        var objectPath = isDevelopmentEnvironment || isContainerEnvironment
            // Path-style access should be used to work with minio
            ? isContainerEnvironment
                ? $"http://localhost:10502/{bucketName}/{objectKey}"
                : $"{serviceUrl}/{bucketName}/{objectKey}"
            // Virtual-hosted–style access should be used to work with aws s3
            : $"https://{bucketName}.s3.{awsRegion}.amazonaws.com/{objectKey}";

        AmazonS3Config config;
        if (isDevelopmentEnvironment || isContainerEnvironment)
            config = new AmazonS3Config
            {
                ServiceURL = serviceUrl!,
                ForcePathStyle = true
            };
        else
            config = new AmazonS3Config
            {
                RegionEndpoint = RegionEndpoint.GetBySystemName(awsRegion!)
            };

        using var client = new AmazonS3Client(credentials, config);
        var fileTransferUtility = new TransferUtility(client);
        var fileTransferUtilityRequest = new TransferUtilityUploadRequest
        {
            BucketName = bucketName,
            InputStream = stream,
            StorageClass = S3StorageClass.Standard,
            PartSize = stream.Length,
            Key = objectKey,
            CannedACL = S3CannedACL.PublicRead,
            ContentType = contentType
        };
        await fileTransferUtility.UploadAsync(fileTransferUtilityRequest);
        return (HttpStatusCode.Created, objectPath);
    }
}