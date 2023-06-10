using Amazon.S3;

namespace NimbleFlow.Api.Options;

public class AmazonS3Options
{
    public AmazonS3Config AmazonS3Config { get; set; } = null!;
    public string BucketName { get; set; } = null!;
    public bool IsProductionEnvironment { get; set; }

    public void Deconstruct(
        out AmazonS3Config amazonS3Config,
        out string bucketName,
        out bool isProductionEnvironment
    )
    {
        amazonS3Config = AmazonS3Config;
        bucketName = BucketName;
        isProductionEnvironment = IsProductionEnvironment;
    }
}