// ***********************************************************************
// Assembly         : Deploy.LaunchPad.Core
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 02-18-2023
// ***********************************************************************
// <copyright file="ISecretProvider.cs" company="Deploy Software Solutions, inc.">
//     2018-2024 Deploy Software Solutions, inc.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Castle.Core.Logging;
using Deploy.LaunchPad.Core.Configuration;
using Deploy.LaunchPad.Util;
using Deploy.LaunchPad.Util.Dependency;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Deploy.LaunchPad.Core.Secrets.Configuration
{
    /// <summary>
    /// Interface ISecretProvider
    /// </summary>
    public partial interface ISecretProvider :  ITransientDependency, ILaunchPadService
    {

        [NotMapped]
        [JsonIgnore]
        public IDictionary<string, ISettingDefinition> Secrets { get; }

        [NotMapped]
        [JsonIgnore]
        public IDictionary<string, ISecretVault> Vaults { get; }

        /// <summary>
        /// Used to populate secret vaults
        /// </summary>
        /// <param name="context">SecretProvider context</param>
        public abstract void LoadAllSecretVaultsFromConfigurationRoot(IConfigurationRoot configurationRoot, ISecretProviderContext context = null);

        // get methods

        // refresh method
        /// <summary>
        /// Refreshes the secret vault.
        /// </summary>
        /// <param name="vaultId">The vault identifier.</param>
        /// <param name="caller">The caller.</param>
        /// <returns>ISecretVault.</returns>
        public ISecretVault RefreshSecretVault(string vaultId, string caller);

        /// <summary>
        /// Refreshes the secret vault asynchronous.
        /// </summary>
        /// <param name="vaultId">The vault identifier.</param>
        /// <param name="caller">The caller.</param>
        /// <returns>Task&lt;ISecretVault&gt;.</returns>
        public Task<ISecretVault> RefreshSecretVaultAsync(string vaultId, string caller);

        /// <summary>
        /// Refreshes the secret vault.
        /// </summary>
        /// <param name="vault">The vault.</param>
        /// <param name="caller">The caller.</param>
        /// <returns>ISecretVault.</returns>
        public ISecretVault RefreshSecretVault(ISecretVault vault, string caller);

        /// <summary>
        /// Refreshes the secret vault asynchronous.
        /// </summary>
        /// <param name="vault">The vault.</param>
        /// <param name="caller">The caller.</param>
        /// <returns>Task&lt;ISecretVault&gt;.</returns>
        public Task<ISecretVault> RefreshSecretVaultAsync(ISecretVault vault, string caller);

        /// <summary>
        /// Refreshes all secret vaults.
        /// </summary>
        /// <param name="caller">The caller.</param>
        public void RefreshAllSecretVaults(string caller);


    }
}
