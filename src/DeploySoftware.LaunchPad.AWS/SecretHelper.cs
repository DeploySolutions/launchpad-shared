using Abp.UI;
using Amazon;
using Amazon.Runtime;
using Amazon.SecretsManager;
using Amazon.SecretsManager.Model;
using Castle.Core.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DeploySoftware.LaunchPad.AWS
{
    public partial class SecretHelper : AwsHelperBase
    {

        public IAmazonSecretsManager SecretClient { get; set; }


        public SecretHelper(ILogger logger) : base(logger)
        {
            SecretClient = GetSecretClient(Region);
        }

        public SecretHelper(IAmazonSecretsManager client, ILogger logger) : base(logger)
        {
            SecretClient = client;
        }

        public SecretHelper(string awsRegionEndpointName, ILogger logger) : base(awsRegionEndpointName, logger)
        {
            SecretClient = GetSecretClient(Region);
        }

        public SecretHelper(string awsProfileName, string awsRegionEndpointName, ILogger logger) : base(awsRegionEndpointName,logger)
        {
            SecretClient = GetSecretClient(awsProfileName, Region);
        }

        /// <summary>
        /// Creates a new Secrets Manager client
        /// </summary>
        /// <returns></returns>
        protected AmazonSecretsManagerClient GetSecretClient(RegionEndpoint region)
        {
            return new AmazonSecretsManagerClient(region);
        }

        /// <summary>
        /// Creates a new Secrets Manager client using the provided profile information
        /// </summary>
        /// <param name="profileName"></param>
        /// <param name="awsRegionSystemName"></param>
        /// <returns></returns>
        protected AmazonSecretsManagerClient GetSecretClient(string profileName, RegionEndpoint region)
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
                client = GetSecretClient(region);
            }            
            return client;

        }

        public async virtual Task<string> GetJsonFromSecret(string secretArn)
        {
            Logger.Info(string.Format(DeploySoftware_LaunchPad_AWS_Resources.Logger_Info_GetJsonFromSecret_Getting, secretArn));
            GetSecretValueRequest request = new GetSecretValueRequest();
            request.SecretId = secretArn;
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
            Logger.Info(string.Format(DeploySoftware_LaunchPad_AWS_Resources.Logger_Info_GetJsonFromSecret_Got, secretArn));
            return response.SecretString;
        }

        /// <summary>
        /// Returns the text value of of a particular key, from a given secret ARN
        /// </summary>
        /// <param name="key"></param>
        /// <param name="secretArn"></param>
        /// <returns></returns>
        public async Task<string> GetValueFromSecret(string key, string secretArn)
        {
            string secretStringJson = await GetJsonFromSecret(secretArn);
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
        /// Get AWS Immutable Credentials where the IAM access key and secret values are stored in an AWS Secret Manager secret.
        /// </summary>
        /// <param name="secretArn">The ARN of the secret in which the IAM values are kept.</param>
        /// <returns>IAM credentials if value, or null</returns>
        public async virtual Task<ImmutableCredentials>  GetCredentialsFromSecret(string secretArn)
        {
            Logger.Info(string.Format(DeploySoftware_LaunchPad_AWS_Resources.Logger_Info_GetCredentialsFromSecret_Getting, secretArn));
            // create the aws credentials given the provided credentials taken from the secret
            dynamic secret = JsonConvert.DeserializeObject(await GetJsonFromSecret(secretArn));            
            string iamAccessKey = secret.apiGatewayIAMAccessKey;
            string iamSecretKey = secret.apiGatewayIAMSecret;
            Logger.Info(string.Format(DeploySoftware_LaunchPad_AWS_Resources.Logger_Info_GetCredentialsFromSecret_Got, secretArn));
            return new ImmutableCredentials(iamAccessKey, iamSecretKey, null);
        }

        /// <summary>
        /// Returns a valid database connection string which is stored in "dbConnectionString" key in a Secrets Manager secret.
        /// </summary>
        /// <param name="secretArn">The AWS ARN of the secret in which the key is located.</param>
        /// <returns>A SQL connection string</returns>
        public async virtual Task<string> GetDbConnectionStringFromSecret(string secretArn)
        {
            Logger.Info(string.Format(DeploySoftware_LaunchPad_AWS_Resources.Logger_Info_GetDbConnectionStringFromSecret_Getting, secretArn));
            string connectionStringJson = await GetJsonFromSecret(secretArn);            
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
                    Logger.Error(string.Format(DeploySoftware_LaunchPad_AWS_Resources.Logger_Error_GetDbConnectionStringFromSecret_ExceptionThrown, secretArn, jEx.Message));
                }
            }
            Console.WriteLine("AWS connection string: " + connectionString);
            Logger.Info(string.Format(DeploySoftware_LaunchPad_AWS_Resources.Logger_Info_GetDbConnectionStringFromSecret_Got, secretArn));
            return connectionString;
        }

        /// <summary>
        /// Writes the text value of a particular key, to a given secret ARN
        /// </summary>
        /// <param name="key">The field within the secret to update</param>
        /// <param name="value">The value to update for the given key</param>
        /// <param name="secretArn">The full secret ARN</param>
        /// <returns>A status code with the result of the request</returns>
        public async Task<HttpStatusCode> WriteValuesToSecret(IDictionary<string,string> fieldsToInsertOrUpdate, string secretArn)
        {
            string originalSecretJson = await GetJsonFromSecret(secretArn);

            // for each value in the dictionary, try to update the JSON
            string sbUpdatedSecretJson = originalSecretJson;
            foreach(var field in fieldsToInsertOrUpdate)
            {
                sbUpdatedSecretJson = UpdateJsonForSecret(secretArn, sbUpdatedSecretJson, field.Key, field.Value);
            }

            PutSecretValueResponse response = null;

            // Now update the secret
            if (!string.IsNullOrEmpty(sbUpdatedSecretJson))
            {
                PutSecretValueRequest request = new PutSecretValueRequest();
                request.SecretId = secretArn;
                request.SecretString = sbUpdatedSecretJson;
                try
                {
                    response = await SecretClient.PutSecretValueAsync(request);
                }
                catch (EncryptionFailureException e)
                {
                    // Secrets Manager can't encrypt the protected secret text using the provided KMS key.\
                    Logger.Error(string.Format(DeploySoftware_LaunchPad_AWS_Resources.Logger_Error_WriteValueToSecret_Exception, secretArn, e.Message));
                    throw;
                }
                catch (InternalServiceErrorException e)
                {
                    // An error occurred on the server side.
                    Logger.Error(string.Format(DeploySoftware_LaunchPad_AWS_Resources.Logger_Error_WriteValueToSecret_Exception, secretArn, e.Message));
                    throw;
                }
                catch (InvalidParameterException e)
                {
                    // You provided an invalid value for a parameter.
                    Logger.Error(string.Format(DeploySoftware_LaunchPad_AWS_Resources.Logger_Error_WriteValueToSecret_Exception, secretArn, e.Message));
                    throw;
                }
                catch (InvalidRequestException e)
                {
                    // You provided a parameter value that is not valid for the current state of the resource.
                    Logger.Error(string.Format(DeploySoftware_LaunchPad_AWS_Resources.Logger_Error_WriteValueToSecret_Exception, secretArn, e.Message));
                    throw;
                }
                catch (ResourceNotFoundException e)
                {
                    // We can't find the resource that you asked for.
                    Logger.Error(string.Format(DeploySoftware_LaunchPad_AWS_Resources.Logger_Error_WriteValueToSecret_Exception, secretArn, e.Message));
                    throw;
                }
                catch (ResourceExistsException e)
                {
                    // A resource with the ID you requested already exists.
                    Logger.Error(string.Format(DeploySoftware_LaunchPad_AWS_Resources.Logger_Error_WriteValueToSecret_Exception, secretArn, e.Message));
                    throw;
                }
                catch (AggregateException e)
                {
                    // More than one of the above exceptions were triggered.
                    Logger.Error(string.Format(DeploySoftware_LaunchPad_AWS_Resources.Logger_Error_WriteValueToSecret_Exception, secretArn, e.Message));
                    throw;
                }

            }
            return response.HttpStatusCode;
        }

        public virtual string UpdateJsonForSecret(string secretArn, string originalSecretJson, string key, string value)
        {
            Logger.Info(string.Format(DeploySoftware_LaunchPad_AWS_Resources.Logger_Info_UpdateJsonForSecret_Updating, value, key, secretArn));
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

            Logger.Info(string.Format(DeploySoftware_LaunchPad_AWS_Resources.Logger_Info_UpdateJsonForSecret_Updated, value, key, secretArn));
            return updatedJsonString;
        }

    }
}
