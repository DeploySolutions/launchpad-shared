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
