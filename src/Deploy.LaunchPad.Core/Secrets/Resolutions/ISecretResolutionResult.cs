using Deploy.LaunchPad.Core.Secrets.References;

namespace Deploy.LaunchPad.Core.Secrets.Resolutions
{
    public interface ISecretResolutionResult
    {
        string FieldName { get; init; }
        string FieldValue { get; init; }
        ISecretFieldReference ResolvedBy { get; init; }
        bool WasFound { get; init; }
    }
}