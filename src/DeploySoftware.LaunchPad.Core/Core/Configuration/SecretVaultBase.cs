using System;
using System.Collections.Generic;

namespace DeploySoftware.LaunchPad.Core.Configuration
{
    public abstract partial class SecretVaultBase : ISecretVault
    {
        public virtual IDictionary<string, string> Fields { get; set; }
        public virtual string FullName { get; set; }
        public virtual string Identifier { get; set; }
        public virtual string Name { get; set; }

        public SecretVaultBase()
        {
            Name = string.Empty;
            FullName = string.Empty;
            Identifier = string.Empty;
            var comparer = StringComparer.OrdinalIgnoreCase;
            Fields = new Dictionary<string, string>(comparer);

        }
    }
}