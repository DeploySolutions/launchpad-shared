using System;
using System.Collections.Generic;

namespace DeploySoftware.LaunchPad.Core.Config
{
    public partial class SecretVault : ISecretVault
    {
        public virtual IDictionary<string, string> Fields { get; set; }
        public virtual string Name { get; set; }

        public virtual string Description { get; set; } 

        public virtual string Id { get
            {
                return ProviderId + "." + VaultId;
            }
        }

        public virtual string VaultId { get; set; }
        public virtual string ProviderId { get; set; }

        public SecretVault()
        {
            Name = string.Empty;
            Description = string.Empty;
            ProviderId = string.Empty;
            VaultId = string.Empty;
            var comparer = StringComparer.OrdinalIgnoreCase;
            Fields = new Dictionary<string, string>(comparer);

        }

        public SecretVault(string providerId, string vaultId, string vaultName)
        {
            Name = vaultName;
            ProviderId = providerId;
            VaultId = vaultId;
            Description = "Vault for " + providerId + "." + vaultName;
            var comparer = StringComparer.OrdinalIgnoreCase;
            Fields = new Dictionary<string, string>(comparer);

        }

        public SecretVault(string providerId, string vaultId, string vaultName, IDictionary<string, string> fields, string description = "")
        {
            Name = vaultName;
            ProviderId = providerId;
            VaultId = vaultId;
            if(description == string.Empty)
            {
                Description = "Vault for " + providerId + "." + vaultName;
            }
            else
            {
                Description = description;
            }
            Fields = fields;
        }


    }
}