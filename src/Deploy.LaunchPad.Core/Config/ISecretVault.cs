using System.Collections.Generic;

namespace Deploy.LaunchPad.Core.Config
{
    public interface ISecretVault
    {
        public IDictionary<string, string> Fields { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Id { get; }

        public string VaultId { get; set; }

        public string ProviderId { get; set; }

        public string GetValue(string key, string caller);

        public IDictionary<string, string> FindValuesForKeys(IList<string> keys, string caller);


    }
}