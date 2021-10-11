using Abp.Dependency;
using Castle.Core.Logging;
using DeploySoftware.LaunchPad.Core.Util;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DeploySoftware.LaunchPad.Core.Configuration
{
    public abstract partial class SecretHelper : HelperBase, ISecretHelper, ISingletonDependency
    {
        public SecretHelper() : base()
        {
        }

        public SecretHelper(ILogger logger) : base(logger)
        {
        }

        public virtual string GetJsonFromSecret(string secretVaultIdentifier)
        {
            return GetJsonFromSecretAsync(secretVaultIdentifier).Result;
        }

        public abstract Task<string> GetJsonFromSecretAsync(string secretVaultIdentifier);

        public virtual string GetValueFromSecret(string key, string secretVaultIdentifier)
        {
            return GetValueFromSecretAsync(key, secretVaultIdentifier).Result;
        }

        /// <summary>
        /// Returns the text value of of a particular key, from a given secret ARN
        /// </summary>
        /// <param name="key"></param>
        /// <param name="secretVaultIdentifier"></param>
        /// <returns></returns>
        public async Task<string> GetValueFromSecretAsync(string key, string secretVaultIdentifier)
        {
            string secretStringJson = await GetJsonFromSecretAsync(secretVaultIdentifier);
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
        public async Task<IDictionary<string, string>> GetAllFieldsFromSecret(string secretVaultIdentifier)
        {
            string secretStringJson = await GetJsonFromSecretAsync(secretVaultIdentifier);
            IDictionary<string, string> kvps = null;

            // Decrypt the secret
            if (!string.IsNullOrEmpty(secretStringJson))
            {
                kvps = new Dictionary<string, string>();
                dynamic secretVault = JValue.Parse(secretStringJson);
                // loop through the desired set of keys to find the corresponding values in the JSON
                foreach (Newtonsoft.Json.Linq.JProperty jproperty in secretVault)
                {
                    kvps.Add(jproperty.Name, jproperty.Value.ToString());
                }
            }
            return kvps;
        }

        public virtual ISecretVault GetSecretVault(string secretVaultIdentifier, string name, string fullName)
        {
            return GetSecretVaultAsync(secretVaultIdentifier,name, fullName).Result;
        }

        public abstract Task<ISecretVault> GetSecretVaultAsync(string secretVaultIdentifier, string name, string fullName);

        /// <summary>
        /// Returns the set of key value pairs for a given set of keys, which are part of a given secret ARN
        /// </summary>
        /// <param name="keys">The list of keys you are looking for</param>
        /// <param name="secretVaultIdentifier">The ARN of the secret in which these keys are fields</param>
        /// <returns></returns>
        public virtual async Task<IDictionary<string, string>> GetValuesFromSecret(IList<string> keys, string secretVaultIdentifier)
        {
            string secretStringJson = await GetJsonFromSecretAsync(secretVaultIdentifier);
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


        public virtual string GetDbConnectionStringFromSecret(string secretVaultIdentifier, string connectionStringFieldName)
        {
            return GetDbConnectionStringFromSecretAsync(secretVaultIdentifier, connectionStringFieldName).Result;
        }

        /// <summary>
        /// Returns a valid database connection string which is stored in "dbConnectionString" key in a Secrets Manager secret.
        /// </summary>
        /// <param name="secretVaultIdentifier">The AWS ARN of the secret in which the key is located.</param>
        /// <returns>A SQL connection string</returns>
        public async virtual Task<string> GetDbConnectionStringFromSecretAsync(string secretVaultIdentifier, string connectionStringFieldName)
        {
            _logger.Info(string.Format("Getting DB Connection string from Secrets Manager for secret ARN {0}", secretVaultIdentifier));
            string connectionStringJson = await GetJsonFromSecretAsync(secretVaultIdentifier);
            string connectionString = String.Empty;

            // Decrypts secret using the associated JSON. The secret should contain a field with the connection string Fieldname key with a valid 
            // database connection string as its field value.
            if (!string.IsNullOrEmpty(connectionStringJson))
            {
                try
                {
                    dynamic parsedObject = JsonConvert.DeserializeObject(connectionStringJson);
                    foreach (dynamic entry in parsedObject)
                    {
                        if (entry.Name.Equals(connectionStringFieldName))
                        {
                            connectionString = entry.Value;
                            break;
                        }

                    }
                    
                }
                catch (JsonReaderException jEx)
                {
                    _logger.Error(string.Format("An exception was thrown while attempting to GetDbConnectionStringFromSecret for ARN: {0}. The message was {1}.", secretVaultIdentifier, jEx.Message));
                }
            }
            Console.WriteLine("AWS connection string: " + connectionString);
            _logger.Info(string.Format("Got DB Connection string from Secrets Manager for secret ARN {0}", secretVaultIdentifier));
            return connectionString;
        }

        class Root
        {
            [JsonProperty("data")]
            public List<JObject> Data { get; set; }
        }

        /// <summary>
        /// Writes the text value of a particular key, to a given secret ARN
        /// </summary>
        /// <param name="key">The field within the secret to update</param>
        /// <param name="value">The value to update for the given key</param>
        /// <param name="secretVaultIdentifier">The full secret ARN</param>
        /// <returns>A status code with the result of the request</returns>
        public abstract HttpStatusCode WriteValuesToSecret(IDictionary<string, string> fieldsToInsertOrUpdate, string secretVaultIdentifier);

        /// <summary>
        /// Writes the text value of a particular key, to a given secret ARN
        /// </summary>
        /// <param name="key">The field within the secret to update</param>
        /// <param name="value">The value to update for the given key</param>
        /// <param name="secretVaultIdentifier">The full secret ARN</param>
        /// <returns>A status code with the result of the request</returns>
        public abstract Task<HttpStatusCode> WriteValuesToSecretAsync(IDictionary<string, string> fieldsToInsertOrUpdate, string secretVaultIdentifier);

        public abstract string UpdateJsonForSecret(string secretVaultIdentifier, string originalSecretJson, string key, string value);

    }
}
