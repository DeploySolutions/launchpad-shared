// ***********************************************************************
// Assembly         : Deploy.LaunchPad.Core
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 02-18-2023
// ***********************************************************************
// <copyright file="SecretProviderBase.cs" company="Deploy Software Solutions, inc.">
//     2018-2024 Deploy Software Solutions, inc.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Castle.Core.Logging;
using Deploy.LaunchPad.Core.Configuration;
using Deploy.LaunchPad.Util;
using Deploy.LaunchPad.Util.Dependency;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Deploy.LaunchPad.Core.Secrets.Configuration
{
    /// <summary>
    /// Class SecretProviderBase.
    /// Implements the <see cref="Code.Config.ISecretProvider" />
    /// </summary>
    /// <seealso cref="Code.Config.ISecretProvider" />
    public abstract partial class SecretProviderBase :  LaunchPadServiceBase, ISecretProvider, ITransientDependency
    {
        protected readonly IDictionary<string, ISettingDefinition> _secrets = new Dictionary<string, ISettingDefinition>();

        /// <summary>
        /// Contains an outer dictionary of "secret vaults".
        /// Each secret vault contains an inner dictionary of string pairs representing a unique field contained within the vault, and the field's value.
        /// The outer dictionary key is the unique identifer (such as Azure Key Vault identifier or AWS ARN) of the secret in which the secrets are stored.
        /// Note to implementers: Do not store or record this information!
        /// </summary>
        /// <value>The secret vaults.</value>
        [NotMapped]
        [JsonIgnore]
        public virtual Dictionary<string, ISecretVault> SecretVaults { get; set; }

        /// <summary>
        /// Gets a list of all secrets grouped by provider. These are also listed individually in the Providers property.
        /// </summary>
        public virtual IDictionary<string, ISettingDefinition> Secrets { get { return _secrets; } }

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        public virtual string Id { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        public virtual string Name { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>The description.</value>
        public virtual string Description { get; set; }

        /// <summary>
        /// Gets or sets the logger.
        /// </summary>
        /// <value>The logger.</value>
        public virtual ILogger Logger { get; set; } = NullLogger.Instance;

        /// <summary>
        /// Initializes a new instance of the <see cref="SecretProviderBase"/> class.
        /// </summary>
        public SecretProviderBase()
        {
            SecretVaults = new Dictionary<string, ISecretVault>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SecretProviderBase"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        public SecretProviderBase(ILogger logger)
        {
            if (logger != null)
            {
                Logger = logger;
            }
            SecretVaults = new Dictionary<string, ISecretVault>();
        }
        
        /// <summary>
        /// Used to populate secret vaults
        /// </summary>
        /// <param name="context">SecretProvider context</param>
        public abstract void LoadSecretVault<TVault>(IConfigurationRoot configurationRoot)
            where TVault : SecretVault;

        /// <summary>
        /// Used to populate secret vaults
        /// </summary>
        /// <param name="context">SecretProvider context</param>
        public abstract void LoadSecretVault<TVault>(IConfigurationRoot configurationRoot, ISecretProviderContext context)
            where TVault : SecretVault;


        // get methods

        /// <summary>
        /// Gets the secret vault by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="caller">The caller.</param>
        /// <returns>ISecretVault.</returns>
        public virtual ISecretVault GetSecretVaultById(string id, string caller)
        {
            return GetSecretVaultByIdAsync(id, caller).Result;
        }
        /// <summary>
        /// Gets the secret vault by identifier asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="caller">The caller.</param>
        /// <returns>Task&lt;ISecretVault&gt;.</returns>
        public abstract Task<ISecretVault> GetSecretVaultByIdAsync(string id, string caller);

        
        // Refresh methods
        /// <summary>
        /// Refreshes the secret vault.
        /// </summary>
        /// <param name="vaultId">The vault identifier.</param>
        /// <param name="caller">The caller.</param>
        /// <returns>ISecretVault.</returns>
        public abstract ISecretVault RefreshSecretVault(string vaultId, string caller);

        /// <summary>
        /// Refreshes the secret vault asynchronous.
        /// </summary>
        /// <param name="vaultId">The vault identifier.</param>
        /// <param name="caller">The caller.</param>
        /// <returns>Task&lt;ISecretVault&gt;.</returns>
        public abstract Task<ISecretVault> RefreshSecretVaultAsync(string vaultId, string caller);

        /// <summary>
        /// Refreshes the secret vault.
        /// </summary>
        /// <param name="secretVault">The secret vault.</param>
        /// <param name="caller">The caller.</param>
        /// <returns>ISecretVault.</returns>
        public abstract ISecretVault RefreshSecretVault(ISecretVault secretVault, string caller);

        /// <summary>
        /// Refreshes the secret vault asynchronous.
        /// </summary>
        /// <param name="secretVault">The secret vault.</param>
        /// <param name="caller">The caller.</param>
        /// <returns>Task&lt;ISecretVault&gt;.</returns>
        public abstract Task<ISecretVault> RefreshSecretVaultAsync(ISecretVault secretVault, string caller);

        /// <summary>
        /// Refreshes all secret vaults.
        /// </summary>
        /// <param name="caller">The caller.</param>
        public virtual void RefreshAllSecretVaults(string caller)
        {
            foreach (var vault in SecretVaults.Values)
            {
                // update the vaults in our dictionary
                RefreshSecretVault(vault, caller);
            }
        }

    }
}
