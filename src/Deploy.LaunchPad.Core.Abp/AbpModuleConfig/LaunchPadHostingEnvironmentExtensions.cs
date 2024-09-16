// ***********************************************************************
// Assembly         : Deploy.LaunchPad.Core.Abp
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 10-15-2023
// ***********************************************************************
// <copyright file="LaunchPadHostingEnvironmentExtensions.cs" company="Deploy Software Solutions, inc.">
//     2018-2024 Deploy Software Solutions, inc.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System.Collections;
using System.Collections.Generic;

namespace Deploy.LaunchPad.Core.Abp.AbpModuleConfig
{
    /// <summary>
    /// Class LaunchPadHostingEnvironmentExtensions.
    /// </summary>
    public static class LaunchPadHostingEnvironmentExtensions
    {
        /// <summary>
        /// Gets the application configuration.
        /// </summary>
        /// <param name="env">The env.</param>
        /// <param name="userSecretId">The user secret identifier.</param>
        /// <param name="jsonFiles">The json files.</param>
        /// <returns>IConfigurationRoot.</returns>
        public static IConfigurationRoot GetAppConfiguration(this IWebHostEnvironment env, string userSecretId = "", IList<string> jsonFiles = null)
        {
            return LaunchPadAbpAppConfigurations.Get(env.ContentRootPath, env.EnvironmentName, env.IsDevelopment(), userSecretId, jsonFiles);
        }
    }
}
