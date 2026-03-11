namespace Deploy.LaunchPad.Core.Configuration
{
    public interface ISettingDefinitionProviderContext
    {
        ISettingDefinitionManager Manager { get; }
    }
}