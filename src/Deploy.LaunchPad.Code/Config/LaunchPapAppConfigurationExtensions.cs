// ***********************************************************************
// Assembly         : Deploy.LaunchPad.Core.Abp
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 10-15-2023
// ***********************************************************************
// <copyright file="LaunchPadAbpAppConfigurations.cs" company="Deploy Software Solutions, inc.">
//     2018-2024 Deploy Software Solutions, inc.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Deploy.LaunchPad.Util.Extensions;
using Microsoft.Extensions.Configuration;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;

namespace Deploy.LaunchPad.Code.Config
{
    /// <summary>
    /// Class LaunchPadAbpAppConfigurations.
    /// </summary>
    public static class LaunchPapAppConfigurationExtensions
    {
        /// <summary>
        /// The configuration cache
        /// </summary>
        private static readonly ConcurrentDictionary<string, IConfigurationRoot> _configurationCache;

        /// <summary>
        /// Initializes static members of the <see cref="LaunchPapAppConfigurationExtensions"/> class.
        /// </summary>
        static LaunchPapAppConfigurationExtensions()
        {
            _configurationCache = new ConcurrentDictionary<string, IConfigurationRoot>();
        }

        /// <summary>
        /// Gets the specified path.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="environmentName">Name of the environment.</param>
        /// <param name="addUserSecrets">if set to <c>true</c> [add user secrets].</param>
        /// <param name="userSecretId">The user secret identifier.</param>
        /// <param name="jsonFiles">The json files.</param>
        /// <returns>IConfigurationRoot.</returns>
        public static IConfigurationRoot Get(string path, string environmentName = null, bool addUserSecrets = false, string userSecretId = "", IList<string> jsonFiles = null)
        {
            var cacheKey = path + "#" + environmentName + "#" + addUserSecrets;
            return _configurationCache.GetOrAdd(
                cacheKey,
                _ => BuildConfiguration(path, environmentName, addUserSecrets, userSecretId, jsonFiles)
            );
        }

        /// <summary>
        /// Builds the configuration.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="environmentName">Name of the environment.</param>
        /// <param name="addUserSecrets">if set to <c>true</c> [add user secrets].</param>
        /// <param name="userSecretId">The user secret identifier.</param>
        /// <param name="jsonFiles">The json files.</param>
        /// <returns>IConfigurationRoot.</returns>
        private static IConfigurationRoot BuildConfiguration(string path, string environmentName = null, bool addUserSecrets = false, string userSecretId = "", IList<string> jsonFiles = null)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(path)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            if (!environmentName.IsNullOrWhiteSpace())
            {
                builder = builder.AddJsonFile($"appsettings.{environmentName}.json", optional: true);
            }

            builder = builder.AddEnvironmentVariables();
            if (addUserSecrets && !string.IsNullOrEmpty(userSecretId))
            {
                builder.AddUserSecrets(userSecretId);
            }

            if (jsonFiles != null)
            { 
                foreach( var jsonFile in jsonFiles)
                {
                    builder.AddJsonFile(jsonFile,
                        optional: false,
                        reloadOnChange: true
                    );
                }
            }

            return builder.Build();
        }

    }
}
