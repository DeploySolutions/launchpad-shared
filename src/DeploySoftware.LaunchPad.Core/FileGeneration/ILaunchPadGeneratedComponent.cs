namespace DeploySoftware.LaunchPad.Core.FileGeneration
{
    public interface ILaunchPadGeneratedComponent<TBlueprintDefinitionSettings, TBlueprintDefinitionInstructions> : ILaunchPadGeneratedObject
        where TBlueprintDefinitionSettings : LaunchPadGeneratedObjectBlueprintDefinitionSettings, new()
        where TBlueprintDefinitionInstructions : LaunchPadGeneratedObjectBlueprintDefinitionInstructions, new()
    {
        public LaunchPadGeneratedObjectBlueprintDefinition<TBlueprintDefinitionSettings, TBlueprintDefinitionInstructions> BlueprintDefinition { get; set; }

        /// <summary>
        /// Returns a bool indicating if the component is currently in a valid or invalid state.
        /// </summary>
        /// <returns>True if the component is in a valid state, or false if it is contains missing or invalid elements.</returns>
        public bool CheckValidity();
    }
}