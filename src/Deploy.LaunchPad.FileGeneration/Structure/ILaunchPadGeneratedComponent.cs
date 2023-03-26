using Castle.Core.Logging;
using Deploy.LaunchPad.FileGeneration.Stages;
using System.Collections.Generic;

namespace Deploy.LaunchPad.FileGeneration.Structure
{
    public interface ILaunchPadGeneratedComponent<TBlueprintDefinitionSettings, TBlueprintDefinitionInstructions> : ILaunchPadGeneratedObject
        where TBlueprintDefinitionSettings : LaunchPadGeneratedObjectBlueprintDefinitionSettings, new()
        where TBlueprintDefinitionInstructions : LaunchPadGeneratedObjectBlueprintDefinitionInstructionsBase, new()
    {
        public LaunchPadGeneratedObjectBlueprintDefinition<TBlueprintDefinitionSettings, TBlueprintDefinitionInstructions> BlueprintDefinition { get; set; }

        public ILogger Logger { get; set; }

        public ComponentStatusEnum ComponentStatus { get; set; }

        public IDictionary<string, ILicensedThirdPartySoftwareItem> LicensedThirdPartyItems { get; set; }

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
            where TAssembleInput : AssembleComponentInputBase<TBlueprintDefinitionSettings, TBlueprintDefinitionInstructions>, new()
            where TAssembleOutput : AssembleComponentOutputBase, new()
            where TGeneratedObject : LaunchPadGeneratedSolution, new();
    }
}