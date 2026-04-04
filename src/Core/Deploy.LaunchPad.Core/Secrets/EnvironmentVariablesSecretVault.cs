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


using Castle.Core.Logging;
using Deploy.LaunchPad.Core.Configuration;
using Deploy.LaunchPad.Core.Secrets;
using Deploy.LaunchPad.Core.Secrets.Reference;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;

namespace Deploy.LaunchPad.AWS
{
    /// <summary>
    /// Class AwsSecretVault.
    /// Implements the <see cref="SecretVault" />
    /// Implements the <see cref="ISecretVault" />
    /// </summary>
    /// <seealso cref="SecretVault" />
    /// <seealso cref="ISecretVault" />
    public partial class EnvironmentVariablesSecretVault : SecretVault
    {


        /// <summary>
        /// Initializes a new instance of the <see cref="EnvironmentVariablesSecretVault"/> class.
        /// </summary>
        [SetsRequiredMembers]
        public EnvironmentVariablesSecretVault(ILogger logger, string vaultId) : base(logger, vaultId)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AwsSecretVault"/> class.
        /// </summary>
        /// <param name="arn">The arn.</param>
        /// <param name="name">The name.</param>
        [SetsRequiredMembers]
        public EnvironmentVariablesSecretVault(ILogger logger, string vaultId, string name) : base(logger, vaultId, name)
        {
        }

        public override async Task<string?> GetValueOrNullFromSecretReferenceAsync(
            ISecretFieldReference source,
            ISettingDefinition definition,
            CancellationToken cancellationToken = default)
        {
            return await Task.FromResult(GetValueOrNullFromSecretReference(source, definition));
        }

        public override string? GetValueOrNullFromSecretReference(
            ISecretFieldReference source,
            ISettingDefinition definition)
        {
            // lookup from local computer Environment Settings
            return Environment.GetEnvironmentVariable(source.FieldName);
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
            IDictionary<string, ISettingDefinition> kvps = new Dictionary<string, ISettingDefinition>();
            // loop through the desired set of keys to find the corresponding values in the JSON
            foreach (string key in keys)
            {
                ISettingDefinition value = null;
                if (keyIsCaseInsensitive)
                {
                    value = GetValue(key.ToLower(), "GetValuesForKeysAsync");
                }
                else
                {
                    value = GetValue(key, "GetValuesForKeysAsync");
                }
                if (value != null)
                {
                    kvps.Add(key, value);
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
            throw new NotImplementedException();
        }

        public override ISettingDefinition GetValue(string key, string caller, bool keyIsCaseInsensitive = true)
        {
            return GetValueAsync(key, caller, keyIsCaseInsensitive).Result;
        }

        public override async Task<ISettingDefinition> GetValueAsync(string key, string caller, bool keyIsCaseInsensitive = true)
        {
            // lookup from local computer Environment Settings
            string value = Environment.GetEnvironmentVariable(key);

            // Create a SettingDefinition (constructor may need adjustment based on your actual implementation)
            return new SettingDefinition(key, value);
        }
    }
}
