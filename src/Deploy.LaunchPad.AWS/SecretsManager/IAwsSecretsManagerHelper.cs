using Amazon.SecretsManager;

namespace Deploy.LaunchPad.AWS.SecretsManager
{
    public interface IAwsSecretsManagerHelper : IAwsHelper<AmazonSecretsManagerConfig>
    {

        public AmazonSecretsManagerClient SecretsManagerClient { get; }

    }
}
