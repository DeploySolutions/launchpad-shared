using Amazon.SecretsManager.Model;
using DeploySoftware.LaunchPad.Core.Config;
using System.Threading.Tasks;
using System;

namespace DeploySoftware.LaunchPad.AWS
{
    public class AwsSecretVault : SecretVaultBase
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
