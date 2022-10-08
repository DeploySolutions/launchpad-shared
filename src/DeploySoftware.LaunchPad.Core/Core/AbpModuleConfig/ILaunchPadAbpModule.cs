using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace DeploySoftware.LaunchPad.Core.AbpModuleConfig
{
    public partial interface ILaunchPadAbpModule<TSecretHelper, TSecretVault, TAbpModuleHelper>
        where TSecretHelper : ISecretHelper, new()
        where TSecretVault : SecretVaultBase, new()
        where TAbpModuleHelper : ILaunchPadAbpModuleHelper<TSecretHelper, TSecretVault>
    {

        public TAbpModuleHelper AbpModuleHelper { get; set; }
    }
}
