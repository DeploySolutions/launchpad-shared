using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Deploy.LaunchPad.Core.Secrets.Reference
{
    /// <summary>
    /// /// <summary>
    /// This lets a class refer to secrets without storing the actual secret values.
    /// </summary>
    /// </summary>
    [Serializable]
    public partial class SecretFieldReference : ISecretFieldReference
    {
        public required virtual string FieldName { get; init; }

        public required virtual string VaultId { get; init; }
        
        public required virtual SecretVaultType VaultType { get; init; }

        [SetsRequiredMembers]
        public SecretFieldReference(string fieldName, string vaultId, SecretVaultType vaultType)
        {
            FieldName = fieldName;
            VaultId = vaultId;
            VaultType = vaultType;  
        }

    }
}
