using Abp.Dependency;
using Abp.UI;
using Amazon;
using Amazon.Runtime;
using Amazon.Runtime.CredentialManagement;
using Amazon.SecretsManager;
using Amazon.SecretsManager.Model;
using Castle.Core.Logging;
using DeploySoftware.LaunchPad.Core.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DeploySoftware.LaunchPad.AWS
{
    public partial class AwsSecretHelper : SecretHelper, ISingletonDependency, ISecretHelper
    {
        protected const string DefaultRegionName = "us-east-1";

        public RegionEndpoint Region { get; set; }

        public IAmazonSecretsManager SecretClient { get; set; }


        public AwsSecretHelper() : base()
        {
            Logger = NullLogger.Instance;
            Region = GetRegionEndpoint(DefaultRegionName);
            SecretClient = SetSecretClient(Region);
        }

        public AwsSecretHelper(ILogger logger) : base(logger)
        {
            Region = GetRegionEndpoint(DefaultRegionName);
            SecretClient = SetSecretClient(Region);
        }

        public AwsSecretHelper(IAmazonSecretsManager client, ILogger logger) : base(logger)
        {
            Region = GetRegionEndpoint(DefaultRegionName);
            SecretClient = client;
        }

        public AwsSecretHelper(string awsRegionEndpointName, ILogger logger) : base(logger)
        {
            Region = GetRegionEndpoint(DefaultRegionName);
            SecretClient = SetSecretClient(Region);
        }

        public AwsSecretHelper(string awsProfileName, string awsRegionEndpointName, ILogger logger) : base(logger)
        {
            Region = GetRegionEndpoint(DefaultRegionName);
            SecretClient = SetSecretClient(Region,awsProfileName);
        }

        /// <summary>
        /// Returns an AWS region endpoint from a given endpoint name, or the default region "us-east-1" if invalid/none provided.
        /// </summary>
        /// <param name="awsRegionEndpointSystemName">A valid AWS region endpoint system name.</param>
        /// <returns>A valid AWS Region Endpoint</returns>
        public RegionEndpoint GetRegionEndpoint(string awsRegionEndpointSystemName)
        {
            RegionEndpoint region = null;

            // attempt to load the Region Endpoint from the list of available ones
            if (!string.IsNullOrEmpty(awsRegionEndpointSystemName))
            {
                foreach (var e in RegionEndpoint.EnumerableAllRegions)
                {
                    if (e.Equals(awsRegionEndpointSystemName))
                    {
                        region = e;
                    }
                }
            }

            // if the region is still null, or the string was null or empty previously, use the default region endpoint
            if (region == null)
            {
                region = RegionEndpoint.GetBySystemName(DefaultRegionName);
            }

            Logger.Info(string.Format(DeploySoftware_LaunchPad_AWS_Resources.SecretHelper_GetRegionEndpoint_Logger_Info_RegionName, region.DisplayName, region.SystemName));
            return region;
        }

        /// <summary>
        /// Creates a new Secrets Manager client
        /// </summary>
        /// <returns></returns>
        public AmazonSecretsManagerClient SetSecretClient(RegionEndpoint region)
        {
            return new AmazonSecretsManagerClient(region);
        }

        /// <summary>
        /// Creates a new Secrets Manager client using the provided profile information
        /// </summary>
        /// <param name="profileName"></param>
        /// <param name="awsRegionSystemName"></param>
        /// <returns></returns>
        public AmazonSecretsManagerClient SetSecretClient(RegionEndpoint region, string profileName)
        {
            if(string.IsNullOrEmpty(profileName))
            {
                profileName = "default";
            }
            
            Logger.Info(string.Format(DeploySoftware_LaunchPad_AWS_Resources.SecretHelper_GetSecretClient_ProfileName, profileName));
            Logger.Info(string.Format(DeploySoftware_LaunchPad_AWS_Resources.SecretHelper_GetSecretClient_Region, profileName));

            AmazonSecretsManagerClient client = null;
            try
            {
                var credentials = GetAwsCredentials(profileName);
                client = new AmazonSecretsManagerClient(credentials, region);
            }
            catch(AmazonSecretsManagerException smEx)
            {
                Logger.Info(string.Format(DeploySoftware_LaunchPad_AWS_Resources.SecretHelper_GetSecretClient_Exception_GetAwsCredentials,smEx.Message));
            }
            if (client == null) // try to load using local environment or EC2 information
            {
                Logger.Info(DeploySoftware_LaunchPad_AWS_Resources.SecretHelper_GetSecretClient_SecretClient_IsNull);
                client = SetSecretClient(region);
            }            
            return client;

        }

        public AWSCredentials GetAwsCredentials(string awsProfileName)
        {
            var chain = new CredentialProfileStoreChain();
            AWSCredentials creds;
            if (chain.TryGetAWSCredentials(awsProfileName, out creds))
            {
                Console.WriteLine("AWS credentials created");
            }
            return creds;
        }
        public async override Task<string> GetJsonFromSecret(string secretVaultIdentifier)
        {
            Logger.Info(string.Format(DeploySoftware_LaunchPad_AWS_Resources.Logger_Info_GetJsonFromSecret_Getting, secretVaultIdentifier));
            GetSecretValueRequest request = new GetSecretValueRequest();
            request.SecretId = secretVaultIdentifier;
            request.VersionStage = "AWSCURRENT"; // VersionStage defaults to AWSCURRENT if unspecified.

            GetSecretValueResponse response = null;

            try
            {
                response = await SecretClient.GetSecretValueAsync(request);
            }
            catch (DecryptionFailureException e)
            {
                // Secrets Manager can't decrypt the protected secret text using the provided KMS key.\
                Logger.Error(string.Format(DeploySoftware_LaunchPad_AWS_Resources.Logger_Error_GetJsonFromSecret_ExceptionThrown, e.Message));
                throw;
            }
            catch (InternalServiceErrorException e)
            {
                // An error occurred on the server side.
                Logger.Error(string.Format(DeploySoftware_LaunchPad_AWS_Resources.Logger_Error_GetJsonFromSecret_ExceptionThrown, e.Message));
                throw;
            }
            catch (InvalidParameterException e)
            {
                // You provided an invalid value for a parameter.
                Logger.Error(string.Format(DeploySoftware_LaunchPad_AWS_Resources.Logger_Error_GetJsonFromSecret_ExceptionThrown, e.Message));
                throw;
            }
            catch (InvalidRequestException e)
            {
                // You provided a parameter value that is not valid for the current state of the resource.
                Logger.Error(string.Format(DeploySoftware_LaunchPad_AWS_Resources.Logger_Error_GetJsonFromSecret_ExceptionThrown, e.Message));
                throw;
            }
            catch (ResourceNotFoundException e)
            {
                // We can't find the resource that you asked for.
                Logger.Error(string.Format(DeploySoftware_LaunchPad_AWS_Resources.Logger_Error_GetJsonFromSecret_ExceptionThrown, e.Message));
                throw;
            }
            catch (AggregateException e)
            {
                // More than one of the above exceptions were triggered.
                Logger.Error(string.Format(DeploySoftware_LaunchPad_AWS_Resources.Logger_Error_GetJsonFromSecret_ExceptionThrown, e.Message));
                throw;
            }
            Logger.Info(string.Format(DeploySoftware_LaunchPad_AWS_Resources.Logger_Info_GetJsonFromSecret_Got, secretVaultIdentifier));
            return response.SecretString;
        }

        /// <summary>
        /// Get AWS Immutable Credentials where the IAM access key and secret values are stored in an AWS Secret Manager secret.
        /// </summary>
        /// <param name="secretVaultIdentifier">The ARN of the secret in which the IAM values are kept.</param>
        /// <returns>IAM credentials if value, or null</returns>
        public async virtual Task<ImmutableCredentials>  GetCredentialsFromSecret(string secretVaultIdentifier)
        {
            Logger.Info(string.Format(DeploySoftware_LaunchPad_AWS_Resources.Logger_Info_GetCredentialsFromSecret_Getting, secretVaultIdentifier));
            // create the aws credentials given the provided credentials taken from the secret
            dynamic secret = JsonConvert.DeserializeObject(await GetJsonFromSecret(secretVaultIdentifier));            
            string iamAccessKey = secret.apiGatewayIAMAccessKey;
            string iamSecretKey = secret.apiGatewayIAMSecret;
            Logger.Info(string.Format(DeploySoftware_LaunchPad_AWS_Resources.Logger_Info_GetCredentialsFromSecret_Got, secretVaultIdentifier));
            return new ImmutableCredentials(iamAccessKey, iamSecretKey, null);
        }

        /// <summary>
        /// Writes the text value of a particular key, to a given secret ARN
        /// </summary>
        /// <param name="key">The field within the secret to update</param>
        /// <param name="value">The value to update for the given key</param>
        /// <param name="secretVaultIdentifier">The full secret ARN</param>
        /// <returns>A status code with the result of the request</returns>
        public override HttpStatusCode WriteValuesToSecret(IDictionary<string, string> fieldsToInsertOrUpdate, string secretVaultIdentifier)
        {
            return WriteValuesToSecretAsync(fieldsToInsertOrUpdate, secretVaultIdentifier).Result;
        }

        /// <summary>
        /// Writes the text value of a particular key, to a given secret ARN
        /// </summary>
        /// <param name="key">The field within the secret to update</param>
        /// <param name="value">The value to update for the given key</param>
        /// <param name="secretVaultIdentifier">The full secret ARN</param>
        /// <returns>A status code with the result of the request</returns>
        public override async Task<HttpStatusCode> WriteValuesToSecretAsync(IDictionary<string,string> fieldsToInsertOrUpdate, string secretVaultIdentifier)
        {
            string originalSecretJson = await GetJsonFromSecret(secretVaultIdentifier);

            // for each value in the dictionary, try to update the JSON
            string sbUpdatedSecretJson = originalSecretJson;
            foreach(var field in fieldsToInsertOrUpdate)
            {
                sbUpdatedSecretJson = UpdateJsonForSecret(secretVaultIdentifier, sbUpdatedSecretJson, field.Key, field.Value);
            }

            PutSecretValueResponse response = null;

            // Now update the secret
            if (!string.IsNullOrEmpty(sbUpdatedSecretJson))
            {
                PutSecretValueRequest request = new PutSecretValueRequest();
                request.SecretId = secretVaultIdentifier;
                request.SecretString = sbUpdatedSecretJson;
                try
                {
                    response = await SecretClient.PutSecretValueAsync(request);
                }
                catch (EncryptionFailureException e)
                {
                    // Secrets Manager can't encrypt the protected secret text using the provided KMS key.\
                    Logger.Error(string.Format(DeploySoftware_LaunchPad_AWS_Resources.Logger_Error_WriteValueToSecret_Exception, secretVaultIdentifier, e.Message));
                    throw;
                }
                catch (InternalServiceErrorException e)
                {
                    // An error occurred on the server side.
                    Logger.Error(string.Format(DeploySoftware_LaunchPad_AWS_Resources.Logger_Error_WriteValueToSecret_Exception, secretVaultIdentifier, e.Message));
                    throw;
                }
                catch (InvalidParameterException e)
                {
                    // You provided an invalid value for a parameter.
                    Logger.Error(string.Format(DeploySoftware_LaunchPad_AWS_Resources.Logger_Error_WriteValueToSecret_Exception, secretVaultIdentifier, e.Message));
                    throw;
                }
                catch (InvalidRequestException e)
                {
                    // You provided a parameter value that is not valid for the current state of the resource.
                    Logger.Error(string.Format(DeploySoftware_LaunchPad_AWS_Resources.Logger_Error_WriteValueToSecret_Exception, secretVaultIdentifier, e.Message));
                    throw;
                }
                catch (ResourceNotFoundException e)
                {
                    // We can't find the resource that you asked for.
                    Logger.Error(string.Format(DeploySoftware_LaunchPad_AWS_Resources.Logger_Error_WriteValueToSecret_Exception, secretVaultIdentifier, e.Message));
                    throw;
                }
                catch (ResourceExistsException e)
                {
                    // A resource with the ID you requested already exists.
                    Logger.Error(string.Format(DeploySoftware_LaunchPad_AWS_Resources.Logger_Error_WriteValueToSecret_Exception, secretVaultIdentifier, e.Message));
                    throw;
                }
                catch (AggregateException e)
                {
                    // More than one of the above exceptions were triggered.
                    Logger.Error(string.Format(DeploySoftware_LaunchPad_AWS_Resources.Logger_Error_WriteValueToSecret_Exception, secretVaultIdentifier, e.Message));
                    throw;
                }

            }
            return response.HttpStatusCode;
        }

        public override string UpdateJsonForSecret(string secretVaultIdentifier, string originalSecretJson, string key, string value)
        {
            Logger.Info(string.Format(DeploySoftware_LaunchPad_AWS_Resources.Logger_Info_UpdateJsonForSecret_Updating, value, key, secretVaultIdentifier));
            string updatedJsonString = originalSecretJson;

            JObject jObject = Newtonsoft.Json.JsonConvert.DeserializeObject(originalSecretJson) as JObject;
            
            // Try to select the nested property (if it exists) using the key
            JToken jToken = jObject.SelectToken(key);
            if(jToken != null)
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

            Logger.Info(string.Format(DeploySoftware_LaunchPad_AWS_Resources.Logger_Info_UpdateJsonForSecret_Updated, value, key, secretVaultIdentifier));
            return updatedJsonString;
        }

    }
}
