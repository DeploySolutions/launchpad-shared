using Abp.Dependency;
using Abp.UI;
using Amazon;
using Amazon.Runtime;
using Amazon.Runtime.CredentialManagement;
using Amazon.SecretsManager;
using Amazon.SecretsManager.Model;
using Castle.Core.Logging;
using DeploySoftware.LaunchPad.Core.AbpModuleConfig;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DeploySoftware.LaunchPad.AWS.SecretsManager
{
    public partial class AwsSecretsManagerHelper : AwsHelperBase, IAwsSecretsManagerHelper
    {
       
        [JsonIgnore]
        public IAmazonSecretsManager SecretClient { get; set; }

        public AwsSecretsManagerHelper() : base()
        {
            Region = GetRegionEndpoint(DefaultRegionName);
            SecretClient = SetSecretClient(Region);
        }

        public AwsSecretsManagerHelper(ILogger logger) : base(logger)
        {
            Region = GetRegionEndpoint(DefaultRegionName);
            SecretClient = SetSecretClient(Region);
        }

        public AwsSecretsManagerHelper(IAmazonSecretsManager client, ILogger logger) : base(logger)
        {
            Region = GetRegionEndpoint(DefaultRegionName);
            SecretClient = client;
        }

        public AwsSecretsManagerHelper(string awsRegionEndpointName, ILogger logger) : base(logger)
        {
            Region = GetRegionEndpoint(awsRegionEndpointName);
            SecretClient = SetSecretClient(Region);
        }

        public AwsSecretsManagerHelper(string awsProfileName, string awsRegionEndpointName, ILogger logger) : base(logger)
        {
            Region = GetRegionEndpoint(awsRegionEndpointName);
            SecretClient = SetSecretClient(Region,awsProfileName);
        }


        /// <summary>
        /// Creates a new Secrets Manager client
        /// </summary>
        /// <returns></returns>
        public virtual AmazonSecretsManagerClient SetSecretClient(RegionEndpoint region)
        {
            return new AmazonSecretsManagerClient(region);
        }

        /// <summary>
        /// Creates a new Secrets Manager client using the provided profile information
        /// </summary>
        /// <param name="profileName"></param>
        /// <param name="awsRegionSystemName"></param>
        /// <returns></returns>
        public virtual AmazonSecretsManagerClient SetSecretClient(RegionEndpoint region, string profileName)
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

        public async virtual Task<ISecretVault> GetSecretVaultAsync(string secretVaultIdentifier, string name, string fullName)
        {
            AwsSecretVault vault = new AwsSecretVault(secretVaultIdentifier, name, fullName);
            vault.Fields = await GetAllFieldsFromSecret(secretVaultIdentifier);
            return vault;
        }


        public async virtual Task<string> GetJsonFromSecretAsync(string secretVaultIdentifier)
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
        public virtual ImmutableCredentials GetCredentialsFromSecret(string secretVaultIdentifier)
        {
            return GetCredentialsFromSecretAsync(secretVaultIdentifier).Result;
        }

        /// <summary>
        /// Get AWS Immutable Credentials where the IAM access key and secret values are stored in an AWS Secret Manager secret.
        /// </summary>
        /// <param name="secretVaultIdentifier">The ARN of the secret in which the IAM values are kept.</param>
        /// <returns>IAM credentials if value, or null</returns>
        public async virtual Task<ImmutableCredentials> GetCredentialsFromSecretAsync(string secretVaultIdentifier)
        {
            Logger.Info(string.Format(DeploySoftware_LaunchPad_AWS_Resources.Logger_Info_GetCredentialsFromSecret_Getting, secretVaultIdentifier));
            // create the aws credentials given the provided credentials taken from the secret
            dynamic secret = JsonConvert.DeserializeObject(await GetJsonFromSecretAsync(secretVaultIdentifier));            
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
        public virtual HttpStatusCode WriteValuesToSecret(IDictionary<string, string> fieldsToInsertOrUpdate, string secretVaultIdentifier)
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
        public async virtual Task<HttpStatusCode> WriteValuesToSecretAsync(IDictionary<string,string> fieldsToInsertOrUpdate, string secretVaultIdentifier)
        {
            string originalSecretJson = await GetJsonFromSecretAsync(secretVaultIdentifier);

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

        public virtual string UpdateJsonForSecret(string secretVaultIdentifier, string originalSecretJson, string key, string value)
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

        /// <summary>
        /// Returns a valid database connection string which is stored in "dbConnectionString" key in a Secrets Manager secret.
        /// </summary>
        /// <param name="secretVaultIdentifier">The AWS ARN of the secret in which the key is located.</param>
        /// <returns>A SQL connection string</returns>
        public async virtual Task<string> GetDbConnectionStringFromSecretAsync(string secretVaultIdentifier, string connectionStringFieldName)
        {
            Logger.Info(string.Format("Getting DB Connection string from Secrets Manager for secret ARN {0}", secretVaultIdentifier));
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
                    Logger.Error(string.Format("An exception was thrown while attempting to GetDbConnectionStringFromSecret for ARN: {0}. The message was {1}.", secretVaultIdentifier, jEx.Message));
                }
            }
            Console.WriteLine("AWS connection string: " + connectionString);
            Logger.Info(string.Format("Got DB Connection string from Secrets Manager for secret ARN {0}", secretVaultIdentifier));
            return connectionString;
        }

        public virtual string GetDbConnectionStringFromSecret(string secretVaultIdentifier, string connectionStringFieldName)
        {
            return GetDbConnectionStringFromSecretAsync(secretVaultIdentifier, connectionStringFieldName).Result;
        }

        public virtual string GetJsonFromSecret(string secretVaultIdentifier)
        {
            return GetJsonFromSecretAsync(secretVaultIdentifier).Result;
        }

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

        public virtual ISecretVault GetSecretVault(string secretVaultIdentifier, string name, string fullName)
        {
            return GetSecretVaultAsync(secretVaultIdentifier, name, fullName).Result;
        }

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
    }
}
