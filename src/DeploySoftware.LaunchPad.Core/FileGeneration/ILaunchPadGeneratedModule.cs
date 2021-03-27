using System.Collections.Generic;

namespace DeploySoftware.LaunchPad.Core.FileGeneration
{
    public interface ILaunchPadGeneratedModule<TModuleConfig, TComponent, TComponentConfig>: ILaunchPadGeneratedObject
        where TModuleConfig: LaunchPadGeneratedConfiguration, new()
        where TComponent : LaunchPadGeneratedComponent<TComponentConfig>, new()
        where TComponentConfig : LaunchPadGeneratedConfiguration, new()
    {
        public TModuleConfig Config { get; set; }

        public IDictionary<string,TComponent> Components { get; set; }
    }
}