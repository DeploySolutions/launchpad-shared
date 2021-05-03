using Castle.Core.Logging;

namespace DeploySoftware.LaunchPad.Core.FileGeneration
{
    public interface ILaunchPadGeneratedComponent<TBlueprintDefinitionSettings, TBlueprintDefinitionInstructions> : ILaunchPadGeneratedObject
        where TBlueprintDefinitionSettings : LaunchPadGeneratedObjectBlueprintDefinitionSettings, new()
        where TBlueprintDefinitionInstructions : LaunchPadGeneratedObjectBlueprintDefinitionInstructions, new()
    {
        public LaunchPadGeneratedObjectBlueprintDefinition<TBlueprintDefinitionSettings, TBlueprintDefinitionInstructions> BlueprintDefinition { get; set; }

        public ILogger Logger { get; set; }

        /// <summary>
        /// Returns a bool indicating if the component is currently in a valid or invalid state.
        /// </summary>
        /// <returns>True if the component is in a valid state, or false if it is contains missing or invalid elements.</returns>
        public bool CheckValidity();

        /// <summary>
        /// Assemble the component from the provided input.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public TAssembleOutput AssembleComponent<TAssembleInput, TAssembleOutput, TGeneratedObject>(TAssembleInput input)
            where TAssembleInput : AssembleInputBase<TGeneratedObject>, new()
            where TAssembleOutput : AssembleComponentOutputBase, new()
            where TGeneratedObject : LaunchPadGeneratedSolution, new();
    }
}