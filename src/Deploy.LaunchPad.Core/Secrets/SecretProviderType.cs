using System;
using System.Collections.Generic;
using System.Text;

namespace Deploy.LaunchPad.Core.Secrets
{
    public enum SecretProviderType
    {
        None = 0,
        EnvironmentVariable = 1,
        UserSecrets = 2,
        AwsSecretsManager = 3,
        AzureKeyVault = 4
    }
}
