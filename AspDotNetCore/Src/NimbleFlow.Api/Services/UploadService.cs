using System.Net;
using Amazon;
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
    private readonly RegionEndpoint _amazonS3Region;
    private readonly string _bucketName;
    private readonly string _serviceUrl;
    private readonly bool _isDevelopmentEnvironment;
    private readonly bool _isContainerEnvironment;

    public UploadService(IOptions<AmazonOptions> amazonOptions, IOptions<AmazonS3Options> amazonS3Options)
    {
        _awsCredentials = amazonOptions.Value.Credentials;

        (
            _amazonS3Config,
            _amazonS3Region,
            _bucketName,
            _serviceUrl,
            _isDevelopmentEnvironment,
            _isContainerEnvironment
        ) = amazonS3Options.Value;
    }

    private string GetObjectPath(FileTypeEnum fileTypeEnum, out string objectKey)
    {
        var fileExtension = fileTypeEnum switch
        {
            FileTypeEnum.Jpeg => ".jpeg",
            FileTypeEnum.Png => ".png",
            _ => string.Empty
        };
        objectKey = string.Join(string.Empty, Guid.NewGuid(), fileExtension);
        return _isDevelopmentEnvironment switch
        {
            true when _isContainerEnvironment => $"{_serviceUrl}/{_bucketName}/{objectKey}",
            true when !_isContainerEnvironment => $"http://localhost:10502/{_bucketName}/{objectKey}",
            _ => $"https://{_bucketName}.s3.{_amazonS3Region.SystemName}.amazonaws.com/{objectKey}"
        };
    }

    public async Task<(HttpStatusCode, string)> UploadFileAsync(
        Stream stream,
        string contentType,
        FileTypeEnum fileTypeEnum
    )
    {
        var objectPath = GetObjectPath(fileTypeEnum, out var objectKey);
        using var client = new AmazonS3Client(_awsCredentials, _amazonS3Config);
        var fileTransferUtility = new TransferUtility(client);
        var fileTransferUtilityRequest = new TransferUtilityUploadRequest
        {
            BucketName = _bucketName,
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