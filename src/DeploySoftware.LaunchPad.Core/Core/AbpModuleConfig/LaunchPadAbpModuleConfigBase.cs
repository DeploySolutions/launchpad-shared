using Castle.Core.Logging;
using DeploySoftware.LaunchPad.Core.AbpModuleConfig;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeploySoftware.LaunchPad.Core.AbpModuleConfig
{
    public abstract partial class LaunchPadAbpModuleConfigBase<TSecretVault, TSecretProvider, THostEnvironment> : ILaunchPadAbpModuleConfig<TSecretVault, TSecretProvider, THostEnvironment>
        where TSecretVault : SecretVaultBase, new()
        where TSecretProvider : SecretProviderBase<TSecretVault>, new()
        where THostEnvironment : IHostEnvironment
    {

        public ILogger Logger { get; set; } = NullLogger.Instance;

        public virtual THostEnvironment HostEnvironment { get; set; }

        public virtual IConfigurationRoot ConfigurationRoot { get; set; }

        public virtual TSecretProvider SecretProvider { get; set; }

        protected LaunchPadAbpModuleConfigBase(ILogger logger)
        {
            Logger = logger;
        }

    }
}
