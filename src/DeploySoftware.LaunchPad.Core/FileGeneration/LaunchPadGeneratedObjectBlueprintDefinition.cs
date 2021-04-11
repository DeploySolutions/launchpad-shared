using System;

namespace DeploySoftware.LaunchPad.Core.FileGeneration
{
    /// <summary>
    /// The definition of this generated object including its blueprint definition settings and generation instructions.
    /// </summary>    
    [Serializable]
    public partial class LaunchPadGeneratedObjectBlueprintDefinition<TBlueprintDefinitionSettings, TBlueprintDefinitionInstructions>
        where TBlueprintDefinitionSettings : LaunchPadGeneratedObjectBlueprintDefinitionSettings, new()
        where TBlueprintDefinitionInstructions : LaunchPadGeneratedObjectBlueprintDefinitionInstructions, new()
    {
        public virtual TBlueprintDefinitionSettings Settings { get; set; }

        public virtual TBlueprintDefinitionInstructions Instructions { get; set; }

        public LaunchPadGeneratedObjectBlueprintDefinition()
        {
            Settings = new TBlueprintDefinitionSettings();
            Instructions = new TBlueprintDefinitionInstructions();
        }

    }
}
