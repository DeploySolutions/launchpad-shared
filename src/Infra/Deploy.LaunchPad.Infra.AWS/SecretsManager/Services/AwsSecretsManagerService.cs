// ***********************************************************************
// Assembly         : Deploy.LaunchPad.Infra.AWS.Abp
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 03-21-2023
// ***********************************************************************
// <copyright file="AwsSecretsManagerService.cs" company="Deploy Software Solutions, inc.">
//     2021-2023 Deploy Software Solutions, inc.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Amazon;
using Amazon.SecretsManager;
using Deploy.LaunchPad.Code.Services;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Deploy.LaunchPad.Infra.AWS.SecretsManager.Services
{
    /// <summary>
    /// Class AwsSecretsManagerService.
    /// Implements the <see cref="LaunchPadAbpSystemIntegrationServiceBase" />
    /// Implements the <see cref="IAwsSecretsManagerService" />
    /// </summary>
    /// <seealso cref="LaunchPadAbpSystemIntegrationServiceBase" />
    /// <seealso cref="IAwsSecretsManagerService" />
    public partial class AwsSecretsManagerService : SystemIntegrationServiceBase, IAwsSecretsManagerService
    {
        /// <summary>
        /// The aws client
        /// </summary>
        protected readonly IAmazonSecretsManager _awsClient;

        /// <summary>
        /// Gets the secrets manager client.
        /// </summary>
        /// <value>The secrets manager client.</value>
        [JsonIgnore]
        public virtual IAmazonSecretsManager Client { get { return _awsClient; } }

        protected readonly RegionEndpoint _region = RegionEndpoint.USEast1;
        public RegionEndpoint Region { get { return _region; } }

        /// <summary>
        /// Initializes a new instance of the <see cref="AwsSecretsManagerService"/> class.
        /// </summary>
        public AwsSecretsManagerService() : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AwsSecretsManagerService"/> class.
        /// </summary>
        public AwsSecretsManagerService(string regionName) : base()
        {
            _region = RegionEndpoint.GetBySystemName(regionName);
            _awsClient = new AmazonSecretsManagerClient(_region);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AwsSecretsManagerService"/> class.
        /// </summary>
        public AwsSecretsManagerService(IAmazonSecretsManager secretsManagerClient) : base()
        {
            _awsClient = secretsManagerClient;
        }

        /// <summary>
        /// Returns the "Plaintext" value of the secret stored in AWS Secrets Manager for the given ARN. This is the unencrypted value of the secret, 
        /// which may be a JSON string containing multiple key-value pairs, or a simple string value.
        /// </summary>
        /// <param name="arn"></param>
        /// <returns></returns>
        public virtual async Task<string> GetPlaintextFromFromSecretVaultAsync(string arn)
        {
            var getSecretValueRequest = new Amazon.SecretsManager.Model.GetSecretValueRequest
            {
                SecretId = arn
            };

            var getSecretValueResponse = await Client.GetSecretValueAsync(getSecretValueRequest);
            string secretJson = getSecretValueResponse.SecretString;
            return secretJson;
        }

        /// <summary>
        /// Returns a typed object deserialized from the "Plaintext" value of the secret stored in AWS Secrets Manager for the given ARN. 
        /// This is the unencrypted value of the secret
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="arn"></param>
        /// <returns></returns>
        public virtual async Task<T> GetObjectFromSecretVaultAsync<T>(string arn)
        {
            string secretJson = await GetPlaintextFromFromSecretVaultAsync(arn);
            return JsonConvert.DeserializeObject<T>(secretJson);
        }

        /// <summary>
        /// Returns a dictionary of string key-value pairs deserialized from the 
        /// "Plaintext" value of the secret stored in AWS Secrets Manager for the given ARN.
        /// </summary>
        /// <param name="arn"></param>
        /// <returns></returns>
        public virtual async Task<Dictionary<string, string>> GetDictionaryFromSecretAsync(string arn)
        {
            string secretJson = await GetPlaintextFromFromSecretVaultAsync(arn);
            return JsonConvert.DeserializeObject<Dictionary<string, string>>(secretJson);
        }
    }
}
