namespace Deploy.LaunchPad.FileGeneration.Stages
{
    public interface ILaunchPadGeneratedObjectBlueprintDefinition<TBlueprintDefinitionSettings, TBlueprintDefinitionInstructions>
        where TBlueprintDefinitionSettings : LaunchPadGeneratedObjectBlueprintDefinitionSettings, new()
        where TBlueprintDefinitionInstructions : LaunchPadGeneratedObjectBlueprintDefinitionInstructionsBase, new()
    {
        public TBlueprintDefinitionSettings Settings { get; set; }

        public TBlueprintDefinitionInstructions Instructions { get; set; }

    }
}
