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

        public ISecretFieldReference? ResolvedBy { get; init; }

        [SetsRequiredMembers]
        public SecretResolutionResult(string fieldName)
        {
            FieldName = fieldName;
        }

        [SetsRequiredMembers]
        public SecretResolutionResult(string fieldName, string fieldValue,ISecretFieldReference resolvedBy)
        {
            FieldName = fieldName;
            FieldValue = fieldValue;
            ResolvedBy = resolvedBy;
            WasFound = true;
        }

        [SetsRequiredMembers]
        public SecretResolutionResult(string fieldName, string? fieldValue, bool wasFound, ISecretFieldReference? resolvedBy)
        {
            FieldName = fieldName;
            FieldValue = fieldValue;
            WasFound = wasFound;
            ResolvedBy = resolvedBy;
        }

    }
}
