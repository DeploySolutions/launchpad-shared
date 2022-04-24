using Abp.Dependency;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Castle.Core.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.UserSecrets;
using Microsoft.Extensions.Hosting;
using System;
using System.Reflection;

namespace DeploySoftware.LaunchPad.Core.AbpModuleConfig
{
    [Serializable()]
    public abstract class LaunchPadAbpModuleBase<TSecretHelper, TSecretVault, TSecretProvider, TAbpModuleHelper, THostEnvironment> : AbpModule, 
        ILaunchPadAbpModule<TSecretHelper, TSecretVault, TSecretProvider, TAbpModuleHelper, THostEnvironment>
        where TSecretHelper : ISecretHelper, new()
        where TSecretVault : SecretVaultBase, new()
        where TSecretProvider : SecretProviderBase<TSecretVault>, new()
        where TAbpModuleHelper: ILaunchPadAbpModuleHelper<TSecretHelper, TSecretVault, THostEnvironment>
        where THostEnvironment : IHostEnvironment
    {
        protected THostEnvironment _hostEnvironment;
        public THostEnvironment HostEnvironment
        {
            get
            {
                return _hostEnvironment;
            }
        }

        protected IConfigurationRoot _appConfiguration;
        public IConfigurationRoot ConfigurationRoot
        {
            get
            {
                return _appConfiguration;
            }
        }

        protected TAbpModuleHelper _abpModuleHelper;

        public TAbpModuleHelper AbpModuleHelper { 
            get { return _abpModuleHelper; }
            set { _abpModuleHelper = value; }
        }

        protected TSecretProvider _secretProvider;
        public TSecretProvider SecretProvider { get { return _secretProvider; } }

        protected LaunchPadAbpModuleBase()
        {
        }

        public LaunchPadAbpModuleBase(ILogger logger, THostEnvironment hostEnvironment, IConfigurationRoot configurationRoot)
        {
            Logger = logger;
            _appConfiguration = configurationRoot;
            _hostEnvironment = hostEnvironment;
            _secretProvider = new TSecretProvider();
        }

        public override void PreInitialize()
        {
            base.PreInitialize();        
        }

        public override void Initialize()
        {
            base.Initialize();
            _abpModuleHelper.Initialize();
        }

        public override void PostInitialize()
        {
            base.PostInitialize();
            _abpModuleHelper.PostInitialize();
        }


    }
}