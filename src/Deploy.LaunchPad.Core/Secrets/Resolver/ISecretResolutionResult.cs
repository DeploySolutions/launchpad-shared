using Deploy.LaunchPad.Core.Secrets.Reference;

namespace Deploy.LaunchPad.Core.Secrets.Resolver
{
    public partial interface ISecretResolutionResult
    {
        string FieldName { get; init; }
        string FieldValue { get; init; }
        ISecretFieldReference ResolvedBy { get; init; }
        bool WasFound { get; init; }
    }
}