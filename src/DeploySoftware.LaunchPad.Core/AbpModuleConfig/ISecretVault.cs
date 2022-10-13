using System.Collections.Generic;

namespace DeploySoftware.LaunchPad.Core.AbpModuleConfig
{
    public interface ISecretVault
    {
        public IDictionary<string, string> Fields { get; set; }

        public string FullName { get; set; }
        
        public string Identifier { get; set; }
        
        public string Name { get; set; }
    }
}