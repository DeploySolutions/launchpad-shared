
using System;
using System.Collections.Generic;

namespace DeploySoftware.LaunchPad.Core.FileGeneration
{
    public abstract partial class AssembleComponentInputBase<TBlueprintDefinitionSettings, TBlueprintDefinitionInstructions> 
        : AssembleInputBase
        where TBlueprintDefinitionSettings : LaunchPadGeneratedObjectBlueprintDefinitionSettings, new()
        where TBlueprintDefinitionInstructions : LaunchPadGeneratedObjectBlueprintDefinitionInstructions, new()
    {
        

    }
}
