﻿using Castle.Core.Logging;
using DeploySoftware.LaunchPad.Core.Util;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DeploySoftware.LaunchPad.Core.Configuration
{
    public abstract partial class SecretHelper : HelperBase
    {
        public SecretHelper() : base()
        {
            Logger = NullLogger.Instance;
        }

        public SecretHelper(ILogger logger) : base(logger)
        {
        }


        public abstract Task<string> GetJsonFromSecret(string secretVaultIdentifier);

        /// <summary>
        /// Returns the text value of of a particular key, from a given secret ARN
        /// </summary>
        /// <param name="key"></param>
        /// <param name="secretVaultIdentifier"></param>
        /// <returns></returns>
        public async Task<string> GetValueFromSecret(string key, string secretVaultIdentifier)
        {
            string secretStringJson = await GetJsonFromSecret(secretVaultIdentifier);
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
            string secretStringJson = await GetJsonFromSecret(secretVaultIdentifier);
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

        /// <summary>
        /// Returns the set of key value pairs for a given set of keys, which are part of a given secret ARN
        /// </summary>
        /// <param name="keys">The list of keys you are looking for</param>
        /// <param name="secretVaultIdentifier">The ARN of the secret in which these keys are fields</param>
        /// <returns></returns>
        public async Task<IDictionary<string,string>> GetValuesFromSecret(IList<string> keys, string secretVaultIdentifier)
        {
            string secretStringJson = await GetJsonFromSecret(secretVaultIdentifier);
            IDictionary<string, string> kvps = null;

            // Decrypt the secret
            if (!string.IsNullOrEmpty(secretStringJson))
            {
                dynamic secretObj = JObject.Parse(secretStringJson);
                kvps = new Dictionary<string, string>();
                // loop through the desired set of keys to find the corresponding values in the JSON
                foreach(string key in keys)
                {
                    string value = secretObj[key];
                    if(!string.IsNullOrEmpty(value))
                    {
                        kvps.Add(key, value);
                    }
                }
            }
            return kvps;
        }


        /// <summary>
        /// Returns a valid database connection string which is stored in "dbConnectionString" key in a Secrets Manager secret.
        /// </summary>
        /// <param name="secretVaultIdentifier">The AWS ARN of the secret in which the key is located.</param>
        /// <returns>A SQL connection string</returns>
        public async virtual Task<string> GetDbConnectionStringFromSecret(string secretVaultIdentifier)
        {
            Logger.Info(string.Format("Getting DB Connection string from Secrets Manager for secret ARN {0}", secretVaultIdentifier));
            string connectionStringJson = await GetJsonFromSecret(secretVaultIdentifier);            
            string connectionString = String.Empty;
            
            // Decrypts secret using the associated JSON. The secret should contain a "dbConnectionString" key with a valid 
            // database connection string
            if (!string.IsNullOrEmpty(connectionStringJson))
            {
                try
                {
                    dynamic secretObj = JObject.Parse(connectionStringJson);
                    connectionString = secretObj.dbConnectionString;
                }
                catch(JsonReaderException jEx)
                {
                    Logger.Error(string.Format("An exception was thrown while attempting to GetDbConnectionStringFromSecret for ARN: {0}. The message was {1}.", secretVaultIdentifier, jEx.Message));
                }
            }
            Console.WriteLine("AWS connection string: " + connectionString);
            Logger.Info(string.Format("Got DB Connection string from Secrets Manager for secret ARN {0}", secretVaultIdentifier));
            return connectionString;
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