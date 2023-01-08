using Amazon.ElasticFileSystem;
using Castle.Core.Logging;

namespace Deploy.LaunchPad.AWS.ElasticFileSystem
{
    public partial class AwsElasticFileSystemHelper : AwsHelperBase<AmazonElasticFileSystemConfig>, IAwsElasticFileSystemHelper
    {
        public AwsElasticFileSystemHelper() : base()
        {
        }

        public AwsElasticFileSystemHelper(ILogger logger, string awsRegionEndpointName) : base(logger, awsRegionEndpointName)
        {

        }

    }
}
