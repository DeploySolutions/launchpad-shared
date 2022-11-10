using Amazon.SecretsManager;
using Castle.Core.Logging;

namespace DeploySoftware.LaunchPad.AWS.SecretsManager
{
    public partial class AwsSecretsManagerHelperFactory : AwsHelperBase<AmazonSecretsManagerConfig>
    {

        public AwsSecretsManagerHelperFactory() : base()
        {
        }

        public AwsSecretsManagerHelperFactory(ILogger logger, string awsRegionEndpointName) : base(logger, awsRegionEndpointName)
        {

        }

    }
}
