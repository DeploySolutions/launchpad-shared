using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Castle.Core.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;

namespace Deploy.LaunchPad.Core.Abp.AbpModuleConfig
{
    [Serializable()]
    public abstract class LaunchPadAbpModuleBase<TAbpModuleHelper> : AbpModule,
        ILaunchPadAbpModule<TAbpModuleHelper>
        where TAbpModuleHelper : ILaunchPadAbpModuleHelper
    {


        protected TAbpModuleHelper _abpModuleHelper;

        public TAbpModuleHelper AbpModuleHelper
        {
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

            // Abp standard module code for registering by convention and finding any automapper classes
            var thisAssembly = typeof(LaunchPadAbpModuleBase<TAbpModuleHelper>).GetAssembly();

            IocManager.RegisterAssemblyByConvention(thisAssembly);

            Configuration.Modules.AbpAutoMapper().Configurators.Add(
                // Scan the assembly for classes which inherit from AutoMapper.Profile
                cfg => cfg.AddMaps(thisAssembly)
            );
        }

        public override void PostInitialize()
        {
            base.PostInitialize();

        }
        protected virtual ILaunchPadAbpModuleConfig<THostEnvironment> LoadBaseConfigPropertiesOnPostInitialize<THostEnvironment>(
            LaunchPadAbpModuleConfigBase<THostEnvironment> config)
            where THostEnvironment : IHostEnvironment
        {

            // set the host and configuration parameters
            var hostEnvironment = IocManager.Resolve<THostEnvironment>();
            config.HostEnvironment = hostEnvironment;
            var configurationRoot = IocManager.Resolve<IConfigurationRoot>();
            config.ConfigurationRoot = configurationRoot;
            return config;

        }

    }
}