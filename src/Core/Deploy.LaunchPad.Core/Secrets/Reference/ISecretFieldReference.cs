using System;
using System.Collections.Generic;
using System.Text;

namespace Deploy.LaunchPad.Core.Secrets.Reference
{
    /// <summary>
    /// This lets a class refer to secrets without storing the actual secret values.
    /// </summary>
    public partial interface ISecretFieldReference
    {
        string FieldName { get; }
        
        string VaultName { get; }

        string? ResolvedValue { get; set; }
    }
}
