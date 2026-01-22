// ***********************************************************************
// Assembly         : Deploy.LaunchPad.Core.Abp
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 01-08-2023
// ***********************************************************************
// <copyright file="LaunchPadAbpAppConfigurationAccessor.cs" company="Deploy Software Solutions, inc.">
//     2018-2024 Deploy Software Solutions, inc.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Abp.Dependency;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System;

namespace Deploy.LaunchPad.Core.Abp.AbpModuleConfig
{
    /// <summary>
    /// Class LaunchPadAbpAppConfigurationAccessor.
    /// Implements the <see cref="Deploy.LaunchPad.Core.Abp.AbpModuleConfig.ILaunchPadAppConfigurationAccessor" />
    /// Implements the <see cref="ISingletonDependency" />
    /// </summary>
    /// <seealso cref="Deploy.LaunchPad.Core.Abp.AbpModuleConfig.ILaunchPadAppConfigurationAccessor" />
    /// <seealso cref="ISingletonDependency" />
    public partial class LaunchPadAbpAppConfigurationAccessor : ILaunchPadAppConfigurationAccessor
    {
        /// <summary>
        /// Gets the configuration.
        /// </summary>
        /// <value>The configuration.</value>
        public IConfigurationRoot Configuration { get; }

       
        /// <summary>
        /// Initializes a new instance of the <see cref="LaunchPadAbpAppConfigurationAccessor"/> class.
        /// </summary>
        /// <param name="configuration">The configuration provided by DI.</param>
        public LaunchPadAbpAppConfigurationAccessor(IConfiguration configuration)
        {
            // If you need IConfigurationRoot for reloading, cast; otherwise, just use IConfiguration
            Configuration = configuration as IConfigurationRoot
                ?? throw new ArgumentException("configuration must be an IConfigurationRoot", nameof(configuration));
        }
    }
}
