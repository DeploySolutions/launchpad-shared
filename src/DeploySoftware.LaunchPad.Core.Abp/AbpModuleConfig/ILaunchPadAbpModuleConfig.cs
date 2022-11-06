using DeploySoftware.LaunchPad.Core.Config;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace DeploySoftware.LaunchPad.Core.Abp.AbpModuleConfig
{
    public partial interface ILaunchPadAbpModuleConfig<TSecretVault, TSecretProvider, THostEnvironment> 
        where TSecretVault : SecretVaultBase, new()
        where TSecretProvider : SecretProviderBase<TSecretVault>, new()
        where THostEnvironment : IHostEnvironment
    {

        public THostEnvironment HostEnvironment { get; }

        public IConfigurationRoot ConfigurationRoot { get; }

        public TSecretProvider SecretProvider { get; }
    }
}
