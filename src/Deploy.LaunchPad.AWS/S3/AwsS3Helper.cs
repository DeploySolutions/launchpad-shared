using Amazon.S3;
using Castle.Core.Logging;
using Newtonsoft.Json;

namespace Deploy.LaunchPad.AWS.S3
{
    public partial class AwsS3Helper : AwsHelperBase<AmazonS3Config>, IAwsS3Helper
    {
        protected AmazonS3Client _s3Client;

        [JsonIgnore]
        public AmazonS3Client S3Client { get { return _s3Client; } }


        public AwsS3Helper() : base()
        {
        }

        public AwsS3Helper(ILogger logger, string awsRegionEndpointName) : base(logger, awsRegionEndpointName)
        {
            _s3Client = new AmazonS3Client(Region);
        }

        public AwsS3Helper(ILogger logger, string awsRegionEndpointName, AmazonS3Client s3Client) : base(logger, awsRegionEndpointName)
        {
            _s3Client = s3Client;
        }

        public AwsS3Helper(ILogger logger, string awsRegionEndpointName, AmazonS3Client s3Client, string localAwsProfileName) : base(logger, awsRegionEndpointName)
        {
            AwsProfileName = localAwsProfileName;
            _s3Client = s3Client;
        }
    }
}
