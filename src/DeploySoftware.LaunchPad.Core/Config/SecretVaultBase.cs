using System;
using System.Collections.Generic;

namespace DeploySoftware.LaunchPad.Core.Config
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

        public SecretVaultBase(string secretIdentifier)
        {
            Name = string.Empty;
            FullName = string.Empty;
            Identifier = secretIdentifier;
            var comparer = StringComparer.OrdinalIgnoreCase;
            Fields = new Dictionary<string, string>(comparer);

        }

        public SecretVaultBase(string secretIdentifier, string name)
        {
            Name = name;
            FullName = name;
            Identifier = secretIdentifier;
            var comparer = StringComparer.OrdinalIgnoreCase;
            Fields = new Dictionary<string, string>(comparer);

        }

        public SecretVaultBase(string secretIdentifier, string name, string fullName)
        {
            Name = name;
            FullName = fullName;
            Identifier = secretIdentifier;
            var comparer = StringComparer.OrdinalIgnoreCase;
            Fields = new Dictionary<string, string>(comparer);

        }

        public SecretVaultBase(string secretIdentifier, string name, string fullName, IDictionary<string, string> fields)
        {
            Name = name;
            FullName = fullName;
            Identifier = secretIdentifier;
            Fields = fields;

        }

    }
}