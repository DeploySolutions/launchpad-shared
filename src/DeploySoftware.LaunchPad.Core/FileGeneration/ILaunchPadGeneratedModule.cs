using Castle.Core.Logging;
using System.Collections.Generic;

namespace DeploySoftware.LaunchPad.Core.FileGeneration
{
    public interface ILaunchPadGeneratedModule<TModuleSettings> : ILaunchPadGeneratedObject
        where TModuleSettings : LaunchPadGeneratedObjectBlueprintDefinitionSettings, new()
    {
        public ILogger Logger { get; set; }

        public TModuleSettings Settings { get; set; }

        public bool CheckValidity();
    }
}