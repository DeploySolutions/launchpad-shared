// ***********************************************************************
// Assembly         : Deploy.LaunchPad.Core.Abp
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 01-08-2023
// ***********************************************************************
// <copyright file="ILaunchPadAbpModuleHelper.cs" company="Deploy Software Solutions, inc.">
//     2018-2024 Deploy Software Solutions, inc.
// </copyright>
// <summary></summary>
// ***********************************************************************

using Abp.Dependency;
using Deploy.LaunchPad.Core.Config;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace Deploy.LaunchPad.Core.Abp.AbpModuleConfig
{
    /// <summary>
    /// Interface ILaunchPadAbpModuleHelper
    /// Extends the <see cref="ISingletonDependency" />
    /// </summary>
    /// <seealso cref="ISingletonDependency" />
    public partial interface ILaunchPadAbpModuleHelper : ISingletonDependency
    {
        /// <summary>
        /// Gets the module secrets.
        /// </summary>
        /// <returns>IDictionary&lt;System.String, System.String&gt;.</returns>
        public IDictionary<string, string> GetModuleSecrets();

        /// <summary>
        /// Gets the module secrets for vault identifier.
        /// </summary>
        /// <param name="vaultIdentifier">The vault identifier.</param>
        /// <returns>IDictionary&lt;System.String, System.String&gt;.</returns>
        public IDictionary<string, string> GetModuleSecretsForVaultIdentifier(string vaultIdentifier);

        /// <summary>
        /// Gets the module vaults.
        /// </summary>
        /// <returns>IList&lt;ISecretVault&gt;.</returns>
        public IList<ISecretVault> GetModuleVaults();

        /// <summary>
        /// Adds the module secret vaults to provider.
        /// </summary>
        /// <typeparam name="TSecretVault">The type of the t secret vault.</typeparam>
        /// <param name="provider">The provider.</param>
        /// <param name="secretProviderVaultsJsonPath">The secret provider vaults json path.</param>
        /// <param name="caller">The caller.</param>
        /// <returns>IDictionary&lt;System.String, ISecretVault&gt;.</returns>
        public IDictionary<string, ISecretVault> AddModuleSecretVaultsToProvider<TSecretVault>(ISecretProvider provider, string secretProviderVaultsJsonPath, string caller)
            where TSecretVault : ISecretVault, new();

        /// <summary>
        /// Gets the database connection string.
        /// </summary>
        /// <typeparam name="TSecretVault">The type of the t secret vault.</typeparam>
        /// <param name="vault">The vault.</param>
        /// <param name="configuration">The configuration.</param>
        /// <param name="connectionStringFieldName">Name of the connection string field.</param>
        /// <param name="caller">The caller.</param>
        /// <param name="shouldLogConnectionString">if set to <c>true</c> [should log connection string].</param>
        /// <returns>System.String.</returns>
        public string GetDatabaseConnectionString<TSecretVault>(TSecretVault vault, IConfigurationRoot configuration, string connectionStringFieldName, string caller, bool shouldLogConnectionString = false)
            where TSecretVault : ISecretVault;


        /// <summary>
        /// Gets the secret vault identifier from setting.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        /// <param name="settingName">Name of the setting.</param>
        /// <returns>System.String.</returns>
        string GetSecretVaultIdentifierFromSetting(IConfigurationRoot configuration, string settingName);

    }
}