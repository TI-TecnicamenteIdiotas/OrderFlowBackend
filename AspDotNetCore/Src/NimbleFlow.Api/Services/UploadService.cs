using System.Net;
using Amazon.Runtime;
using Amazon.S3;
using Amazon.S3.Transfer;
using Microsoft.Extensions.Options;
using NimbleFlow.Api.Options;
using NimbleFlow.Contracts.Enums;

namespace NimbleFlow.Api.Services;

public class UploadService
{
    private readonly AWSCredentials _awsCredentials;
    private readonly AmazonS3Config _amazonS3Config;
    private readonly string _bucketName;
    private readonly bool _isProductionEnvironment;

    public UploadService(IOptions<AmazonOptions> amazonOptions, IOptions<AmazonS3Options> amazonS3Options)
    {
        _awsCredentials = amazonOptions.Value.Credentials;

        (
            _amazonS3Config,
            _bucketName,
            _isProductionEnvironment
        ) = amazonS3Options.Value;
    }

    private static string GetObjectKey(FileTypeEnum fileTypeEnum)
        => string.Join(
            string.Empty,
            Guid.NewGuid(),
            fileTypeEnum switch
            {
                FileTypeEnum.Jpeg => ".jpeg",
                FileTypeEnum.Png => ".png",
                _ => string.Empty
            }
        );

    private string GetObjectPath(string objectKey)
        => _isProductionEnvironment switch
        {
            true => $"https://{_bucketName}.s3.{_amazonS3Config.RegionEndpoint?.SystemName}.amazonaws.com/{objectKey}",
            _ => $"http://localhost:10502/{_bucketName}/{objectKey}"
        };

    public async Task<(HttpStatusCode, string)> UploadFileAsync(
        Stream stream,
        string contentType,
        FileTypeEnum fileTypeEnum
    )
    {
        using var client = new AmazonS3Client(_awsCredentials, _amazonS3Config);
        var objectKey = GetObjectKey(fileTypeEnum);
        var fileTransferUtility = new TransferUtility(client);
        var fileTransferUtilityRequest = new TransferUtilityUploadRequest
        {
            BucketName = _bucketName,
            Key = objectKey,
            CannedACL = S3CannedACL.PublicRead,
            ContentType = contentType,
            StorageClass = S3StorageClass.Standard,
            PartSize = stream.Length,
            InputStream = stream
        };

        await fileTransferUtility.UploadAsync(fileTransferUtilityRequest);
        return (HttpStatusCode.Created, GetObjectPath(objectKey));
    }
}