using Castle.Core.Logging;
using Deploy.LaunchPad.FileGeneration.Stages;
using System.Collections.Generic;

namespace Deploy.LaunchPad.FileGeneration.Structure
{
    public interface ILaunchPadGeneratedModule<TModuleSettings> : ILaunchPadGeneratedObject
        where TModuleSettings : ILaunchPadGeneratedObjectBlueprintDefinitionSettings, new()
    {
        public ILogger Logger { get; set; }

        public TModuleSettings Settings { get; set; }
        public IDictionary<string, ILicensedThirdPartySoftwareItem> LicensedThirdPartyItems { get; set; }

        public bool CheckValidity();
    }
}