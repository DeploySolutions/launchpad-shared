using Abp.Dependency;
using Abp.Modules;
using Castle.Core.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;
using DeploySoftware.LaunchPad.Core.Config;
using System.Collections.Generic;
using Abp.Configuration;

namespace DeploySoftware.LaunchPad.Core.Abp.AbpModuleConfig
{
    [Serializable()]
    public abstract class LaunchPadAbpModuleBase<TSecretHelper, TSecretVault, TAbpModuleHelper> : AbpModule, 
        ILaunchPadAbpModule<TSecretHelper, TSecretVault, TAbpModuleHelper>
        where TSecretHelper : ISecretHelper
        where TSecretVault : SecretVaultBase, new()
        where TAbpModuleHelper: ILaunchPadAbpModuleHelper<TSecretHelper, TSecretVault>
    {
        

        protected TAbpModuleHelper _abpModuleHelper;

        public TAbpModuleHelper AbpModuleHelper { 
            get { return _abpModuleHelper; }
            set { _abpModuleHelper = value; }
        }
        protected LaunchPadAbpModuleBase()
        {
        }

        public LaunchPadAbpModuleBase(ILogger logger)
        {
            Logger = logger;
        }

        public override void PreInitialize()
        {
            base.PreInitialize();        
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        public override void PostInitialize()
        {
            base.PostInitialize();

        }

        protected virtual ILaunchPadAbpModuleConfig<TSecretVault, TSecretProvider, THostEnvironment> LoadBaseConfigPropertiesOnPostInitialize<TSecretProvider, THostEnvironment>(
            LaunchPadAbpModuleConfigBase<TSecretVault, TSecretProvider, THostEnvironment> config,
            string vaultsJsonPath, string abpModuleInternalName)
            where TSecretProvider : SecretProviderBase<TSecretVault>, new () 
            where THostEnvironment : IHostEnvironment
        {
            var settingManager = IocManager.Resolve<ISettingManager>();
            // add the secret vaults to the module's SecretProvider
            config.SecretProvider.SecretVaults =
                (Dictionary<string, TSecretVault>)AbpModuleHelper.GetSecretVaults<TSecretVault>(
                    settingManager,
                    vaultsJsonPath,
                    abpModuleInternalName + ".PostInitialize()"
            );

            // set the host and configuration parameters
            var hostEnvironment = IocManager.Resolve<THostEnvironment>();
            config.HostEnvironment = hostEnvironment;
            var configurationRoot = IocManager.Resolve<IConfigurationRoot>();
            config.ConfigurationRoot = configurationRoot;
            return config;

        }


    }
}