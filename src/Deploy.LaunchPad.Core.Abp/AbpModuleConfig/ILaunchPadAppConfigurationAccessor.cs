// ***********************************************************************
// Assembly         : Deploy.LaunchPad.Core.Abp
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 01-08-2023
// ***********************************************************************
// <copyright file="ILaunchPadAppConfigurationAccessor.cs" company="Deploy Software Solutions, inc.">
//     2018-2024 Deploy Software Solutions, inc.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Abp.Dependency;
using Microsoft.Extensions.Configuration;

namespace Deploy.LaunchPad.Core.Abp.AbpModuleConfig
{
    /// <summary>
    /// Interface ILaunchPadAppConfigurationAccessor
    /// Extends the <see cref="ISingletonDependency" />
    /// </summary>
    /// <seealso cref="ISingletonDependency" />
    public partial interface ILaunchPadAppConfigurationAccessor : ISingletonDependency
    {
        /// <summary>
        /// Gets the configuration.
        /// </summary>
        /// <value>The configuration.</value>
        IConfigurationRoot Configuration { get; }
    }
}