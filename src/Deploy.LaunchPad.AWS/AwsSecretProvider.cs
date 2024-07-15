// ***********************************************************************
// Assembly         : Deploy.LaunchPad.AWS
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 02-18-2023
// ***********************************************************************
// <copyright file="AwsSecretProvider.cs" company="Deploy Software Solutions, inc.">
//     2021-2023 Deploy Software Solutions, inc.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Amazon;
using Amazon.Runtime;
using Amazon.SecretsManager;
using Amazon.SecretsManager.Model;
using Deploy.LaunchPad.AWS.SecretsManager;
using Deploy.LaunchPad.Core.Config;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Deploy.LaunchPad.AWS
{
    /// <summary>
    /// Class AwsSecretProvider.
    /// Implements the <see cref="SecretProviderBase" />
    /// Implements the <see cref="ISecretProvider" />
    /// </summary>
    /// <seealso cref="SecretProviderBase" />
    /// <seealso cref="ISecretProvider" />
    public partial class AwsSecretProvider : SecretProviderBase, ISecretProvider
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

        /// <summary>
        /// Gets the type.
        /// </summary>
        /// <value>The type.</value>
        public override string Type { get; protected set; } = "Deploy.LaunchPad.AWS.AwsSecretProvider";

        /// <summary>
        /// Initializes a new instance of the <see cref="AwsSecretProvider"/> class.
        /// </summary>
        public AwsSecretProvider() : base()
        {
            RegionEndpoint region = RegionEndpoint.GetBySystemName("us-east-1");
            string awsProfileName = "";
            bool shouldUseLocalAwsProfile = false;
            _secretClient = GetSecretClient(region, awsProfileName, shouldUseLocalAwsProfile);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AwsSecretProvider"/> class.
        /// </summary>
        /// <param name="regionName">Name of the region.</param>
        /// <param name="awsProfileName">Name of the aws profile.</param>
        /// <param name="shouldUseLocalAwsProfile">if set to <c>true</c> [should use local aws profile].</param>
        public AwsSecretProvider(string regionName = "us-east-1", string awsProfileName = "default", bool shouldUseLocalAwsProfile = false) : base()
        {
            RegionEndpoint region = RegionEndpoint.GetBySystemName(regionName);
            _secretClient = GetSecretClient(region, awsProfileName, shouldUseLocalAwsProfile);
        }



        /// <summary>
        /// Get secret vault by identifier as an asynchronous operation.
        /// </summary>
        /// <param name="arn">The arn.</param>
        /// <param name="caller">The caller.</param>
        /// <returns>A Task&lt;ISecretVault&gt; representing the asynchronous operation.</returns>
        public async override Task<ISecretVault> GetSecretVaultByIdAsync(string arn, string caller)
        {
            AwsSecretVault vault = new AwsSecretVault();
            vault.VaultId = arn;
            vault.Fields = await GetAllValuesFromSecretVaultAsync(vault, caller);
            return vault;
        }

        /// <summary>
        /// Get secret vault by vault identifier as an asynchronous operation.
        /// </summary>
        /// <param name="arn">The arn.</param>
        /// <param name="caller">The caller.</param>
        /// <returns>A Task&lt;ISecretVault&gt; representing the asynchronous operation.</returns>
        public async override Task<ISecretVault> GetSecretVaultByVaultIdAsync(string arn, string caller)
        {
            AwsSecretVault vault = new AwsSecretVault();
            vault.VaultId = arn;
            vault.Fields = await GetAllValuesFromSecretVaultAsync(vault, caller);
            return vault;
        }


        /// <summary>
        /// Get json from secret vault as an asynchronous operation.
        /// </summary>
        /// <param name="secretVault">The secret vault.</param>
        /// <param name="caller">The caller.</param>
        /// <returns>A Task&lt;System.String&gt; representing the asynchronous operation.</returns>
        public async override Task<string> GetJsonFromSecretVaultAsync(ISecretVault secretVault, string caller)
        {
            Logger.Info(string.Format(Deploy_LaunchPad_AWS_Resources.Logger_Info_GetJsonFromSecret_Getting,
                secretVault.Id,
                caller
            ));
            GetSecretValueRequest request = new GetSecretValueRequest();
            request.SecretId = secretVault.VaultId;
            request.VersionStage = "AWSCURRENT"; // VersionStage defaults to AWSCURRENT if unspecified.
            GetSecretValueResponse response = null;
            try
            {
                Logger.Info(string.Format("Getting DB Connection string from Secrets Manager for secret ARN {0}", secretVault.Id));
                response = await SecretClient.GetSecretValueAsync(request);
            }
            catch (DecryptionFailureException e)
            {
                // Secrets Manager can't decrypt the protected secret text using the provided KMS key.\
                Logger.Error(string.Format(
                    Deploy_LaunchPad_AWS_Resources.Logger_Error_GetJsonFromSecret_ExceptionThrown, secretVault.Id, e.Message));
                throw;
            }
            catch (InternalServiceErrorException e)
            {
                // An error occurred on the server side.
                Logger.Error(string.Format(Deploy_LaunchPad_AWS_Resources.Logger_Error_GetJsonFromSecret_ExceptionThrown, secretVault.Id, e.Message));
                throw;
            }
            catch (InvalidParameterException e)
            {
                // You provided an invalid value for a parameter.
                Logger.Error(string.Format(Deploy_LaunchPad_AWS_Resources.Logger_Error_GetJsonFromSecret_ExceptionThrown, secretVault.Id, e.Message));
                throw;
            }
            catch (InvalidRequestException e)
            {
                // You provided a parameter value that is not valid for the current state of the resource.
                Logger.Error(string.Format(Deploy_LaunchPad_AWS_Resources.Logger_Error_GetJsonFromSecret_ExceptionThrown, secretVault.Id, e.Message));
                throw;
            }
            catch (ResourceNotFoundException e)
            {
                // We can't find the resource that you asked for.
                Logger.Error(string.Format(Deploy_LaunchPad_AWS_Resources.Logger_Error_GetJsonFromSecret_ExceptionThrown, secretVault.Id, e.Message));
                throw;
            }
            catch (AggregateException e)
            {
                // More than one of the above exceptions were triggered.
                Logger.Error(string.Format(Deploy_LaunchPad_AWS_Resources.Logger_Error_GetJsonFromSecret_ExceptionThrown, secretVault.Id, e.Message));
                throw;
            }
            Logger.Info(string.Format(
                Deploy_LaunchPad_AWS_Resources.Logger_Info_GetJsonFromSecret_Got,
                secretVault.Id,
                caller
            ));
            return response.SecretString;
        }

        /// <summary>
        /// Get AWS Immutable Credentials where the IAM access key and secret values are stored in an AWS Secret Manager secret.
        /// </summary>
        /// <param name="arn">The arn.</param>
        /// <returns>IAM credentials if value, or null</returns>
        public virtual ImmutableCredentials GetCredentialsFromSecret(string arn)
        {
            AwsSecretVault vault = (AwsSecretVault)GetSecretVaultByVaultId(arn, "AwsSecretProvier.GetCredentialsFromSecret(string secretVaultIdentifier)");
            return GetCredentialsFromSecret(vault);
        }

        /// <summary>
        /// Get AWS Immutable Credentials where the IAM access key and secret values are stored in an AWS Secret Manager secret.
        /// </summary>
        /// <param name="secretVault">The secret vault.</param>
        /// <returns>IAM credentials if value, or null</returns>
        public virtual ImmutableCredentials GetCredentialsFromSecret(ISecretVault secretVault)
        {
            return GetCredentialsFromSecretVaultAsync(secretVault).Result;
        }


        /// <summary>
        /// Get AWS Immutable Credentials where the IAM access key and secret values are stored in an AWS Secret Manager secret.
        /// </summary>
        /// <param name="secretVault">The secret vault.</param>
        /// <returns>IAM credentials if value, or null</returns>
        public async virtual Task<ImmutableCredentials> GetCredentialsFromSecretVaultAsync(ISecretVault secretVault)
        {
            Logger.Info(string.Format(Deploy_LaunchPad_AWS_Resources.Logger_Info_GetCredentialsFromSecret_Getting, secretVault.Id));
            // create the aws credentials given the provided credentials taken from the secret
            dynamic secret = JsonConvert.DeserializeObject(await GetJsonFromSecretVaultAsync(secretVault, "AwsSecretsManagerHelper.GetCredentialsFromSecretAsync(string secretVaultIdentifier)"));
            string iamAccessKey = secret.apiGatewayIAMAccessKey;
            string iamSecretKey = secret.apiGatewayIAMSecret;
            Logger.Info(string.Format(Deploy_LaunchPad_AWS_Resources.Logger_Info_GetCredentialsFromSecret_Got, secretVault.Id));
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
        // Refresh methods
        /// <summary>
        /// Refreshes the secret vault.
        /// </summary>
        /// <param name="arn">The arn.</param>
        /// <param name="caller">The caller.</param>
        /// <returns>ISecretVault.</returns>
        public override ISecretVault RefreshSecretVault(string arn, string caller)
        {
            return GetSecretVaultByIdAsync(arn, caller).Result;
        }

        /// <summary>
        /// Refresh secret vault as an asynchronous operation.
        /// </summary>
        /// <param name="arn">The arn.</param>
        /// <param name="caller">The caller.</param>
        /// <returns>A Task&lt;ISecretVault&gt; representing the asynchronous operation.</returns>
        public override async Task<ISecretVault> RefreshSecretVaultAsync(string arn, string caller)
        {
            var vault = await GetSecretVaultByIdAsync(arn, "AwsSecretProvier.RefreshSecretVault(string vaultId, string caller)");
            return vault;
        }


        /// <summary>
        /// Refreshes the secret vault.
        /// </summary>
        /// <param name="secretVault">The secret vault.</param>
        /// <param name="caller">The caller.</param>
        /// <returns>ISecretVault.</returns>
        public override ISecretVault RefreshSecretVault(ISecretVault secretVault, string caller)
        {
            return RefreshSecretVaultAsync(secretVault, caller).Result;
        }

        /// <summary>
        /// Refresh secret vault as an asynchronous operation.
        /// </summary>
        /// <param name="secretVault">The secret vault.</param>
        /// <param name="caller">The caller.</param>
        /// <returns>A Task&lt;ISecretVault&gt; representing the asynchronous operation.</returns>
        public override async Task<ISecretVault> RefreshSecretVaultAsync(ISecretVault secretVault, string caller)
        {
            secretVault.Fields = await GetAllValuesFromSecretVaultAsync(secretVault, caller);
            return secretVault;
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
        /// <returns>System.String.</returns>
        public override string CreateOrUpdateFieldInSecretVault(ISecretVault secretVault, string originalSecretJson, string key, string value, string caller)
        {
            Logger.Info(string.Format(Deploy_LaunchPad_AWS_Resources.Logger_Info_UpdateJsonForSecret_Updating, value, key, secretVault.Id));
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
                jToken.Replace(value);
            }
            else // The property does not exist, insert a new one
            {
                JProperty newProperty = new JProperty(key, value);
                jObject.Add(newProperty);
            }
            // Convert the JObject back to a string to get the resulting Json from the updated secret
            updatedJsonString = jObject.ToString();

            Logger.Info(string.Format(Deploy_LaunchPad_AWS_Resources.Logger_Info_UpdateJsonForSecret_Updated, value, key, secretVault.Id));
            return updatedJsonString;
        }

        /// <summary>
        /// Writes the text value of a particular key, to a given secret ARN
        /// </summary>
        /// <param name="secretVault">The secret vault.</param>
        /// <param name="fieldsToInsertOrUpdate">The fields to insert or update.</param>
        /// <param name="caller">The caller.</param>
        /// <returns>A status code with the result of the request</returns>
        public override HttpStatusCode UpdateFieldsInSecretVault(ISecretVault secretVault, IDictionary<string, string> fieldsToInsertOrUpdate, string caller)
        {
            return UpdateFieldsInSecretVaultAsync(secretVault, fieldsToInsertOrUpdate, caller).Result;
        }

        /// <summary>
        /// Writes the text value of a particular key, to a given secret ARN
        /// </summary>
        /// <param name="secretVault">The secret vault in which the field is stored</param>
        /// <param name="fieldsToInsertOrUpdate">The fields to insert or update.</param>
        /// <param name="caller">The caller.</param>
        /// <returns>A status code with the result of the request</returns>
        public async override Task<HttpStatusCode> UpdateFieldsInSecretVaultAsync(ISecretVault secretVault, IDictionary<string, string> fieldsToInsertOrUpdate, string caller)
        {
            string originalSecretJson = await GetJsonFromSecretVaultAsync(secretVault, caller);

            // for each value in the dictionary, try to update the JSON
            string sbUpdatedSecretJson = originalSecretJson;
            foreach (var field in fieldsToInsertOrUpdate)
            {
                sbUpdatedSecretJson = CreateOrUpdateFieldInSecretVault(secretVault, sbUpdatedSecretJson, field.Key, field.Value, caller);
            }

            PutSecretValueResponse response = null;

            // Now update the secret
            if (!string.IsNullOrEmpty(sbUpdatedSecretJson))
            {
                PutSecretValueRequest request = new PutSecretValueRequest();
                request.SecretId = secretVault.VaultId;
                request.SecretString = sbUpdatedSecretJson;
                try
                {
                    response = await SecretClient.PutSecretValueAsync(request);
                }
                catch (EncryptionFailureException e)
                {
                    // Secrets Manager can't encrypt the protected secret text using the provided KMS key.\
                    Logger.Error(string.Format(Deploy_LaunchPad_AWS_Resources.Logger_Error_WriteValueToSecret_Exception, secretVault.Id, e.Message));
                    throw;
                }
                catch (InternalServiceErrorException e)
                {
                    // An error occurred on the server side.
                    Logger.Error(string.Format(Deploy_LaunchPad_AWS_Resources.Logger_Error_WriteValueToSecret_Exception, secretVault.Id, e.Message));
                    throw;
                }
                catch (InvalidParameterException e)
                {
                    // You provided an invalid value for a parameter.
                    Logger.Error(string.Format(Deploy_LaunchPad_AWS_Resources.Logger_Error_WriteValueToSecret_Exception, secretVault.Id, e.Message));
                    throw;
                }
                catch (InvalidRequestException e)
                {
                    // You provided a parameter value that is not valid for the current state of the resource.
                    Logger.Error(string.Format(Deploy_LaunchPad_AWS_Resources.Logger_Error_WriteValueToSecret_Exception, secretVault.Id, e.Message));
                    throw;
                }
                catch (ResourceNotFoundException e)
                {
                    // We can't find the resource that you asked for.
                    Logger.Error(string.Format(Deploy_LaunchPad_AWS_Resources.Logger_Error_WriteValueToSecret_Exception, secretVault.Id, e.Message));
                    throw;
                }
                catch (ResourceExistsException e)
                {
                    // A resource with the ID you requested already exists.
                    Logger.Error(string.Format(Deploy_LaunchPad_AWS_Resources.Logger_Error_WriteValueToSecret_Exception, secretVault.Id, e.Message));
                    throw;
                }
                catch (AggregateException e)
                {
                    // More than one of the above exceptions were triggered.
                    Logger.Error(string.Format(Deploy_LaunchPad_AWS_Resources.Logger_Error_WriteValueToSecret_Exception, secretVault.Id, e.Message));
                    throw;
                }

            }
            return response.HttpStatusCode;
        }




    }
}
