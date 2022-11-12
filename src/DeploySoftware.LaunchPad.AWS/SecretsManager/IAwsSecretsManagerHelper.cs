using Amazon.SecretsManager;

namespace DeploySoftware.LaunchPad.AWS.SecretsManager
{
    public interface IAwsSecretsManagerHelper : IAwsHelper<AmazonSecretsManagerConfig>
    {

        public AmazonSecretsManagerClient SecretsManagerClient { get;}

    }
}
