using Amazon.SecretsManager;
using Castle.Core.Logging;
using Newtonsoft.Json;

namespace DeploySoftware.LaunchPad.AWS.SecretsManager
{
    public partial class AwsSecretsManagerHelper : AwsHelperBase<AmazonSecretsManagerConfig>, IAwsSecretsManagerHelper
    {
        protected AmazonSecretsManagerClient _awsClient;

        [JsonIgnore]
        public AmazonSecretsManagerClient SecretsManagerClient { get { return _awsClient; } }


        public AwsSecretsManagerHelper() : base()
        {
        }

        public AwsSecretsManagerHelper(ILogger logger, string awsRegionEndpointName) :base(logger, awsRegionEndpointName)
        {
            _awsClient = new AmazonSecretsManagerClient(Region);
        }

        public AwsSecretsManagerHelper(ILogger logger, string awsRegionEndpointName, AmazonSecretsManagerClient awsClient) : base(logger, awsRegionEndpointName)
        {
            _awsClient = awsClient;
        }

        public AwsSecretsManagerHelper(ILogger logger, string awsRegionEndpointName, AmazonSecretsManagerClient awsClient, string localAwsProfileName) : base(logger, awsRegionEndpointName)
        {
            AwsProfileName = localAwsProfileName;
            _awsClient = awsClient;
        }
    }
}
