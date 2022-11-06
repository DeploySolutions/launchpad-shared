using Castle.Core.Logging;
using DeploySoftware.LaunchPad.Core.Config;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace DeploySoftware.LaunchPad.Core.Abp.AbpModuleConfig
{
    public abstract partial class LaunchPadAbpModuleConfigBase<TSecretVault, TSecretProvider, THostEnvironment> : ILaunchPadAbpModuleConfig<TSecretVault, TSecretProvider, THostEnvironment>
        where TSecretVault : SecretVaultBase, new()
        where TSecretProvider : SecretProviderBase<TSecretVault>, new()
        where THostEnvironment : IHostEnvironment
    {

        public virtual ILogger Logger { get; set; } = NullLogger.Instance;

        public virtual THostEnvironment HostEnvironment { get; set; }

        public virtual IConfigurationRoot ConfigurationRoot { get; set; }

        public virtual TSecretProvider SecretProvider { get; set; }

        protected LaunchPadAbpModuleConfigBase(ILogger logger)
        {
            Logger = logger;
            SecretProvider = new TSecretProvider();
        }

    }
}
