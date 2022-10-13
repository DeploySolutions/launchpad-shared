using DeploySoftware.LaunchPad.Core.AbpModuleConfig;

namespace DeploySoftware.LaunchPad.AWS.SecretsManager
{
    public class AwsSecretVault : SecretVaultBase
    {
        

        public AwsSecretVault() : base()
        {
            
        }

        public AwsSecretVault(string secretIdentifier) : base(secretIdentifier)
        {

        }

        public AwsSecretVault(string secretIdentifier, string name) : base(secretIdentifier, name)
        {

        }

        public AwsSecretVault(string secretIdentifier, string name, string fullName) : base(secretIdentifier,name, fullName)
        {

        }

    }
}
