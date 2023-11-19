// ***********************************************************************
// Assembly         : Deploy.LaunchPad.FileGeneration
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 01-08-2023
// ***********************************************************************
// <copyright file="LaunchPadGeneratedObjectBlueprintDefinition.cs" company="Deploy Software Solutions, inc.">
//     2018-2023 Deploy Software Solutions, inc.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;

namespace Deploy.LaunchPad.FileGeneration.Stages
{
    /// <summary>
    /// The definition of this generated object including its blueprint definition settings and generation instructions.
    /// </summary>
    /// <typeparam name="TBlueprintDefinitionSettings">The type of the t blueprint definition settings.</typeparam>
    /// <typeparam name="TBlueprintDefinitionInstructions">The type of the t blueprint definition instructions.</typeparam>
    [Serializable]
    public partial class LaunchPadGeneratedObjectBlueprintDefinition<TBlueprintDefinitionSettings, TBlueprintDefinitionInstructions>
        where TBlueprintDefinitionSettings : LaunchPadGeneratedObjectBlueprintDefinitionSettings, new()
        where TBlueprintDefinitionInstructions : LaunchPadGeneratedObjectBlueprintDefinitionInstructionsBase, new()
    {
        /// <summary>
        /// Gets or sets the settings.
        /// </summary>
        /// <value>The settings.</value>
        public virtual TBlueprintDefinitionSettings Settings { get; set; }

        /// <summary>
        /// Gets or sets the instructions.
        /// </summary>
        /// <value>The instructions.</value>
        public virtual TBlueprintDefinitionInstructions Instructions { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="LaunchPadGeneratedObjectBlueprintDefinition{TBlueprintDefinitionSettings, TBlueprintDefinitionInstructions}"/> class.
        /// </summary>
        public LaunchPadGeneratedObjectBlueprintDefinition()
        {
            Settings = new TBlueprintDefinitionSettings();
            Instructions = new TBlueprintDefinitionInstructions();
        }

    }
}
