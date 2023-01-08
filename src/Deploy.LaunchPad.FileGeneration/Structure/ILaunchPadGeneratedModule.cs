using Castle.Core.Logging;
using Deploy.LaunchPad.FileGeneration.Stages;

namespace Deploy.LaunchPad.FileGeneration.Structure
{
    public interface ILaunchPadGeneratedModule<TModuleSettings> : ILaunchPadGeneratedObject
        where TModuleSettings : ILaunchPadGeneratedObjectBlueprintDefinitionSettings, new()
    {
        public ILogger Logger { get; set; }

        public TModuleSettings Settings { get; set; }

        public bool CheckValidity();
    }
}