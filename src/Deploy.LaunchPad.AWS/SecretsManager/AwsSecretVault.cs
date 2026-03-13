// ***********************************************************************
// Assembly         : Deploy.LaunchPad.AWS
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 01-08-2023
// ***********************************************************************
// <copyright file="AwsSecretVault.cs" company="Deploy Software Solutions, inc.">
//     2021-2023 Deploy Software Solutions, inc.
// </copyright>
// <summary></summary>
// ***********************************************************************

using Amazon;
using Amazon.Runtime;
using Amazon.SecretsManager;
using Amazon.SecretsManager.Model;
using Castle.Core.Logging;
using Deploy.LaunchPad.AWS.SecretsManager;
using Deploy.LaunchPad.Core.Configuration;
using Deploy.LaunchPad.Core.Secrets;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Deploy.LaunchPad.AWS
{
    /// <summary>
    /// Class AwsSecretVault.
    /// Implements the <see cref="SecretVaultBase" />
    /// Implements the <see cref="ISecretVault" />
    /// </summary>
    /// <seealso cref="SecretVaultBase" />
    /// <seealso cref="ISecretVault" />
    public partial class AwsSecretVault : SecretVaultBase, ISecretVault
    {

        /// <summary>
        /// The secret client
        /// </summary>
        protected IAmazonSecretsManager _secretClient;

        /// <summary>
        /// Gets the secret client.
        /// </summary>
        /// <value>The secret client.</value>
        [JsonIgnore]
        public IAmazonSecretsManager SecretClient { get { return _secretClient; } }

        public override SecretSource Source => SecretSource.AwsSecretsManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="AwsSecretVault"/> class.
        /// </summary>
        /// <param name="arn">The arn.</param>
        [SetsRequiredMembers]
        public AwsSecretVault(ILogger logger, string arn, string regionName = "us-east-1", string awsProfileName = "default", bool shouldUseLocalAwsProfile = false) : base(logger, arn)
        {
            RegionEndpoint region = RegionEndpoint.GetBySystemName(regionName);
            _secretClient = GetSecretClient(region, awsProfileName, shouldUseLocalAwsProfile);
        }

        public override async Task<string?> GetValueOrNullForSettingSecretProviderDescriptorAsync(
            SettingSecretProviderDescriptor source,
            ISettingDefinition definition,
            CancellationToken cancellationToken = default)
        {
            return await Task.FromResult(GetValueOrNullForSettingSecretProviderDescriptor(source, definition));
        }

        public override string? GetValueOrNullForSettingSecretProviderDescriptor(
            SettingSecretProviderDescriptor source,
            ISettingDefinition definition)
        {
            // lookup from AWS
            return null;
        }

        /// <summary>
        /// Returns the set of key value pairs for a given set of keys, which are part of a given secret vault's fields
        /// </summary>
        /// <param name="secretVault">The secret vault in which these keys are fields</param>
        /// <param name="keys">The list of keys you are looking for</param>
        /// <param name="caller">The caller.</param>
        /// <returns>A Task&lt;IDictionary`2&gt; representing the asynchronous operation.</returns>
        public override async Task<IDictionary<string, ISettingDefinition>> GetValuesForKeysAsync(IList<string> keys, string caller, bool keyIsCaseInsensitive = true)
        {
            string secretStringJson = await GetJsonFromSecretVaultAsync(caller, keyIsCaseInsensitive);
            IDictionary<string, ISettingDefinition> kvps = null;

            // Decrypt the secret
            if (!string.IsNullOrEmpty(secretStringJson))
            {
                dynamic secretObj = JObject.Parse(secretStringJson);
                kvps = new Dictionary<string, ISettingDefinition>();
                // loop through the desired set of keys to find the corresponding values in the JSON
                foreach (string key in keys)
                {
                    ISettingDefinition value = null;
                    if (keyIsCaseInsensitive)
                    {
                        value = secretObj[key.ToLower()];
                    }
                    else
                    {
                        value = secretObj[key];
                    }
                    if (value != null)
                    {
                        kvps.Add(key, value);
                    }
                }
            }
            return kvps;
        }

        /// <summary>
        /// Returns the set of all key value pairs, which are part of a given secret ARN
        /// The field names do not have to be known ahead of time.
        /// </summary>
        /// <param name="secretVault">The secret vault.</param>
        /// <param name="caller">The caller.</param>
        /// <returns>A Task&lt;IDictionary`2&gt; representing the asynchronous operation.</returns>
        public override async Task<IDictionary<string, ISettingDefinition>> GetAllValuesAsync(string caller)
        {
            string secretStringJson = await GetJsonFromSecretVaultAsync(caller);
            IDictionary<string, ISettingDefinition> kvps = null;

            // Decrypt the secret
            if (!string.IsNullOrEmpty(secretStringJson))
            {
                kvps = new Dictionary<string, ISettingDefinition>();
                dynamic secretJson = JValue.Parse(secretStringJson);
                // loop through the desired set of keys to find the corresponding values in the JSON
                foreach (Newtonsoft.Json.Linq.JProperty jproperty in secretJson)
                {
                    ISettingDefinition settingDefinition = new SettingDefinition
                    (jproperty.Name, jproperty.Value.ToString(), null, null, null, SettingScopes.Application, false, false, null);
                    kvps.Add(jproperty.Name, settingDefinition);
                }
            }
            return kvps;
        }


        // update methods
        /// <summary>
        /// Creates the or update field in secret vault.
        /// </summary>
        /// <param name="secretVault">The secret vault.</param>
        /// <param name="originalSecretJson">The original secret json.</param>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <param name="caller">The caller.</param>
        /// <returns>ISettingDefinition.</returns>
        public override ISettingDefinition CreateOrUpdateField(string originalSecretJson, string key, ISettingDefinition value, string caller)
        {
            Logger.Info(string.Format(Deploy_LaunchPad_AWS_Resources.Logger_Info_UpdateJsonForSecret_Updating, value, key, VaultId));
            Logger.Debug(string.Format("Caller = '{0}'.", caller));
            string updatedJsonString = originalSecretJson;

            JObject jObject = Newtonsoft.Json.JsonConvert.DeserializeObject(originalSecretJson) as JObject;

            // Try to select the nested property (if it exists) using the key
            // but also check a parameterized version in format ['key.abc'] in case it contains periods or special characters.
            string parameterizedJsonKey = "['" + key + "']";
            var jTokenValues = jObject.Root.Values<JToken>();
            JToken jToken = jTokenValues.FirstOrDefault(x => x.Path == key || x.Path == parameterizedJsonKey);
            if (jToken != null)
            {
                // The property exists - update its value
                //jToken.Replace(value);
                jObject[jToken.Path] = value.DefaultValue;
            }
            else // The property does not exist, insert a new one
            {
                JProperty newProperty = new JProperty(key, value);
                jObject.Add(newProperty);
            }
            // Convert the JObject back to a string to get the resulting Json from the updated secret
            updatedJsonString = jObject.ToString();
            ISettingDefinition updatedSetting = new SettingDefinition(key, updatedJsonString);
            Logger.Info(string.Format(Deploy_LaunchPad_AWS_Resources.Logger_Info_UpdateJsonForSecret_Updated, value, key, VaultId));
            return updatedSetting;
        }

        /// <summary>
        /// Writes the text value of a particular key, to a given secret ARN
        /// </summary>
        /// <param name="secretVault">The secret vault.</param>
        /// <param name="fieldsToInsertOrUpdate">The fields to insert or update.</param>
        /// <param name="caller">The caller.</param>
        /// <returns>A status code with the result of the request</returns>
        public override HttpStatusCode BatchUpdateFields(IDictionary<string, ISettingDefinition> fieldsToInsertOrUpdate, string caller)
        {
            return BatchUpdateFieldsAsync(fieldsToInsertOrUpdate, caller).Result;
        }

        /// <summary>
        /// Writes the text value of a particular key, to a given secret ARN
        /// </summary>
        /// <param name="secretVault">The secret vault in which the field is stored</param>
        /// <param name="fieldsToInsertOrUpdate">The fields to insert or update.</param>
        /// <param name="caller">The caller.</param>
        /// <returns>A status code with the result of the request</returns>
        public async override Task<HttpStatusCode> BatchUpdateFieldsAsync(IDictionary<string, ISettingDefinition> fieldsToInsertOrUpdate, string caller)
        {
            string originalSecretJson = await GetJsonFromSecretVaultAsync(caller);

            // for each value in the dictionary, try to update the JSON
            string sbUpdatedSecretJson = originalSecretJson;
            foreach (var field in fieldsToInsertOrUpdate)
            {
                sbUpdatedSecretJson = CreateOrUpdateField(sbUpdatedSecretJson, field.Key, field.Value, caller).DefaultValue;
            }

            PutSecretValueResponse response = null;

            // Now update the secret
            if (!string.IsNullOrEmpty(sbUpdatedSecretJson))
            {
                PutSecretValueRequest request = new PutSecretValueRequest();
                request.SecretId = VaultId;
                request.SecretString = sbUpdatedSecretJson;
                try
                {
                    response = await SecretClient.PutSecretValueAsync(request);
                }
                catch (EncryptionFailureException e)
                {
                    // Secrets Manager can't encrypt the protected secret text using the provided KMS key.\
                    Logger.Error(string.Format(Deploy_LaunchPad_AWS_Resources.Logger_Error_WriteValueToSecret_Exception, VaultId, e.Message));
                    throw;
                }
                catch (InternalServiceErrorException e)
                {
                    // An error occurred on the server side.
                    Logger.Error(string.Format(Deploy_LaunchPad_AWS_Resources.Logger_Error_WriteValueToSecret_Exception, VaultId, e.Message));
                    throw;
                }
                catch (InvalidParameterException e)
                {
                    // You provided an invalid value for a parameter.
                    Logger.Error(string.Format(Deploy_LaunchPad_AWS_Resources.Logger_Error_WriteValueToSecret_Exception, VaultId, e.Message));
                    throw;
                }
                catch (InvalidRequestException e)
                {
                    // You provided a parameter value that is not valid for the current state of the resource.
                    Logger.Error(string.Format(Deploy_LaunchPad_AWS_Resources.Logger_Error_WriteValueToSecret_Exception, VaultId, e.Message));
                    throw;
                }
                catch (ResourceNotFoundException e)
                {
                    // We can't find the resource that you asked for.
                    Logger.Error(string.Format(Deploy_LaunchPad_AWS_Resources.Logger_Error_WriteValueToSecret_Exception, VaultId, e.Message));
                    throw;
                }
                catch (ResourceExistsException e)
                {
                    // A resource with the ID you requested already exists.
                    Logger.Error(string.Format(Deploy_LaunchPad_AWS_Resources.Logger_Error_WriteValueToSecret_Exception, VaultId, e.Message));
                    throw;
                }
                catch (AggregateException e)
                {
                    // More than one of the above exceptions were triggered.
                    Logger.Error(string.Format(Deploy_LaunchPad_AWS_Resources.Logger_Error_WriteValueToSecret_Exception, VaultId, e.Message));
                    throw;
                }

            }
            return response.HttpStatusCode;
        }


        /// <summary>
        /// Get json from secret vault as an asynchronous operation.
        /// </summary>
        /// <param name="secretVault">The secret vault.</param>
        /// <param name="caller">The caller.</param>
        /// <returns>A Task&lt;System.String&gt; representing the asynchronous operation.</returns>
        public async virtual Task<string> GetJsonFromSecretVaultAsync(string caller, bool keyIsCaseInsensitive = true)
        {
            Logger.Info(string.Format(Deploy_LaunchPad_AWS_Resources.Logger_Info_GetJsonFromSecret_Getting,
                VaultId,
                caller
            ));
            GetSecretValueRequest request = new GetSecretValueRequest();
            request.SecretId = VaultId;
            request.VersionStage = "AWSCURRENT"; // VersionStage defaults to AWSCURRENT if unspecified.
            GetSecretValueResponse response = null;
            try
            {
                Logger.Info(string.Format("Getting DB Connection string from Secrets Manager for secret ARN {0}", VaultId));
                response = await SecretClient.GetSecretValueAsync(request);
            }
            catch (DecryptionFailureException e)
            {
                // Secrets Manager can't decrypt the protected secret text using the provided KMS key.\
                Logger.Error(string.Format(
                    Deploy_LaunchPad_AWS_Resources.Logger_Error_GetJsonFromSecret_ExceptionThrown, VaultId, e.Message));
                throw;
            }
            catch (InternalServiceErrorException e)
            {
                // An error occurred on the server side.
                Logger.Error(string.Format(Deploy_LaunchPad_AWS_Resources.Logger_Error_GetJsonFromSecret_ExceptionThrown, VaultId, e.Message));
                throw;
            }
            catch (InvalidParameterException e)
            {
                // You provided an invalid value for a parameter.
                Logger.Error(string.Format(Deploy_LaunchPad_AWS_Resources.Logger_Error_GetJsonFromSecret_ExceptionThrown, VaultId, e.Message));
                throw;
            }
            catch (InvalidRequestException e)
            {
                // You provided a parameter value that is not valid for the current state of the resource.
                Logger.Error(string.Format(Deploy_LaunchPad_AWS_Resources.Logger_Error_GetJsonFromSecret_ExceptionThrown, VaultId, e.Message));
                throw;
            }
            catch (ResourceNotFoundException e)
            {
                // We can't find the resource that you asked for.
                Logger.Error(string.Format(Deploy_LaunchPad_AWS_Resources.Logger_Error_GetJsonFromSecret_ExceptionThrown, VaultId, e.Message));
                throw;
            }
            catch (AggregateException e)
            {
                // More than one of the above exceptions were triggered.
                Logger.Error(string.Format(Deploy_LaunchPad_AWS_Resources.Logger_Error_GetJsonFromSecret_ExceptionThrown, VaultId, e.Message));
                throw;
            }
            Logger.Info(string.Format(
                Deploy_LaunchPad_AWS_Resources.Logger_Info_GetJsonFromSecret_Got,
                VaultId,
                caller
            ));
            return response.SecretString;
        }

        /// <summary>
        /// Get AWS Immutable Credentials where the IAM access key and secret values are stored in an AWS Secret Manager secret.
        /// </summary>
        /// <param name="secretVault">The secret vault.</param>
        /// <returns>IAM credentials if value, or null</returns>
        public virtual ImmutableCredentials GetCredentialsFromSecret()
        {
            return GetCredentialsFromSecretVaultAsync().Result;
        }


        /// <summary>
        /// Get AWS Immutable Credentials where the IAM access key and secret values are stored in an AWS Secret Manager secret.
        /// </summary>
        /// <param name="secretVault">The secret vault.</param>
        /// <returns>IAM credentials if value, or null</returns>
        public async virtual Task<ImmutableCredentials> GetCredentialsFromSecretVaultAsync()
        {
            Logger.Info(string.Format(Deploy_LaunchPad_AWS_Resources.Logger_Info_GetCredentialsFromSecret_Getting, VaultId));
            // create the aws credentials given the provided credentials taken from the secret
            dynamic secret = JsonConvert.DeserializeObject(await GetJsonFromSecretVaultAsync("AwsSecretsManagerHelper.GetCredentialsFromSecretAsync(string secretVaultIdentifier)"));
            string iamAccessKey = secret.apiGatewayIAMAccessKey;
            string iamSecretKey = secret.apiGatewayIAMSecret;
            Logger.Info(string.Format(Deploy_LaunchPad_AWS_Resources.Logger_Info_GetCredentialsFromSecret_Got, VaultId));
            return new ImmutableCredentials(iamAccessKey, iamSecretKey, null);
        }


        /// <summary>
        /// Gets the secret client.
        /// </summary>
        /// <param name="region">The region.</param>
        /// <param name="awsProfileName">Name of the aws profile.</param>
        /// <param name="shouldUseLocalAwsProfile">if set to <c>true</c> [should use local aws profile].</param>
        /// <param name="secretManagerConfig">The secret manager configuration.</param>
        /// <returns>AmazonSecretsManagerClient.</returns>
        protected virtual AmazonSecretsManagerClient GetSecretClient(
            RegionEndpoint region,
            string awsProfileName = "",
            bool shouldUseLocalAwsProfile = false,
            AmazonSecretsManagerConfig secretManagerConfig = null
        )
        {
            Logger.Info("AwsSecretProviderGetSecretClient() started.");
            Logger.Debug(string.Format(Deploy_LaunchPad_AWS_Resources.SecretHelper_GetSecretClient_ProfileName, awsProfileName));
            Logger.Debug(string.Format(Deploy_LaunchPad_AWS_Resources.SecretHelper_GetSecretClient_Region, region.SystemName));
            Logger.Debug(string.Format("shouldUseLocalAwsProfile?", shouldUseLocalAwsProfile));

            AwsSecretsManagerHelper awsSecretsManagerHelper = new AwsSecretsManagerHelper();
            AmazonSecretsManagerClient client = null;
            try
            {
                if (shouldUseLocalAwsProfile)
                {
                    Logger.Debug("AwsSecretProvider.GetSecretClient() => shouldUseLocalAwsProfile = true.");
                    if (string.IsNullOrEmpty(awsProfileName))
                    {
                        awsProfileName = AwsSecretsManagerHelper.DefaultLocalAwsProfileName;
                    }
                    var credentials = awsSecretsManagerHelper.GetAwsCredentialsFromNamedLocalProfile(awsProfileName);
                    if (credentials != null)
                    {
                        Logger.Debug(string.Format(
                            "AwsSecretProvider.GetSecretClient() => credentials was not null, creating client using local profile name '{0}'.",
                            awsProfileName
                        ));
                        if (secretManagerConfig == null) // use whatever the client config settings are
                        {
                            secretManagerConfig = new AmazonSecretsManagerConfig();
                            secretManagerConfig.RegionEndpoint = region;
                        }
                        client = new AmazonSecretsManagerClient(credentials, secretManagerConfig);

                        Logger.Debug("AwsSecretProvider.GetSecretClient() => credentials was not null, created client.");
                    }
                }
                else
                {
                    client = new AmazonSecretsManagerClient(region);
                }
            }
            catch (AmazonSecretsManagerException smEx)
            {
                Logger.Error(string.Format(Deploy_LaunchPad_AWS_Resources.SecretHelper_GetSecretClient_Exception_GetAwsCredentials, smEx.Message));
            }
            catch (AggregateException aEx)
            {
                Logger.Error(string.Format(Deploy_LaunchPad_AWS_Resources.SecretHelper_GetSecretClient_Exception_GetAwsCredentials, aEx.Message));
            }
            if (client == null) // try to load using local environment or EC2 information
            {
                if (secretManagerConfig == null)
                {
                    secretManagerConfig = new AmazonSecretsManagerConfig();
                    secretManagerConfig.RegionEndpoint = region;
                }
                client = new AmazonSecretsManagerClient(secretManagerConfig);
                Logger.Info(Deploy_LaunchPad_AWS_Resources.SecretHelper_GetSecretClient_SecretClient_IsNull);
            }
            Logger.Info("AwsSecretProvider.GetSecretClient() ended.");
            return client;

        }

        public override ISettingDefinition GetValue(string key, string caller, bool keyIsCaseInsensitive = true)
        {
            return GetValueAsync(key, caller, keyIsCaseInsensitive).Result;
        }

        public override async Task<ISettingDefinition> GetValueAsync(string key, string caller, bool keyIsCaseInsensitive = true)
        {
            // Get the secret value from AWS Secrets Manager
            var request = new Amazon.SecretsManager.Model.GetSecretValueRequest
            {
                SecretId = VaultId,
                VersionStage = "AWSCURRENT"
            };

            var response = await SecretClient.GetSecretValueAsync(request);
            if (string.IsNullOrEmpty(response.SecretString))
            {
                return null;
            }

            var secretObj = JObject.Parse(response.SecretString);

            string value = null;
            if (keyIsCaseInsensitive)
            {
                // Find the key in a case-insensitive manner
                var property = secretObj.Properties()
                    .FirstOrDefault(p => string.Equals(p.Name, key, StringComparison.OrdinalIgnoreCase));
                if (property != null)
                {
                    value = property.Value.ToString();
                }
            }
            else
            {
                value = secretObj[key]?.ToString();
            }

            if (value == null)
            {
                return null;
            }

            // Create a SettingDefinition (constructor may need adjustment based on your actual implementation)
            return new SettingDefinition(key, value);
        }
    }
}
