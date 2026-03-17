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
using Newtonsoft.Json.Converters;
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
    /// Class SecretVault.
    /// Implements the <see cref="Deploy.LaunchPad.Code.Config.ISecretVault" />
    /// </summary>
    /// <seealso cref="Deploy.LaunchPad.Code.Config.ISecretVault" />
    public partial class SecretVault : LaunchPadSecretFields, ISecretVault
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
        /// Gets or sets the name.
        /// </summary>
        /// <value>The name.</value>
        public virtual string Name { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>The description.</value>
        public virtual string Description { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public virtual SecretSource Source { get; set; } = SecretSource.None;

        public virtual bool IsWritable { get; set; } = false;

        /// <summary>
        /// Initializes a new instance of the <see cref="SecretVault"/> class.
        /// </summary>
        [SetsRequiredMembers]
        public SecretVault()
        {
            Name = string.Empty;
            Description = string.Empty;
            VaultId = string.Empty;
            var comparer = StringComparer.OrdinalIgnoreCase;
            Fields = new Dictionary<string, ISettingDefinition>(comparer);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SecretVault"/> class.
        /// </summary>
        [SetsRequiredMembers]
        public SecretVault(ILogger logger, string vaultId)
        {
            Name = vaultId;
            Description = string.Empty;
            VaultId = vaultId;
            var comparer = StringComparer.OrdinalIgnoreCase;
            Fields = new Dictionary<string, ISettingDefinition>(comparer);

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SecretVault"/> class.
        /// </summary>
        /// <param name="vaultId">The vault identifier.</param>
        /// <param name="vaultName">Name of the vault.</param>
        [SetsRequiredMembers]
        public SecretVault(ILogger logger, string vaultId, string vaultName)
        {
            Name = vaultName;
            VaultId = vaultId;
            Description = "Vault for " + vaultName;
            var comparer = StringComparer.OrdinalIgnoreCase;
            Fields = new Dictionary<string, ISettingDefinition>(comparer);

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SecretVault"/> class.
        /// </summary>
        /// <param name="vaultId">The vault identifier.</param>
        /// <param name="vaultName">Name of the vault.</param>
        /// <param name="fields">The fields.</param>
        /// <param name="description">The description.</param>
        [SetsRequiredMembers]
        public SecretVault(ILogger logger, string vaultId, string vaultName, IDictionary<string, ISettingDefinition> fields, string description = "")
        {
            Name = vaultName;
            VaultId = vaultId;
            if (description == string.Empty)
            {
                Description = "Vault for " + vaultName;
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
        public virtual Task<IDictionary<string, ISettingDefinition>> GetValuesForKeysAsync(IList<string> keys, string caller, bool keyIsCaseInsensitive = true)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Returns the set of all key value pairs, which are part of a given secret ARN
        /// The field names do not have to be known ahead of time.
        /// </summary>
        /// <param name="secretVault">The secret vault.</param>
        /// <param name="caller">The caller.</param>
        /// <returns>A Task&lt;IDictionary`2&gt; representing the asynchronous operation.</returns>
        public virtual Task<IDictionary<string, ISettingDefinition>> GetAllValuesAsync(string caller)
        {
            throw new NotImplementedException();
        }

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


        public virtual Task<string> GetValueOrNullForSettingSecretProviderDescriptorAsync(SettingSecretProviderDescriptor source, ISettingDefinition definition, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public virtual string GetValueOrNullForSettingSecretProviderDescriptor(SettingSecretProviderDescriptor source, ISettingDefinition definition)
        {
            throw new NotImplementedException();
        }

        public virtual ISettingDefinition CreateOrUpdateField(string originalSecretJson, string key, ISettingDefinition value, string caller)
        {
            if (IsWritable)
            {
                throw new NotImplementedException();
            }
            return null;
        }

        public virtual HttpStatusCode BatchUpdateFields(IDictionary<string, ISettingDefinition> fieldsToInsertOrUpdate, string caller)
        {
            return BatchUpdateFieldsAsync(fieldsToInsertOrUpdate, caller).Result;
        }

        public virtual Task<HttpStatusCode> BatchUpdateFieldsAsync(IDictionary<string, ISettingDefinition> fieldsToInsertOrUpdate, string caller)
        {
            if (IsWritable)
            {
                throw new NotImplementedException();
            }
            return Task.FromResult(HttpStatusCode.NotImplemented);
        }
    }
}