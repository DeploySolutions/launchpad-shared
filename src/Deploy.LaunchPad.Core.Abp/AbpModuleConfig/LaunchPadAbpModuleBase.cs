using Abp.AutoMapper;
using Abp.Dependency;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Abp.UI;
using Castle.Core.Logging;
using Deploy.LaunchPad.Core.Config;
using Deploy.LaunchPad.Core.Util;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using FluentValidation;
using FluentValidation.Results;

namespace Deploy.LaunchPad.Core.Abp.AbpModuleConfig
{
    [Serializable()]
    public abstract class LaunchPadAbpModuleBase<TAbpModuleHelper> : AbpModule,
        ILaunchPadAbpModule<TAbpModuleHelper>
        where TAbpModuleHelper : ILaunchPadAbpModuleHelper
    {

        public virtual string InternalModuleName { get; set; }


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

        protected virtual ValidationResult IsConfigurationValid<TAbpModuleConfig>(TAbpModuleConfig config)
            where TAbpModuleConfig : LaunchPadAbpModuleConfigBase<IWebHostEnvironment>
        {
            IValidator<TAbpModuleConfig> validator = null;
            ValidationResult validationResult = null;
            using (var scope = IocManager.CreateScope())
            {
                validator = scope.Resolve<IValidator<TAbpModuleConfig>>();
            }

            if (validator != null)
            {
                validationResult = validator.Validate(config);
                if (!validationResult.IsValid)
                {
                    string validationErrorMessage = string.Format(
                        "Config validation error(s) during PostInitialize() of '{0}': '{1}'",
                        config.InternalModuleName,
                        validationResult
                    );
                    throw new UserFriendlyException(validationErrorMessage);
                }

            }

            return validationResult;
        }

        /// <summary>
        /// Load the AbpModule base properties, starting a new scope
        /// </summary>
        /// <typeparam name="THostEnvironment"></typeparam>
        /// <param name="config"></param>
        /// <returns></returns>
        protected virtual ILaunchPadAbpModuleConfig<THostEnvironment> LoadBaseConfigPropertiesOnPostInitialize<THostEnvironment>(
            LaunchPadAbpModuleConfigBase<THostEnvironment> config)
            where THostEnvironment : IHostEnvironment
        {
            using (var scope = IocManager.CreateScope())
            {
                // set the host and configuration parameters
                config.HostEnvironment = scope.Resolve<THostEnvironment>();
                config.ConfigurationRoot = scope.Resolve<IConfigurationRoot>();
            }
            return config;

        }

        /// <summary>
        /// Loads the AbpModule base properties within an existing scope
        /// </summary>
        /// <typeparam name="THostEnvironment"></typeparam>
        /// <param name="config"></param>
        /// <param name="scope"></param>
        /// <returns></returns>

        protected virtual ILaunchPadAbpModuleConfig<THostEnvironment> LoadBaseConfigPropertiesOnPostInitialize<THostEnvironment>(
            LaunchPadAbpModuleConfigBase<THostEnvironment> config, IScopedIocResolver scope)
            where THostEnvironment : IHostEnvironment
        {

            // set the host and configuration parameters
            config.HostEnvironment = scope.Resolve<THostEnvironment>();
            config.ConfigurationRoot = scope.Resolve<IConfigurationRoot>();
            return config;

        }

        /// <summary>
        /// Loads the Fields dictionary for a given AbpModule from the available secrets
        /// </summary>
        /// <typeparam name="THostEnvironment"></typeparam>
        /// <param name="config"></param>
        /// <param name="abpModuleKeyPrefix">The starting text that identifies a field belongs to this particular abpmodule</param>
        /// <param name="secretVault"></param>
        /// <returns></returns>
        protected virtual ILaunchPadAbpModuleConfig<THostEnvironment> PopulateSecretConfiguration<THostEnvironment>(LaunchPadAbpModuleConfigBase<THostEnvironment> config, string abpModuleKeyPrefix, ISecretVault secretVault)
            where THostEnvironment : IHostEnvironment
        {
            Guard.Against<ArgumentNullException>(config == null, "config cannot be null.");
            Guard.Against<ArgumentNullException>(secretVault == null, "secretVault cannot be null.");
            Guard.Against<ArgumentNullException>(string.IsNullOrEmpty(abpModuleKeyPrefix), "abpModuleKeyPrefix cannot be null or empty.");

            foreach (var field in from field in secretVault.Fields
                                  where field.Key.StartsWith(abpModuleKeyPrefix)
                                  select field)
            {
                config.Secret.Fields.TryAdd(field.Key, field.Value);
            }
            return config;
        }

    }
}