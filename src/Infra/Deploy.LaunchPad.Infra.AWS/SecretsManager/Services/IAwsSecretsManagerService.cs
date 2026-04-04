// ***********************************************************************
// Assembly         : Deploy.LaunchPad.AWS
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 03-21-2023
// ***********************************************************************
// <copyright file="IAwsSecretsManagerService.cs" company="Deploy Software Solutions, inc.">
//     2021-2023 Deploy Software Solutions, inc.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Amazon;
using Amazon.SecretsManager;
using Deploy.LaunchPad.Code.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Deploy.LaunchPad.Infra.AWS.SecretsManager.Services
{
    /// <summary>
    /// Interface IAwsSecretsManagerService
    /// Extends the <see cref="ILaunchPadSystemIntegrationService" />
    /// </summary>
    /// <seealso cref="ILaunchPadSystemIntegrationService" />
    public partial interface IAwsSecretsManagerService : ILaunchPadSystemIntegrationService
    {
        public IAmazonSecretsManager Client { get; }

        public RegionEndpoint Region { get; }

        public Task<string> GetPlaintextFromFromSecretVaultAsync(string arn);
        
        public Task<T> GetObjectFromSecretVaultAsync<T>(string arn);

        public Task<Dictionary<string, string>> GetDictionaryFromSecretAsync(string arn);
    }
}
