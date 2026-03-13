// ***********************************************************************
// Assembly         : Deploy.LaunchPad.Core
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 01-08-2023
// ***********************************************************************
// <copyright file="ISecretVault.cs" company="Deploy Software Solutions, inc.">
//     2018-2024 Deploy Software Solutions, inc.
// </copyright>
// <summary></summary>
// ***********************************************************************

using Deploy.LaunchPad.Core.Configuration;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Deploy.LaunchPad.Core.Secrets
{
    /// <summary>
    /// Interface ISecretVault
    /// </summary>
    public partial interface ISecretVault : ILaunchPadSecretFields
    {
        /// <summary>
        /// Gets or sets the vault identifier.
        /// </summary>
        /// <value>The vault identifier.</value>
        public string VaultId { get; set; }

        /// <summary>
        /// Gets or sets the provider identifier.
        /// </summary>
        /// <value>The provider identifier.</value>
        public string ProviderId { get; set; }

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

        public SecretSource Source { get; set; }

        // Get methods
        Task<string?> GetValueOrNullForSettingSecretProviderDescriptorAsync(
            SettingSecretProviderDescriptor source,
            ISettingDefinition definition,
            CancellationToken cancellationToken = default);

        string? GetValueOrNullForSettingSecretProviderDescriptor(
            SettingSecretProviderDescriptor source,
            ISettingDefinition definition);

        /// <summary>
        /// Finds the values for keys asynchronous.
        /// </summary>
        /// <param name="secretVault">The secret vault.</param>
        /// <param name="keys">The keys.</param>
        /// <param name="caller">The caller.</param>
        /// <returns>Task&lt;IDictionary&lt;System.String, System.String&gt;&gt;.</returns>
        public Task<IDictionary<string, ISettingDefinition>> GetValuesForKeysAsync(IList<string> keys, string caller, bool keyIsCaseInsensitive = true);

        /// <summary>
        /// Gets all values from secret vault asynchronous.
        /// </summary>
        /// <param name="secretVault">The secret vault.</param>
        /// <param name="caller">The caller.</param>
        /// <returns>Task&lt;IDictionary&lt;System.String, System.String&gt;&gt;.</returns>
        public Task<IDictionary<string, ISettingDefinition>> GetAllValuesAsync(string caller);


        // create or update methods

        /// <summary>
        /// Creates the or update field in secret vault.
        /// </summary>
        /// <param name="originalSecretJson">The original secret json.</param>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <param name="caller">The caller.</param>
        /// <returns>System.String.</returns>
        public ISettingDefinition CreateOrUpdateField(string originalSecretJson, string key, ISettingDefinition value, string caller);

        /// <summary>
        /// Writes the text value of a particular key
        /// </summary>
        /// <param name="fieldsToInsertOrUpdate">The fields to insert or update.</param>
        /// <param name="caller">The caller.</param>
        /// <returns>A status code with the result of the request</returns>
        public abstract HttpStatusCode BatchUpdateFields(IDictionary<string, ISettingDefinition> fieldsToInsertOrUpdate, string caller);

        /// <summary>
        /// Writes the text value of a particular key
        /// </summary>
        /// <param name="fieldsToInsertOrUpdate">The fields to insert or update.</param>
        /// <param name="caller">The caller.</param>
        /// <returns>A status code with the result of the request</returns>
        public Task<HttpStatusCode> BatchUpdateFieldsAsync(IDictionary<string, ISettingDefinition> fieldsToInsertOrUpdate, string caller);

    }
}