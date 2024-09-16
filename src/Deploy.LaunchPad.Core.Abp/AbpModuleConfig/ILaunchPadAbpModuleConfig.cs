// ***********************************************************************
// Assembly         : Deploy.LaunchPad.Core.Abp
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 02-19-2023
// ***********************************************************************
// <copyright file="ILaunchPadAbpModuleConfig.cs" company="Deploy Software Solutions, inc.">
//     2018-2024 Deploy Software Solutions, inc.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace Deploy.LaunchPad.Core.Abp.AbpModuleConfig
{
    /// <summary>
    /// Interface ILaunchPadAbpModuleConfig
    /// </summary>
    /// <typeparam name="THostEnvironment">The type of the t host environment.</typeparam>
    public partial interface ILaunchPadAbpModuleConfig<THostEnvironment>
        where THostEnvironment : IHostEnvironment
    {

        /// <summary>
        /// Gets the host environment.
        /// </summary>
        /// <value>The host environment.</value>
        public THostEnvironment HostEnvironment { get; }

        /// <summary>
        /// Gets the configuration root.
        /// </summary>
        /// <value>The configuration root.</value>
        public IConfigurationRoot ConfigurationRoot { get; }



    }
}
