using Abp.Dependency;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Castle.Core.Logging;
using Microsoft.Extensions.Configuration.UserSecrets;
using System;
using System.Reflection;

namespace DeploySoftware.LaunchPad.Core.AbpModuleConfig
{
    [Serializable()]
    public abstract class LaunchPadAbpModuleBase<TSecretHelper, TSecretVault, TSecretProvider, TAbpModuleHelper> : AbpModule, ILaunchPadAbpModule<TSecretHelper, TSecretVault, TSecretProvider>
        where TSecretHelper : ISecretHelper, new()
        where TSecretVault : SecretVaultBase, new()
        where TSecretProvider : SecretProviderBase<TSecretVault>, new()
        where TAbpModuleHelper: ILaunchPadAbpModuleHelper<TSecretHelper, TSecretVault>
    {

        protected TAbpModuleHelper _abpModuleHelper;

        public TAbpModuleHelper AbpModuleHelper { get { return _abpModuleHelper; } }

        protected TSecretProvider _secretProvider;
        public TSecretProvider SecretProvider { get { return _secretProvider; } }

        protected LaunchPadAbpModuleBase()
        {
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        public override void PostInitialize()
        {
            base.PostInitialize();
            
        }


        public override void PreInitialize()
        {
            base.PreInitialize();

        }
    }
}