using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeploySoftware.LaunchPad.Core.AbpModuleConfig
{
    public interface ISecretProvider<TSecretVault>
        where TSecretVault : SecretVaultBase, new()
    {
        /// <summary>
        /// Contains a dictionary of "secret vaults", keyed using the friendly name of the vault. 
        /// Each secret vault contains within it an inner dictionary of key value pairs representing a unique field contained within the vault, and the field's value. 
        /// The secret vault also contains the unique identifer (such as Azure Key Vault identifier or AWS ARN) of the secret in which the secrets are stored.
        /// Note to implementers: Do not store or record this information!
        /// </summary>
        [NotMapped]
        public Dictionary<string, TSecretVault> SecretVaults { get; set; }

        public bool RefreshSecretVault(string vaultSecretIdentifier, string vaultName, string vaultFullName, SecretHelper helper);

        public Task<bool> RefreshSecretVaultAsync(string vaultSecretIdentifier, string vaultName, string vaultFullName, SecretHelper helper);

        public void RefreshAllSecretVaults(SecretHelper helper);
    }
}
