using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeploySoftware.LaunchPad.Core.Config
{
    public abstract partial class SecretVaultBase : ISecretVault
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

        public SecretVaultBase()
        {
            Name = string.Empty;
            Description = string.Empty;
            ProviderId = string.Empty;
            VaultId = string.Empty;
            var comparer = StringComparer.OrdinalIgnoreCase;
            Fields = new Dictionary<string, string>(comparer);

        }

        public SecretVaultBase(string providerId, string vaultId, string vaultName)
        {
            Name = vaultName;
            ProviderId = providerId;
            VaultId = vaultId;
            Description = "Vault for " + providerId + "." + vaultName;
            var comparer = StringComparer.OrdinalIgnoreCase;
            Fields = new Dictionary<string, string>(comparer);

        }

        public SecretVaultBase(string providerId, string vaultId, string vaultName, IDictionary<string, string> fields, string description = "")
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

        public virtual string GetValue(string key, string caller)
        {
            Fields.TryGetValue(key, out string value);
            return value;
        }

        public virtual IDictionary<string, string> FindValuesForKeys(IList<string> keys, string caller)
        {
            IDictionary<string, string> kvps = new Dictionary<string, string>();
            // loop through the desired set of keys to find the corresponding values in the JSON

            kvps = (IDictionary<string, string>)keys.Where(k => Fields.ContainsKey(k));
            
            return kvps;
        }

    }
}