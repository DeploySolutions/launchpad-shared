﻿
using DeploySoftware.LaunchPad.Core.Config;

namespace DeploySoftware.LaunchPad.Core.Abp.AbpModuleConfig
{
    public partial interface ILaunchPadAbpModule<TSecretHelper, TSecretVault, TAbpModuleHelper>
        where TSecretHelper : ISecretHelper
        where TSecretVault : SecretVaultBase, new()
        where TAbpModuleHelper : ILaunchPadAbpModuleHelper<TSecretHelper, TSecretVault>
    {

        public TAbpModuleHelper AbpModuleHelper { get; set; }

    }
}
