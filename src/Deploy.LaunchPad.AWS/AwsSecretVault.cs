using Deploy.LaunchPad.Core.Config;

namespace Deploy.LaunchPad.AWS
{
    public class AwsSecretVault : SecretVaultBase, ISecretVault
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
