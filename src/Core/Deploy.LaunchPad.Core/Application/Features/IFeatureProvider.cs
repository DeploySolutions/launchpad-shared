namespace Deploy.LaunchPad.Core.Application.Features
{
    public partial interface IFeatureProvider
    {
        void SetFeatures(IFeatureDefinitionContext context);
    }
}