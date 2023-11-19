// ***********************************************************************
// Assembly         : Deploy.LaunchPad.Core.Abp
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 09-17-2023
// ***********************************************************************
// <copyright file="LaunchPadAbpModuleConfigBase.cs" company="Deploy Software Solutions, inc.">
//     2018-2023 Deploy Software Solutions, inc.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Castle.Core.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace Deploy.LaunchPad.Core.Abp.AbpModuleConfig
{
    /// <summary>
    /// Class LaunchPadAbpModuleConfigBase.
    /// Implements the <see cref="Deploy.LaunchPad.Core.Abp.AbpModuleConfig.ILaunchPadAbpModuleConfig{THostEnvironment}" />
    /// </summary>
    /// <typeparam name="THostEnvironment">The type of the t host environment.</typeparam>
    /// <seealso cref="Deploy.LaunchPad.Core.Abp.AbpModuleConfig.ILaunchPadAbpModuleConfig{THostEnvironment}" />
    public abstract partial class LaunchPadAbpModuleConfigBase<THostEnvironment> : ILaunchPadAbpModuleConfig<THostEnvironment>
        where THostEnvironment : IHostEnvironment
    {

        /// <summary>
        /// Gets or sets the name of the internal module.
        /// </summary>
        /// <value>The name of the internal module.</value>
        public virtual string InternalModuleName { get; set; }

        /// <summary>
        /// Gets or sets the logger.
        /// </summary>
        /// <value>The logger.</value>
        public virtual ILogger Logger { get; set; } = NullLogger.Instance;

        /// <summary>
        /// Gets the host environment.
        /// </summary>
        /// <value>The host environment.</value>
        public virtual THostEnvironment HostEnvironment { get; set; }

        /// <summary>
        /// Gets the configuration root.
        /// </summary>
        /// <value>The configuration root.</value>
        public virtual IConfigurationRoot ConfigurationRoot { get; set; }

        /// <summary>
        /// Gets or sets the secret.
        /// </summary>
        /// <value>The secret.</value>
        public virtual ILaunchPadAbpModuleSecretConfiguration Secret { get; protected set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="LaunchPadAbpModuleConfigBase{THostEnvironment}"/> class.
        /// </summary>
        protected LaunchPadAbpModuleConfigBase()
        {
            Secret = new LaunchPadAbpModuleSecretConfiguration();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LaunchPadAbpModuleConfigBase{THostEnvironment}"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        protected LaunchPadAbpModuleConfigBase(ILogger logger)
        {
            Logger = logger;
            Secret = new LaunchPadAbpModuleSecretConfiguration();
        }

    }
}
