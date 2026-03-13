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
using Deploy.LaunchPad.Util.Dependency;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading.Tasks;

namespace Deploy.LaunchPad.Core.Secrets
{
    /// <summary>
    /// Interface ISecretProvider
    /// </summary>
    public partial interface ISecretProvider :  ITransientDependency
    {

        /// <summary>
        /// Gets or sets the logger.
        /// </summary>
        /// <value>The logger.</value>
        public ILogger Logger { get; set; }

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>The description.</value>
        public string Description { get; set; }

        /// <summary>
        /// Contains a dictionary of "secret vaults", keyed using the friendly name of the vault.
        /// Each secret vault contains within it an inner dictionary of key value pairs representing a unique field contained within the vault, and the field's value.
        /// The secret vault also contains the unique identifer (such as Azure Key Vault identifier or AWS ARN) of the secret in which the secrets are stored.
        /// Note to implementers: Do not store or record this information!
        /// </summary>
        /// <value>The secret vaults.</value>
        [NotMapped]
        public Dictionary<string, ISecretVault> SecretVaults { get; set; }

        /// <summary>
        /// Used to populate secret vaults
        /// </summary>
        /// <param name="context">SecretProvider context</param>
        public void PopulateSecretVaults(ISecretProviderContext context);

        // get methods


        /// <summary>
        /// Gets the secret vault by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="caller">The caller.</param>
        /// <returns>ISecretVault.</returns>
        public ISecretVault GetSecretVaultById(string id, string caller);

        /// <summary>
        /// Gets the secret vault by identifier asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="caller">The caller.</param>
        /// <returns>Task&lt;ISecretVault&gt;.</returns>
        public Task<ISecretVault> GetSecretVaultByIdAsync(string id, string caller);

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
