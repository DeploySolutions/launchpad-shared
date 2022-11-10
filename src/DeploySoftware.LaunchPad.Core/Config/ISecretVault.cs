using System.Collections.Generic;

namespace DeploySoftware.LaunchPad.Core.Config
{
    public interface ISecretVault
    {
        public IDictionary<string, string> Fields { get; set; }
        
        public string Name { get; set; }

        public string Description { get; set; }

        public string Id { get; }
        
        public string VaultId { get; set; }

        public string ProviderId { get; set; }
    }
}