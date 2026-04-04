// ***********************************************************************
// Assembly         : Deploy.LaunchPad.FileGeneration
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 01-08-2023
// ***********************************************************************
// <copyright file="AssembleComponentInputBase.cs" company="Deploy Software Solutions, inc.">
//     2018-2023 Deploy Software Solutions, inc.
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace Deploy.LaunchPad.FileGeneration.Stages
{
    /// <summary>
    /// Class AssembleComponentInputBase.
    /// Implements the <see cref="Deploy.LaunchPad.FileGeneration.Stages.LaunchPadGenerationInputBase" />
    /// </summary>
    /// <typeparam name="TBlueprintDefinitionSettings">The type of the t blueprint definition settings.</typeparam>
    /// <typeparam name="TBlueprintDefinitionInstructions">The type of the t blueprint definition instructions.</typeparam>
    /// <seealso cref="Deploy.LaunchPad.FileGeneration.Stages.LaunchPadGenerationInputBase" />
    public abstract partial class AssembleComponentInputBase<TBlueprintDefinitionSettings, TBlueprintDefinitionInstructions>
        : LaunchPadGenerationInputBase
        where TBlueprintDefinitionSettings : LaunchPadGeneratedObjectBlueprintDefinitionSettings, new()
        where TBlueprintDefinitionInstructions : LaunchPadGeneratedObjectBlueprintDefinitionInstructionsBase, new()
    {


    }
}
