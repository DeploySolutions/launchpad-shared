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
using Amazon.CloudFront.Model;
using Amazon.Runtime.Internal.Transform;
using Castle.Core.Logging;
using Deploy.LaunchPad.Core.Configuration;
using Deploy.LaunchPad.Core.Secrets;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Deploy.LaunchPad.AWS.SecretsManager
{
    /// <summary>
    /// Class AwsSecretProvider.
    /// Implements the <see cref="SecretProviderBase" />
    /// Implements the <see cref="ISecretProvider" />
    /// </summary>
    /// <seealso cref="SecretProviderBase" />
    /// <seealso cref="ISecretProvider" />
    public partial class AwsSecretProvider : SecretProviderBase
    {


        /// <summary>
        /// Initializes a new instance of the <see cref="AwsSecretProvider"/> class.
        /// </summary>
        /// <param name="regionName">Name of the region.</param>
        /// <param name="awsProfileName">Name of the aws profile.</param>
        /// <param name="shouldUseLocalAwsProfile">if set to <c>true</c> [should use local aws profile].</param>
        public AwsSecretProvider() : base()
        {
           
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AwsSecretProvider"/> class.
        /// </summary>
        /// <param name="regionName">Name of the region.</param>
        /// <param name="awsProfileName">Name of the aws profile.</param>
        /// <param name="shouldUseLocalAwsProfile">if set to <c>true</c> [should use local aws profile].</param>
        public AwsSecretProvider(ILogger logger) : base(logger)
        {

        }

        /// <summary>
        /// Get secret vault by identifier as an asynchronous operation.
        /// </summary>
        /// <param name="arn">The arn.</param>
        /// <param name="caller">The caller.</param>
        /// <returns>A Task&lt;ISecretVault&gt; representing the asynchronous operation.</returns>
        public async override Task<ISecretVault> GetSecretVaultByIdAsync(string arn, string caller)
        {
            AwsSecretVault vault = new AwsSecretVault(Logger,arn);
            vault.Fields = await vault.GetAllValuesAsync(caller);
            return vault;
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
            secretVault.Fields = await secretVault.GetAllValuesAsync(caller);
            return secretVault;
        }

        public override void PopulateSecretVaults(ISecretProviderContext context)
        {
            // add environment variables
            EnvironmentVariablesSecretVault environmentVariables = new EnvironmentVariablesSecretVault(Logger, "EnvironmentVariables");
            var newSecret = new SettingDefinition("AWS_ACCESS_KEY_ID", "AWS Access Key Id")
            { 
                SecretSources = new List<SettingSecretProviderDescriptor>() {
                    new SettingSecretProviderDescriptor()
                    {
                        VaultId = environmentVariables.VaultId,
                        Key = "AWS_ACCESS_KEY_ID"
                    }
                },
                ClientVisibilityProvider = new HiddenSettingClientVisibilityProvider(),
                IsEncrypted = true
            };
            environmentVariables.AddField(newSecret.Name, newSecret);
            SecretVaults.Add(environmentVariables.VaultId, environmentVariables);
            context.Manager.Secrets.Add(newSecret.Name,newSecret);

            // add user secrets json


        }
    }
}
