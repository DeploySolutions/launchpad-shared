using Castle.Core.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DeploySoftware.LaunchPad.Core.Config
{
    public abstract partial class SecretProviderBase : ISecretProvider
    {
        /// <summary>
        /// Contains an outer dictionary of "secret vaults". 
        /// Each secret vault contains an inner dictionary of string pairs representing a unique field contained within the vault, and the field's value. 
        /// The outer dictionary key is the unique identifer (such as Azure Key Vault identifier or AWS ARN) of the secret in which the secrets are stored.
        /// Note to implementers: Do not store or record this information!
        /// </summary>
        [NotMapped]
        [JsonIgnore]
        public virtual Dictionary<string, ISecretVault> SecretVaults { get; set; }

        public virtual string Id { get; set; }

        public virtual string Name { get; set; }

        public virtual string Description { get; set; }

        public virtual ILogger Logger { get; set; } = NullLogger.Instance;

        public SecretProviderBase()
        {
            SecretVaults = new Dictionary<string, ISecretVault>();
        }

        public SecretProviderBase(ILogger logger)
        {
            if (logger != null)
            {
                Logger = logger;
            }
            SecretVaults = new Dictionary<string, ISecretVault>();
        }

        // get methods

        public virtual string GetJsonFromSecretVault(ISecretVault secretVault, string caller)
        {
            return GetJsonFromSecretVaultAsync(secretVault, caller).Result;
        }

        public abstract Task<string> GetJsonFromSecretVaultAsync(ISecretVault secretVault, string caller);

        public virtual string GetValueFromSecretVault(string secretVaultIdentifier, string key,  string caller)
        {
            var secretVault = GetSecretVaultById(secretVaultIdentifier, caller);
            return GetValueFromSecretVaultAsync(secretVault, key, caller).Result;
        }
        /// <summary>
        /// Returns the text value of of a particular key, from a given secret ARN
        /// </summary>
        /// <param name="key"></param>
        /// <param name="secretVaultIdentifier"></param>
        /// <returns></returns>
        public string GetValueFromSecretVault(ISecretVault secretVault, string key, string caller)
        {
            return GetValueFromSecretVaultAsync(secretVault, key, caller).Result;
        }


        /// <summary>
        /// Returns the text value of of a particular key, from a given secret ARN
        /// </summary>
        /// <param name="key"></param>
        /// <param name="secretVaultIdentifier"></param>
        /// <returns></returns>
        public async Task<string> GetValueFromSecretVaultAsync(ISecretVault secretVault, string key, string caller)
        {
            string secretStringJson = await GetJsonFromSecretVaultAsync(secretVault, caller);
            string val = string.Empty;
            // Decrypts secret
            if (!string.IsNullOrEmpty(secretStringJson))
            {
                dynamic secretObj = JObject.Parse(secretStringJson);
                val = secretObj[key];
            }
            return val;
        }

        /// <summary>
        /// Returns the set of all key value pairs, which are part of a given secret ARN
        /// The field names do not have to be known ahead of time.
        /// </summary>
        /// <param name="secretVaultIdentifier">The ARN of the secret in which the fields are present</param>
        /// <returns></returns>
        public async Task<IDictionary<string, string>> GetAllValuesFromSecretVaultAsync(ISecretVault secretVault, string caller)
        {
            string secretStringJson = await GetJsonFromSecretVaultAsync(secretVault, caller);
            IDictionary<string, string> kvps = null;

            // Decrypt the secret
            if (!string.IsNullOrEmpty(secretStringJson))
            {
                kvps = new Dictionary<string, string>();
                dynamic secretJson = JValue.Parse(secretStringJson);
                // loop through the desired set of keys to find the corresponding values in the JSON
                foreach (Newtonsoft.Json.Linq.JProperty jproperty in secretJson)
                {
                    kvps.Add(jproperty.Name, jproperty.Value.ToString());
                }
            }
            return kvps;
        }

        public virtual ISecretVault GetSecretVaultById(string id, string caller)
        {
            return GetSecretVaultByIdAsync(id, caller).Result;
        }
        public abstract Task<ISecretVault> GetSecretVaultByIdAsync(string id, string caller);

        public virtual ISecretVault GetSecretVaultByVaultId(string vaultId, string caller)
        {
            return GetSecretVaultByVaultIdAsync(vaultId, caller).Result;
        }

        public abstract Task<ISecretVault> GetSecretVaultByVaultIdAsync(string vaultId, string caller);

        /// <summary>
        /// Returns the set of key value pairs for a given set of keys, which are part of a given secret vault's fields
        /// </summary>
        /// <param name="keys">The list of keys you are looking for</param>
        /// <param name="secretVault">The secret vault in which these keys are fields</param>
        /// <returns></returns>
        public virtual async Task<IDictionary<string, string>> FindValuesForKeysAsync(ISecretVault secretVault, IList<string> keys, string caller)
        {
            string secretStringJson = await GetJsonFromSecretVaultAsync(secretVault, caller);
            IDictionary<string, string> kvps = null;

            // Decrypt the secret
            if (!string.IsNullOrEmpty(secretStringJson))
            {
                dynamic secretObj = JObject.Parse(secretStringJson);
                kvps = new Dictionary<string, string>();
                // loop through the desired set of keys to find the corresponding values in the JSON
                foreach (string key in keys)
                {
                    string value = secretObj[key];
                    if (!string.IsNullOrEmpty(value))
                    {
                        kvps.Add(key, value);
                    }
                }
            }
            return kvps;
        }

        // Refresh methods
        public abstract ISecretVault RefreshSecretVault(string vaultId, string caller);

        public abstract Task<ISecretVault> RefreshSecretVaultAsync(string vaultId, string caller);

        public abstract ISecretVault RefreshSecretVault(ISecretVault secretVault, string caller);

        public abstract Task<ISecretVault> RefreshSecretVaultAsync(ISecretVault secretVault, string caller);

        public void RefreshAllSecretVaults(string caller)
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
        /// <param name="key">The field within the secret to update</param>
        /// <param name="value">The value to update for the given key</param>
        /// <param name="secretVaultIdentifier">The full secret ARN</param>
        /// <returns>A status code with the result of the request</returns>
        public abstract HttpStatusCode UpdateFieldsInSecretVault(ISecretVault secretVault, IDictionary<string, string> fieldsToInsertOrUpdate, string caller);

        /// <summary>
        /// Writes the text value of a particular key, to a given secret ARN
        /// </summary>
        /// <param name="key">The field within the secret to update</param>
        /// <param name="value">The value to update for the given key</param>
        /// <param name="secretVaultIdentifier">The full secret ARN</param>
        /// <returns>A status code with the result of the request</returns>
        public abstract Task<HttpStatusCode> UpdateFieldsInSecretVaultAsync(ISecretVault secretVault, IDictionary<string, string> fieldsToInsertOrUpdate, string caller);

        public abstract string CreateOrUpdateFieldInSecretVault(ISecretVault secretVault, string originalSecretJson, string key, string value, string caller);

        class Root
        {
            [JsonProperty("data")]
            public List<JObject> Data { get; set; }
        }

    }
}
