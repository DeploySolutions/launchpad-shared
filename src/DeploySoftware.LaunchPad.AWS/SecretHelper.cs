﻿using Amazon;
using Amazon.Runtime;
using Amazon.SecretsManager;
using Amazon.SecretsManager.Model;
using Castle.Core.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Threading.Tasks;

namespace DeploySoftware.LaunchPad.AWS
{
    public partial class SecretHelper : AwsHelperBase
    {

        public ILogger Logger { get; set; }

        public IAmazonSecretsManager SecretClient { get; set; }


        public SecretHelper(ILogger logger)
        {
            Logger = logger;
            SecretClient = GetSecretClient();
        }

        public SecretHelper(ILogger logger, IAmazonSecretsManager client)
        {
            Logger = logger;
            SecretClient = client;
        }

        public SecretHelper(ILogger logger, string awsProfileName, string awsRegionSystemName)
        {
            Logger = logger;
            SecretClient = GetSecretClient(awsProfileName, awsRegionSystemName);
        }

        /// <summary>
        /// Creates a new Secrets Manager client
        /// </summary>
        /// <returns></returns>
        protected AmazonSecretsManagerClient GetSecretClient()
        {
            return new AmazonSecretsManagerClient();
        }

        /// <summary>
        /// Creates a new Secrets Manager client using the provided profile information
        /// </summary>
        /// <param name="profileName"></param>
        /// <param name="awsRegionSystemName"></param>
        /// <returns></returns>
        protected AmazonSecretsManagerClient GetSecretClient(string profileName, string awsRegionSystemName)
        {
            if(string.IsNullOrEmpty(profileName))
            {
                profileName = "default";
            }
            if (string.IsNullOrEmpty(awsRegionSystemName))
            {
                awsRegionSystemName = "us-east-1";
            }
            Logger.Info(string.Format(DeploySoftware_LaunchPad_AWS_Resources.SecretHelper_GetSecretClient_ProfileName, profileName));
            Logger.Info(string.Format(DeploySoftware_LaunchPad_AWS_Resources.SecretHelper_GetSecretClient_Region, profileName));

            RegionEndpoint region = RegionEndpoint.GetBySystemName(awsRegionSystemName);
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
                client = GetSecretClient();
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
    }
}
