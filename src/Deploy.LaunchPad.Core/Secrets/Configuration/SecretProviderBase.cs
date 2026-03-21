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

        protected readonly IDictionary<string, ISecretVault> _vaults = new Dictionary<string, ISecretVault>();


        public virtual IDictionary<string, ISettingDefinition> Secrets { get { return _secrets; } }

        public virtual IDictionary<string, ISecretVault> Vaults { get { return _vaults; } }


        /// <summary>
        /// Initializes a new instance of the <see cref="SecretProviderBase"/> class.
        /// </summary>
        public SecretProviderBase()
        {
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
            foreach (var vault in Vaults.Values)
            {
                // update the vaults in our dictionary
                RefreshSecretVault(vault, caller);
            }
        }

    }
}
