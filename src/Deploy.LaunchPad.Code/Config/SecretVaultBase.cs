// ***********************************************************************
// Assembly         : Deploy.LaunchPad.Core
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 01-08-2023
// ***********************************************************************
// <copyright file="SecretVaultBase.cs" company="Deploy Software Solutions, inc.">
//     2018-2024 Deploy Software Solutions, inc.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;

namespace Deploy.LaunchPad.Code.Config
{
    /// <summary>
    /// Class SecretVaultBase.
    /// Implements the <see cref="Deploy.LaunchPad.Code.Config.ISecretVault" />
    /// </summary>
    /// <seealso cref="Deploy.LaunchPad.Code.Config.ISecretVault" />
    public abstract partial class SecretVaultBase : ISecretVault
    {
        /// <summary>
        /// Gets or sets the fields.
        /// </summary>
        /// <value>The fields.</value>
        public virtual IDictionary<string, string> Fields { get; set; }
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
        /// Gets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        public virtual string Id
        {
            get
            {
                return ProviderId + "." + VaultId;
            }
        }

        /// <summary>
        /// Gets or sets the vault identifier.
        /// </summary>
        /// <value>The vault identifier.</value>
        public virtual string VaultId { get; set; }
        /// <summary>
        /// Gets or sets the provider identifier.
        /// </summary>
        /// <value>The provider identifier.</value>
        public virtual string ProviderId { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="SecretVaultBase"/> class.
        /// </summary>
        public SecretVaultBase()
        {
            Name = string.Empty;
            Description = string.Empty;
            ProviderId = string.Empty;
            VaultId = string.Empty;
            var comparer = StringComparer.OrdinalIgnoreCase;
            Fields = new Dictionary<string, string>(comparer);

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SecretVaultBase"/> class.
        /// </summary>
        /// <param name="providerId">The provider identifier.</param>
        /// <param name="vaultId">The vault identifier.</param>
        /// <param name="vaultName">Name of the vault.</param>
        public SecretVaultBase(string providerId, string vaultId, string vaultName)
        {
            Name = vaultName;
            ProviderId = providerId;
            VaultId = vaultId;
            Description = "Vault for " + providerId + "." + vaultName;
            var comparer = StringComparer.OrdinalIgnoreCase;
            Fields = new Dictionary<string, string>(comparer);

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SecretVaultBase"/> class.
        /// </summary>
        /// <param name="providerId">The provider identifier.</param>
        /// <param name="vaultId">The vault identifier.</param>
        /// <param name="vaultName">Name of the vault.</param>
        /// <param name="fields">The fields.</param>
        /// <param name="description">The description.</param>
        public SecretVaultBase(string providerId, string vaultId, string vaultName, IDictionary<string, string> fields, string description = "")
        {
            Name = vaultName;
            ProviderId = providerId;
            VaultId = vaultId;
            if (description == string.Empty)
            {
                Description = "Vault for " + providerId + "." + vaultName;
            }
            else
            {
                Description = description;
            }
            Fields = fields;
        }

        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="caller">The caller.</param>
        /// <returns>System.String.</returns>
        public virtual string GetValue(string key, string caller, bool keyIsCaseInsensitive = true)
        {
            string value = string.Empty;
            if(keyIsCaseInsensitive)
            {
                value = Fields.FirstOrDefault(k => k.Key.ToLower() == key.ToLower()).Value;
            }
            else
            {
                value = Fields.FirstOrDefault(k => k.Key == key).Value;
            }
            return value;
        }

        /// <summary>
        /// Finds the values for keys.
        /// </summary>
        /// <param name="keys">The keys.</param>
        /// <param name="caller">The caller.</param>
        /// <returns>IDictionary&lt;System.String, System.String&gt;.</returns>
        public virtual IDictionary<string, string> FindValuesForKeys(IList<string> keys, string caller, bool keyIsCaseInsensitive = true)
        {
            IDictionary<string, string> kvps = new Dictionary<string, string>();
            // loop through the desired set of keys to find the corresponding values in the JSON
            if (keyIsCaseInsensitive)
            {
                kvps = (IDictionary<string, string>)keys.Where(k => Fields.ContainsKey(k.ToLower()));
            }
            else
            {
                kvps = (IDictionary<string, string>)keys.Where(k => Fields.ContainsKey(k));
            }

            return kvps;
        }

    }
}