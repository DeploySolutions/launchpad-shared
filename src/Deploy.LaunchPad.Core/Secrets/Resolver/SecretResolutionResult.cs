using Deploy.LaunchPad.Core.Secrets.Reference;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Deploy.LaunchPad.Core.Secrets.Resolver
{
    /// <summary>
    /// When an attempt is made to resolve a secret value, this is the result object that is returned. 
    /// It contains information about whether the value was found, which provider it was resolved by, 
    /// and the resolved value itself (if found).
    /// </summary>
    public sealed class SecretResolutionResult : ISecretResolutionResult
    {
        public required string FieldName { get; init; }

        public string? FieldValue { get; init; }

        public bool WasFound { get; init; } = false;

        [SetsRequiredMembers]
        public SecretResolutionResult(string fieldName)
        {
            FieldName = fieldName;
            WasFound = false;
        }

        [SetsRequiredMembers]
        public SecretResolutionResult(string fieldName, string fieldValue)
        {
            FieldName = fieldName;
            FieldValue = fieldValue;
            if(!string.IsNullOrEmpty(fieldValue))
            {
                WasFound = true;
            }
        }

    }
}
