using Amazon.SecretsManager;
using DeploySoftware.LaunchPad.Core.Config;

namespace DeploySoftware.LaunchPad.AWS.SecretsManager
{
    public  interface IAwsSecretsManagerHelper: ISecretHelper, IAwsHelper<AmazonSecretsManagerConfig>
    {
    }
}
