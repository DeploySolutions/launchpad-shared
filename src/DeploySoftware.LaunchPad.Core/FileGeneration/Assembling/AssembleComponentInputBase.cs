
using System;
using System.Collections.Generic;

namespace DeploySoftware.LaunchPad.Core.FileGeneration
{
    public abstract partial class AssembleComponentInputBase<TBlueprintDefinitionSettings, TBlueprintDefinitionInstructions> 
        : LaunchPadGenerationInputBase
        where TBlueprintDefinitionSettings : LaunchPadGeneratedObjectBlueprintDefinitionSettings, new()
        where TBlueprintDefinitionInstructions : LaunchPadGeneratedObjectBlueprintDefinitionInstructionsBase, new()
    {
        

    }
}
