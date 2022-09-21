using Abp.Dependency;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeploySoftware.LaunchPad.Core.AbpModuleConfig
{
    public abstract partial class SecretProviderBase<TSecretVault> : ISecretProvider<TSecretVault>, ISingletonDependency
        where TSecretVault : SecretVaultBase, new()
    {
        /// <summary>
        /// Contains an outer dictionary of "secret vaults". 
        /// Each secret vault contains an inner dictionary of string pairs representing a unique field contained within the vault, and the field's value. 
        /// The outer dictionary key is the unique identifer (such as Azure Key Vault identifier or AWS ARN) of the secret in which the secrets are stored.
        /// Note to implementers: Do not store or record this information!
        /// </summary>
        [NotMapped]
        [JsonIgnore]
        public Dictionary<string, TSecretVault> SecretVaults { get; set; }

        public SecretProviderBase()
        {
            var comparer = StringComparer.OrdinalIgnoreCase;
            SecretVaults = new Dictionary<string, TSecretVault>(comparer);
        }

        public abstract bool RefreshSecretVault(string vaultSecretIdentifier, string vaultName, string vaultFullName, SecretHelper helper, string caller); 
        
        public abstract Task<bool> RefreshSecretVaultAsync(string vaultSecretIdentifier, string vaultName, string vaultFullName, SecretHelper helper, string caller);

        public void RefreshAllSecretVaults(SecretHelper helper, string caller)
        {
            foreach(var vault in SecretVaults.Values)
            {
                // update the vaults in our dictionary
                RefreshSecretVault(
                    vault.Identifier, 
                    vault.Name,
                    vault.FullName,
                    helper, 
                    caller);
            }
        }


    }
}
