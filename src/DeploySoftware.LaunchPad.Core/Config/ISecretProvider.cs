using Castle.Core.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DeploySoftware.LaunchPad.Core.Config
{
    public interface ISecretProvider<TSecretVault>
        where TSecretVault : ISecretVault
    {
        
        public ILogger Logger { get; set; }

        public string Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        /// <summary>
        /// Contains a dictionary of "secret vaults", keyed using the friendly name of the vault. 
        /// Each secret vault contains within it an inner dictionary of key value pairs representing a unique field contained within the vault, and the field's value. 
        /// The secret vault also contains the unique identifer (such as Azure Key Vault identifier or AWS ARN) of the secret in which the secrets are stored.
        /// Note to implementers: Do not store or record this information!
        /// </summary>
        [NotMapped]
        public Dictionary<string, TSecretVault> SecretVaults { get; set; }

        // get methods


        public ISecretVault GetSecretVaultById(string id, string caller);

        public Task<ISecretVault> GetSecretVaultByIdAsync(string id, string caller);

        public ISecretVault GetSecretVaultByVaultId(string vaultId, string caller);

        public Task<ISecretVault> GetSecretVaultByVaultIdAsync(string vaultId, string caller);

        // Vault-related methods
        public string GetJsonFromSecretVault(ISecretVault secretVault, string caller);
        public Task<string> GetJsonFromSecretVaultAsync(ISecretVault secretVault, string caller);

        public string GetValueFromSecretVault(ISecretVault secretVault, string key, string caller);
        public Task<string> GetValueFromSecretVaultAsync(ISecretVault secretVault, string key, string caller);


        public  Task<IDictionary<string, string>> FindValuesForKeysAsync(ISecretVault secretVault, IList<string> keys, string caller);

        public Task<IDictionary<string, string>> GetAllValuesFromSecretVaultAsync(ISecretVault secretVault, string caller);

        // refresh method
        public ISecretVault RefreshSecretVault(string vaultId, string caller);

        public Task<ISecretVault> RefreshSecretVaultAsync(string vaultId, string caller);

        public ISecretVault RefreshSecretVault(ISecretVault vault, string caller);

        public Task<ISecretVault> RefreshSecretVaultAsync(ISecretVault vault, string caller);

        public void RefreshAllSecretVaults(string caller);

        
        // create or update methods

        public string CreateOrUpdateFieldInSecretVault(ISecretVault secretVault, string originalSecretJson, string key, string value, string caller);

        /// <summary>
        /// Writes the text value of a particular key, to a given secret ARN
        /// </summary>
        /// <param name="key">The field within the secret to update</param>
        /// <param name="value">The value to update for the given key</param>
        /// <param name="secretVaultIdentifier">The full secret ARN</param>
        /// <returns>A status code with the result of the request</returns>
        public abstract HttpStatusCode UpdateFieldsInSecretVault(ISecretVault secretVault, IDictionary<string, string> fieldsToInsertOrUpdate, string caller);

        /// <summary>
        /// Writes the text value of a particular key, to a given secret vault
        /// </summary>
        /// <param name="key">The field within the secret to update</param>
        /// <param name="value">The value to update for the given key</param>
        /// <param name="secretVault">The secret vault in which the field is stored</param>
        /// <returns>A status code with the result of the request</returns>
        public Task<HttpStatusCode> UpdateFieldsInSecretVaultAsync(ISecretVault secretVault, IDictionary<string, string> fieldsToInsertOrUpdate, string caller);

    }
}
