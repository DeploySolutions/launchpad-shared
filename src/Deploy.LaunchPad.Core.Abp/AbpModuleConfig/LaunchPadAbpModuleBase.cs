// ***********************************************************************
// Assembly         : Deploy.LaunchPad.Core.Abp
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 09-18-2023
// ***********************************************************************
// <copyright file="LaunchPadAbpModuleBase.cs" company="Deploy Software Solutions, inc.">
//     2018-2024 Deploy Software Solutions, inc.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Abp.AutoMapper;
using Abp.Dependency;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Abp.UI;
using Castle.Core.Logging;
using Deploy.LaunchPad.Core.Config;
using Deploy.LaunchPad.Util;
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
    /// <summary>
    /// Class LaunchPadAbpModuleBase.
    /// Implements the <see cref="AbpModule" />
    /// Implements the <see cref="Deploy.LaunchPad.Core.Abp.AbpModuleConfig.ILaunchPadAbpModule{TAbpModuleHelper}" />
    /// </summary>
    /// <typeparam name="TAbpModuleHelper">The type of the t abp module helper.</typeparam>
    /// <seealso cref="AbpModule" />
    /// <seealso cref="Deploy.LaunchPad.Core.Abp.AbpModuleConfig.ILaunchPadAbpModule{TAbpModuleHelper}" />
    [Serializable()]
    public abstract class LaunchPadAbpModuleBase<TAbpModuleHelper> : AbpModule,
        ILaunchPadAbpModule<TAbpModuleHelper>
        where TAbpModuleHelper : ILaunchPadAbpModuleHelper
    {

        /// <summary>
        /// Gets or sets the name of the internal module.
        /// </summary>
        /// <value>The name of the internal module.</value>
        public virtual string InternalModuleName { get; set; }


        /// <summary>
        /// The abp module helper
        /// </summary>
        protected TAbpModuleHelper _abpModuleHelper;

        /// <summary>
        /// Gets or sets the abp module helper.
        /// </summary>
        /// <value>The abp module helper.</value>
        public TAbpModuleHelper AbpModuleHelper
        {
            get { return _abpModuleHelper; }
            set { _abpModuleHelper = value; }
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="LaunchPadAbpModuleBase{TAbpModuleHelper}"/> class.
        /// </summary>
        protected LaunchPadAbpModuleBase()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LaunchPadAbpModuleBase{TAbpModuleHelper}"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        public LaunchPadAbpModuleBase(ILogger logger)
        {
            Logger = logger;
        }

        /// <summary>
        /// This is the first event called on application startup.
        /// Codes can be placed here to run before dependency injection registrations.
        /// </summary>
        public override void PreInitialize()
        {
            base.PreInitialize();
        }

        /// <summary>
        /// This method is used to register dependencies for this module.
        /// </summary>
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

        /// <summary>
        /// This method is called lastly on application startup.
        /// </summary>
        public override void PostInitialize()
        {
            base.PostInitialize();

        }

        /// <summary>
        /// Determines whether [is configuration valid] [the specified configuration].
        /// </summary>
        /// <typeparam name="TAbpModuleConfig">The type of the t abp module configuration.</typeparam>
        /// <param name="config">The configuration.</param>
        /// <param name="shouldValidateAndThrow">if set to <c>true</c> [should validate and throw].</param>
        /// <returns>ValidationResult.</returns>
        protected virtual ValidationResult IsConfigurationValid<TAbpModuleConfig>(TAbpModuleConfig config, bool shouldValidateAndThrow = false)
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
                if (shouldValidateAndThrow)
                {

                    validator.ValidateAndThrow(config);
                }
                else
                {
                    validationResult = validator.Validate(config);
                }
            }

            return validationResult;
        }

        /// <summary>
        /// Load the AbpModule base properties, starting a new scope
        /// </summary>
        /// <typeparam name="THostEnvironment">The type of the t host environment.</typeparam>
        /// <param name="config">The configuration.</param>
        /// <returns>ILaunchPadAbpModuleConfig&lt;THostEnvironment&gt;.</returns>
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
        /// <typeparam name="THostEnvironment">The type of the t host environment.</typeparam>
        /// <param name="config">The configuration.</param>
        /// <param name="scope">The scope.</param>
        /// <returns>ILaunchPadAbpModuleConfig&lt;THostEnvironment&gt;.</returns>

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
        /// <typeparam name="THostEnvironment">The type of the t host environment.</typeparam>
        /// <param name="config">The configuration.</param>
        /// <param name="abpModuleKeyPrefix">The starting text that identifies a field belongs to this particular abpmodule</param>
        /// <param name="secretVault">The secret vault.</param>
        /// <returns>ILaunchPadAbpModuleConfig&lt;THostEnvironment&gt;.</returns>
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