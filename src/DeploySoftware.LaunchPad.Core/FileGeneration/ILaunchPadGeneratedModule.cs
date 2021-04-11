using System.Collections.Generic;

namespace DeploySoftware.LaunchPad.Core.FileGeneration
{
    public interface ILaunchPadGeneratedModule<TModuleSettings> : ILaunchPadGeneratedObject
        where TModuleSettings : LaunchPadGeneratedObjectBlueprintDefinitionSettings, new()
    {

        public TModuleSettings Settings { get; set; }

        public bool CheckValidity();
    }
}