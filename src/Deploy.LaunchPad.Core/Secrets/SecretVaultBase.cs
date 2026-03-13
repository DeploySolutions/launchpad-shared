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
using Castle.Core.Logging;
using Deploy.LaunchPad.Core.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Deploy.LaunchPad.Core.Secrets
{
    /// <summary>
    /// Class SecretVaultBase.
    /// Implements the <see cref="Deploy.LaunchPad.Code.Config.ISecretVault" />
    /// </summary>
    /// <seealso cref="Deploy.LaunchPad.Code.Config.ISecretVault" />
    public abstract partial class SecretVaultBase : LaunchPadSecretFields, ISecretVault
    {

        /// <summary>
        /// Gets or sets the logger.
        /// </summary>
        /// <value>The logger.</value>
        public virtual ILogger Logger { get; set; } = NullLogger.Instance;

        /// <summary>
        /// Gets or sets the vault identifier.
        /// </summary>
        /// <value>The vault identifier.</value>
        public required virtual string VaultId { get; set; }

        /// <summary>
        /// Gets or sets the provider identifier.
        /// </summary>
        /// <value>The provider identifier.</value>
        public virtual string? ProviderId { get; set; }

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

        public virtual SecretSource Source { get; set; } = SecretSource.None;

        public virtual bool IsReadOnly { get; set; } = true;

        /// <summary>
        /// Initializes a new instance of the <see cref="SecretVaultBase"/> class.
        /// </summary>
        [SetsRequiredMembers]
        public SecretVaultBase(ILogger logger, string vaultId)
        {
            Name = vaultId;
            Description = string.Empty;
            VaultId = vaultId;
            var comparer = StringComparer.OrdinalIgnoreCase;
            Fields = new Dictionary<string, ISettingDefinition>(comparer);

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SecretVaultBase"/> class.
        /// </summary>
        /// <param name="vaultId">The vault identifier.</param>
        /// <param name="vaultName">Name of the vault.</param>
        [SetsRequiredMembers]
        public SecretVaultBase(ILogger logger, string vaultId, string vaultName)
        {
            Name = vaultName;
            VaultId = vaultId;
            Description = "Vault for " + vaultName;
            var comparer = StringComparer.OrdinalIgnoreCase;
            Fields = new Dictionary<string, ISettingDefinition>(comparer);

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SecretVaultBase"/> class.
        /// </summary>
        /// <param name="providerId">The provider identifier.</param>
        /// <param name="vaultId">The vault identifier.</param>
        /// <param name="vaultName">Name of the vault.</param>
        /// <param name="fields">The fields.</param>
        /// <param name="description">The description.</param>
        [SetsRequiredMembers]
        public SecretVaultBase(ILogger logger, string vaultId, string vaultName, string providerId, IDictionary<string, ISettingDefinition> fields, string description = "")
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
        /// Returns the set of key value pairs for a given set of keys, which are part of a given secret vault's fields
        /// </summary>
        /// <param name="secretVault">The secret vault in which these keys are fields</param>
        /// <param name="keys">The list of keys you are looking for</param>
        /// <param name="caller">The caller.</param>
        /// <returns>A Task&lt;IDictionary`2&gt; representing the asynchronous operation.</returns>
        public abstract Task<IDictionary<string, ISettingDefinition>> GetValuesForKeysAsync(IList<string> keys, string caller, bool keyIsCaseInsensitive = true);
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
        public abstract Task<IDictionary<string, ISettingDefinition>> GetAllValuesAsync(string caller);
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


        public abstract Task<string> GetValueOrNullForSettingSecretProviderDescriptorAsync(SettingSecretProviderDescriptor source, ISettingDefinition definition, CancellationToken cancellationToken = default);
        public abstract string GetValueOrNullForSettingSecretProviderDescriptor(SettingSecretProviderDescriptor source, ISettingDefinition definition);
        public abstract ISettingDefinition GetValue(string key, string caller, bool keyIsCaseInsensitive = true);
        public abstract Task<ISettingDefinition> GetValueAsync(string key, string caller, bool keyIsCaseInsensitive = true);
        public abstract ISettingDefinition CreateOrUpdateField(string originalSecretJson, string key, ISettingDefinition value, string caller);
        public abstract HttpStatusCode BatchUpdateFields(IDictionary<string, ISettingDefinition> fieldsToInsertOrUpdate, string caller);
        public abstract Task<HttpStatusCode> BatchUpdateFieldsAsync(IDictionary<string, ISettingDefinition> fieldsToInsertOrUpdate, string caller);
    }
}