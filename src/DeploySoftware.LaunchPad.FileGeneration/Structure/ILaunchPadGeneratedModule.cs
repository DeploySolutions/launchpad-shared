using Castle.Core.Logging;
using DeploySoftware.LaunchPad.FileGeneration.Stages;
using System.Collections.Generic;

namespace DeploySoftware.LaunchPad.FileGeneration.Structure
{
    public interface ILaunchPadGeneratedModule<TModuleSettings> : ILaunchPadGeneratedObject
        where TModuleSettings : ILaunchPadGeneratedObjectBlueprintDefinitionSettings, new()
    {
        public ILogger Logger { get; set; }

        public TModuleSettings Settings { get; set; }

        public bool CheckValidity();
    }
}