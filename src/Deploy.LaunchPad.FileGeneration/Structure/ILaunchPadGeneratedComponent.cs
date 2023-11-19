// ***********************************************************************
// Assembly         : Deploy.LaunchPad.FileGeneration
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 03-26-2023
// ***********************************************************************
// <copyright file="ILaunchPadGeneratedComponent.cs" company="Deploy Software Solutions, inc.">
//     2018-2023 Deploy Software Solutions, inc.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Castle.Core.Logging;
using Deploy.LaunchPad.FileGeneration.Stages;
using System.Collections.Generic;

namespace Deploy.LaunchPad.FileGeneration.Structure
{
    /// <summary>
    /// Interface ILaunchPadGeneratedComponent
    /// Extends the <see cref="Deploy.LaunchPad.FileGeneration.Structure.ILaunchPadGeneratedObject" />
    /// </summary>
    /// <typeparam name="TBlueprintDefinitionSettings">The type of the t blueprint definition settings.</typeparam>
    /// <typeparam name="TBlueprintDefinitionInstructions">The type of the t blueprint definition instructions.</typeparam>
    /// <seealso cref="Deploy.LaunchPad.FileGeneration.Structure.ILaunchPadGeneratedObject" />
    public partial interface ILaunchPadGeneratedComponent<TBlueprintDefinitionSettings, TBlueprintDefinitionInstructions> : ILaunchPadGeneratedObject
        where TBlueprintDefinitionSettings : LaunchPadGeneratedObjectBlueprintDefinitionSettings, new()
        where TBlueprintDefinitionInstructions : LaunchPadGeneratedObjectBlueprintDefinitionInstructionsBase, new()
    {
        /// <summary>
        /// Gets or sets the blueprint definition.
        /// </summary>
        /// <value>The blueprint definition.</value>
        public LaunchPadGeneratedObjectBlueprintDefinition<TBlueprintDefinitionSettings, TBlueprintDefinitionInstructions> BlueprintDefinition { get; set; }

        /// <summary>
        /// Gets or sets the logger.
        /// </summary>
        /// <value>The logger.</value>
        public ILogger Logger { get; set; }

        /// <summary>
        /// Gets or sets the component status.
        /// </summary>
        /// <value>The component status.</value>
        public ComponentStatusEnum ComponentStatus { get; set; }

        /// <summary>
        /// Gets or sets the licensed third party items.
        /// </summary>
        /// <value>The licensed third party items.</value>
        public IDictionary<string, ILicensedThirdPartySoftwareItem> LicensedThirdPartyItems { get; set; }

        /// <summary>
        /// Returns a bool indicating if the component is currently in a valid or invalid state.
        /// </summary>
        /// <returns>True if the component is in a valid state, or false if it is contains missing or invalid elements.</returns>
        public bool CheckValidity();

        /// <summary>
        /// Assemble the component from the provided input.
        /// </summary>
        /// <typeparam name="TAssembleInput">The type of the t assemble input.</typeparam>
        /// <typeparam name="TAssembleOutput">The type of the t assemble output.</typeparam>
        /// <typeparam name="TGeneratedObject">The type of the t generated object.</typeparam>
        /// <param name="input">The input.</param>
        /// <returns>TAssembleOutput.</returns>
        public TAssembleOutput AssembleComponent<TAssembleInput, TAssembleOutput, TGeneratedObject>(TAssembleInput input)
            where TAssembleInput : AssembleComponentInputBase<TBlueprintDefinitionSettings, TBlueprintDefinitionInstructions>, new()
            where TAssembleOutput : AssembleComponentOutputBase, new()
            where TGeneratedObject : LaunchPadGeneratedSolution, new();
    }
}