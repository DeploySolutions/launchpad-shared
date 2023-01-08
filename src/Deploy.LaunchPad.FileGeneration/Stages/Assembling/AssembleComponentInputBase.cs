namespace Deploy.LaunchPad.FileGeneration.Stages
{
    public abstract partial class AssembleComponentInputBase<TBlueprintDefinitionSettings, TBlueprintDefinitionInstructions>
        : LaunchPadGenerationInputBase
        where TBlueprintDefinitionSettings : LaunchPadGeneratedObjectBlueprintDefinitionSettings, new()
        where TBlueprintDefinitionInstructions : LaunchPadGeneratedObjectBlueprintDefinitionInstructionsBase, new()
    {


    }
}
