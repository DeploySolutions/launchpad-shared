// ***********************************************************************
// Assembly         : Deploy.LaunchPad.FileGeneration
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 01-08-2023
// ***********************************************************************
// <copyright file="ILaunchPadGeneratedObjectBlueprintDefinition.cs" company="Deploy Software Solutions, inc.">
//     2018-2023 Deploy Software Solutions, inc.
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace Deploy.LaunchPad.FileGeneration.Stages
{
    /// <summary>
    /// Interface ILaunchPadGeneratedObjectBlueprintDefinition
    /// </summary>
    /// <typeparam name="TBlueprintDefinitionSettings">The type of the t blueprint definition settings.</typeparam>
    /// <typeparam name="TBlueprintDefinitionInstructions">The type of the t blueprint definition instructions.</typeparam>
    public partial interface ILaunchPadGeneratedObjectBlueprintDefinition<TBlueprintDefinitionSettings, TBlueprintDefinitionInstructions>
        where TBlueprintDefinitionSettings : LaunchPadGeneratedObjectBlueprintDefinitionSettings, new()
        where TBlueprintDefinitionInstructions : LaunchPadGeneratedObjectBlueprintDefinitionInstructionsBase, new()
    {
        /// <summary>
        /// Gets or sets the settings.
        /// </summary>
        /// <value>The settings.</value>
        public TBlueprintDefinitionSettings Settings { get; set; }

        /// <summary>
        /// Gets or sets the instructions.
        /// </summary>
        /// <value>The instructions.</value>
        public TBlueprintDefinitionInstructions Instructions { get; set; }

    }
}
