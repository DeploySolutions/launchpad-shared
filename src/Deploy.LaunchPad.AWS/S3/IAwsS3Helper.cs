using Amazon.S3;

namespace Deploy.LaunchPad.AWS.S3
{
    public interface IAwsS3Helper : IAwsHelper<AmazonS3Config>
    {

        public AmazonS3Client S3Client { get; }

    }
}
