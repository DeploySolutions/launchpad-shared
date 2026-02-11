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
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net;
using System.Threading.Tasks;

namespace Deploy.LaunchPad.Code.Config
{
    /// <summary>
    /// Interface ISecretProvider
    /// </summary>
    public partial interface ISecretProvider
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
        /// Gets the type.
        /// </summary>
        /// <value>The type.</value>
        public string Type { get; }

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

        /// <summary>
        /// Gets the secret vault by vault identifier.
        /// </summary>
        /// <param name="vaultId">The vault identifier.</param>
        /// <param name="caller">The caller.</param>
        /// <returns>ISecretVault.</returns>
        public ISecretVault GetSecretVaultByVaultId(string vaultId, string caller);

        /// <summary>
        /// Gets the secret vault by vault identifier asynchronous.
        /// </summary>
        /// <param name="vaultId">The vault identifier.</param>
        /// <param name="caller">The caller.</param>
        /// <returns>Task&lt;ISecretVault&gt;.</returns>
        public Task<ISecretVault> GetSecretVaultByVaultIdAsync(string vaultId, string caller);

        // Vault-related methods
        /// <summary>
        /// Gets the json from secret vault.
        /// </summary>
        /// <param name="secretVault">The secret vault.</param>
        /// <param name="caller">The caller.</param>
        /// <returns>System.String.</returns>
        public string GetJsonFromSecretVault(ISecretVault secretVault, string caller, bool keyIsCaseInsensitive = true);
        /// <summary>
        /// Gets the json from secret vault asynchronous.
        /// </summary>
        /// <param name="secretVault">The secret vault.</param>
        /// <param name="caller">The caller.</param>
        /// <returns>Task&lt;System.String&gt;.</returns>
        public Task<string> GetJsonFromSecretVaultAsync(ISecretVault secretVault, string caller, bool keyIsCaseInsensitive = true);

        /// <summary>
        /// Gets the value from secret vault.
        /// </summary>
        /// <param name="secretVault">The secret vault.</param>
        /// <param name="key">The key.</param>
        /// <param name="caller">The caller.</param>
        /// <returns>System.String.</returns>
        public string GetValueFromSecretVault(ISecretVault secretVault, string key, string caller, bool keyIsCaseInsensitive = true);
        /// <summary>
        /// Gets the value from secret vault asynchronous.
        /// </summary>
        /// <param name="secretVault">The secret vault.</param>
        /// <param name="key">The key.</param>
        /// <param name="caller">The caller.</param>
        /// <returns>Task&lt;System.String&gt;.</returns>
        public Task<string> GetValueFromSecretVaultAsync(ISecretVault secretVault, string key, string caller, bool keyIsCaseInsensitive = true);


        /// <summary>
        /// Finds the values for keys asynchronous.
        /// </summary>
        /// <param name="secretVault">The secret vault.</param>
        /// <param name="keys">The keys.</param>
        /// <param name="caller">The caller.</param>
        /// <returns>Task&lt;IDictionary&lt;System.String, System.String&gt;&gt;.</returns>
        public Task<IDictionary<string, string>> FindValuesForKeysAsync(ISecretVault secretVault, IList<string> keys, string caller, bool keyIsCaseInsensitive = true);

        /// <summary>
        /// Gets all values from secret vault asynchronous.
        /// </summary>
        /// <param name="secretVault">The secret vault.</param>
        /// <param name="caller">The caller.</param>
        /// <returns>Task&lt;IDictionary&lt;System.String, System.String&gt;&gt;.</returns>
        public Task<IDictionary<string, string>> GetAllValuesFromSecretVaultAsync(ISecretVault secretVault, string caller);

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


        // create or update methods

        /// <summary>
        /// Creates the or update field in secret vault.
        /// </summary>
        /// <param name="secretVault">The secret vault.</param>
        /// <param name="originalSecretJson">The original secret json.</param>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <param name="caller">The caller.</param>
        /// <returns>System.String.</returns>
        public string CreateOrUpdateFieldInSecretVault(ISecretVault secretVault, string originalSecretJson, string key, string value, string caller);

        /// <summary>
        /// Writes the text value of a particular key, to a given secret ARN
        /// </summary>
        /// <param name="secretVault">The secret vault.</param>
        /// <param name="fieldsToInsertOrUpdate">The fields to insert or update.</param>
        /// <param name="caller">The caller.</param>
        /// <returns>A status code with the result of the request</returns>
        public abstract HttpStatusCode UpdateFieldsInSecretVault(ISecretVault secretVault, IDictionary<string, string> fieldsToInsertOrUpdate, string caller);

        /// <summary>
        /// Writes the text value of a particular key, to a given secret vault
        /// </summary>
        /// <param name="secretVault">The secret vault in which the field is stored</param>
        /// <param name="fieldsToInsertOrUpdate">The fields to insert or update.</param>
        /// <param name="caller">The caller.</param>
        /// <returns>A status code with the result of the request</returns>
        public Task<HttpStatusCode> UpdateFieldsInSecretVaultAsync(ISecretVault secretVault, IDictionary<string, string> fieldsToInsertOrUpdate, string caller);

    }
}
