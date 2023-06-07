using Amazon;
using Amazon.S3;

namespace NimbleFlow.Api.Options;

public class AmazonS3Options
{
    public AmazonS3Config AmazonS3Config { get; set; } = null!;
    public RegionEndpoint RegionEndpoint { get; set; } = null!;
    public string BucketName { get; set; } = null!;
    public bool IsDevelopmentEnvironment { get; set; }
    public bool IsContainerEnvironment { get; set; }
    public bool IsProductionEnvironment { get; set; }

    public void Deconstruct(
        out AmazonS3Config amazonS3Config,
        out RegionEndpoint regionEndpoint,
        out string bucketName,
        out bool isDevelopmentEnvironment,
        out bool isContainerEnvironment,
        out bool isProductionEnvironment
    )
    {
        amazonS3Config = AmazonS3Config;
        regionEndpoint = RegionEndpoint;
        bucketName = BucketName;
        isDevelopmentEnvironment = IsDevelopmentEnvironment;
        isContainerEnvironment = IsContainerEnvironment;
        isProductionEnvironment = IsProductionEnvironment;
    }
}