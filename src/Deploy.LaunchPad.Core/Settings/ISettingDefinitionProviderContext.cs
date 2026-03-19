namespace Deploy.LaunchPad.Core.Configuration
{
    public partial interface ISettingDefinitionProviderContext
    {
        ISettingDefinitionManager Manager { get; }
    }
}