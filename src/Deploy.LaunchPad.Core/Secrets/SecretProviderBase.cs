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
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Deploy.LaunchPad.Core.Secrets
{
    /// <summary>
    /// Class SecretProviderBase.
    /// Implements the <see cref="Deploy.LaunchPad.Code.Config.ISecretProvider" />
    /// </summary>
    /// <seealso cref="Deploy.LaunchPad.Code.Config.ISecretProvider" />
    public abstract partial class SecretProviderBase : ISecretProvider
    {
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

        public virtual SecretProviderType ProviderType { get; set; } = SecretProviderType.UserSecrets;

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

        public abstract Task<string?> GetValueOrNullForSettingSecretProviderDescriptorAsync(
            SettingSecretProviderDescriptor source,
            ISettingDefinition definition,
            CancellationToken cancellationToken = default);

        public abstract string? GetValueOrNullForSettingSecretProviderDescriptor(
            SettingSecretProviderDescriptor source,
            ISettingDefinition definition);

        /// <summary>
        /// Gets the value from secret vault.
        /// </summary>
        /// <param name="secretVaultIdentifier">The secret vault identifier.</param>
        /// <param name="key">The key.</param>
        /// <param name="caller">The caller.</param>
        /// <returns>System.String.</returns>
        public abstract ISettingDefinition GetValueFromSecretVault(ISecretVault secretVault, string key, string caller, bool keyIsCaseInsensitive = true);

        /// <summary>
        /// Returns the text value of of a particular key, from a given secret key
        /// </summary>
        /// <param name="secretVault">The secret vault.</param>
        /// <param name="key">The key.</param>
        /// <param name="caller">The caller.</param>
        /// <returns>System.String.</returns>
        public abstract Task<ISettingDefinition> GetValueFromSecretVaultAsync(ISecretVault secretVault, string key, string caller, bool keyIsCaseInsensitive = true);

        /// <summary>
        /// Returns the set of key value pairs for a given set of keys, which are part of a given secret vault's fields
        /// </summary>
        /// <param name="secretVault">The secret vault in which these keys are fields</param>
        /// <param name="keys">The list of keys you are looking for</param>
        /// <param name="caller">The caller.</param>
        /// <returns>A Task&lt;IDictionary`2&gt; representing the asynchronous operation.</returns>
        public abstract Task<IDictionary<string, ISettingDefinition>> GetValuesForKeysAsync(ISecretVault secretVault, IList<string> keys, string caller, bool keyIsCaseInsensitive = true);
        //{
        //    string secretStringJson = await GetJsonFromSecretVaultAsync(secretVault, caller, keyIsCaseInsensitive);
        //    IDictionary<string, ISettingDefinition> kvps = null;

        //    // Decrypt the secret
        //    if (!string.IsNullOrEmpty(secretStringJson))
        //    {
        //        dynamic secretObj = JObject.Parse(secretStringJson);
        //        kvps = new Dictionary<string, ISettingDefinition>();
        //        // loop through the desired set of keys to find the corresponding values in the JSON
        //        foreach (string key in keys)
        //        {
        //            ISettingDefinition value = null;
        //            if (keyIsCaseInsensitive)
        //            {
        //                value = secretObj[key.ToLower()];
        //            }
        //            else
        //            {
        //                value = secretObj[key];
        //            }
        //            if (value != null)
        //            {
        //                kvps.Add(key, value);
        //            }
        //        }
        //    }
        //    return kvps;
        //}

        /// <summary>
        /// Returns the set of all key value pairs, which are part of a given secret ARN
        /// The field names do not have to be known ahead of time.
        /// </summary>
        /// <param name="secretVault">The secret vault.</param>
        /// <param name="caller">The caller.</param>
        /// <returns>A Task&lt;IDictionary`2&gt; representing the asynchronous operation.</returns>
        public abstract  Task<IDictionary<string, ISettingDefinition>> GetAllValuesFromSecretVaultAsync(ISecretVault secretVault, string caller);
        //{
        //    string secretStringJson = await GetJsonFromSecretVaultAsync(secretVault, caller);
        //    IDictionary<string, ISettingDefinition> kvps = null;

        //    // Decrypt the secret
        //    if (!string.IsNullOrEmpty(secretStringJson))
        //    {
        //        kvps = new Dictionary<string, ISettingDefinition>();
        //        dynamic secretJson = JValue.Parse(secretStringJson);
        //        // loop through the desired set of keys to find the corresponding values in the JSON
        //        foreach (Newtonsoft.Json.Linq.JProperty jproperty in secretJson)
        //        {
        //            ISettingDefinition settingDefinition = new SettingDefinition
        //            (jproperty.Name, jproperty.Value.ToString(), null,null,null, SettingScopes.Application, false,false,null);
        //            kvps.Add(jproperty.Name, settingDefinition);
        //        }
        //    }
        //    return kvps;
        //}

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


        // create or update methods

        /// <summary>
        /// Writes the text value of a particular key, to a given secret ARN
        /// </summary>
        /// <param name="secretVault">The secret vault.</param>
        /// <param name="fieldsToInsertOrUpdate">The fields to insert or update.</param>
        /// <param name="caller">The caller.</param>
        /// <returns>A status code with the result of the request</returns>
        public abstract HttpStatusCode UpdateFieldsInSecretVault(ISecretVault secretVault, IDictionary<string, ISettingDefinition> fieldsToInsertOrUpdate, string caller);

        /// <summary>
        /// Writes the text value of a particular key, to a given secret ARN
        /// </summary>
        /// <param name="secretVault">The secret vault in which the field is stored</param>
        /// <param name="fieldsToInsertOrUpdate">The fields to insert or update.</param>
        /// <param name="caller">The caller.</param>
        /// <returns>A status code with the result of the request</returns>
        public abstract Task<HttpStatusCode> UpdateFieldsInSecretVaultAsync(ISecretVault secretVault, IDictionary<string, ISettingDefinition> fieldsToInsertOrUpdate, string caller);

        /// <summary>
        /// Creates the or update field in secret vault.
        /// </summary>
        /// <param name="secretVault">The secret vault.</param>
        /// <param name="originalSecretJson">The original secret json.</param>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <param name="caller">The caller.</param>
        /// <returns>ISettingDefinition.</returns>
        public abstract ISettingDefinition CreateOrUpdateFieldInSecretVault(ISecretVault secretVault, string originalSecretJson, string key, ISettingDefinition value, string caller);

        
        /// <summary>
        /// Class Root.
        /// </summary>
        class Root
        {
            /// <summary>
            /// Gets or sets the data.
            /// </summary>
            /// <value>The data.</value>
            [JsonProperty("data")]
            public List<JObject> Data { get; set; }
        }

    }
}
