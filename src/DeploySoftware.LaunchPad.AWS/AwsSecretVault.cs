using DeploySoftware.LaunchPad.Core.Config;

namespace DeploySoftware.LaunchPad.AWS
{
    public class AwsSecretVault : SecretVault
    {


        public AwsSecretVault() : base()
        {

        }

        public AwsSecretVault(string arn) : base()
        {
            Name = arn;
            VaultId = arn;
            ProviderId = "AmazonSecretsManager";
        }

        public AwsSecretVault(string arn, string name) : base()
        {
            Name = name;
            VaultId = arn;
            ProviderId = "AmazonSecretsManager";
        }


    }
}
