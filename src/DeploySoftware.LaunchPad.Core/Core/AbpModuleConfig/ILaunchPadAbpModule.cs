using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace DeploySoftware.LaunchPad.Core.AbpModuleConfig
{
    public partial interface ILaunchPadAbpModule<TSecretHelper, TSecretVault, TSecretProvider>
        where TSecretHelper : ISecretHelper, new()
        where TSecretVault : SecretVaultBase, new()
        where TSecretProvider : SecretProviderBase<TSecretVault>, new()
    {

        public IHostEnvironment HostEnvironment { get; }

        public IConfigurationRoot ConfigurationRoot { get; }

        public TSecretProvider SecretProvider { get; }

    }
}
