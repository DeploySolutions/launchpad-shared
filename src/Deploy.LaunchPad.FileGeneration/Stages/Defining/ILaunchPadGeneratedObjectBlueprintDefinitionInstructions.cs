// ***********************************************************************
// Assembly         : Deploy.LaunchPad.FileGeneration
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 01-08-2023
// ***********************************************************************
// <copyright file="ILaunchPadGeneratedObjectBlueprintDefinitionInstructions.cs" company="Deploy Software Solutions, inc.">
//     2018-2023 Deploy Software Solutions, inc.
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace Deploy.LaunchPad.FileGeneration.Stages
{
    /// <summary>
    /// Interface ILaunchPadGeneratedObjectBlueprintDefinitionInstructions
    /// </summary>
    public partial interface ILaunchPadGeneratedObjectBlueprintDefinitionInstructions
    {

        /// <summary>
        /// Fors the checking validity.
        /// </summary>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public bool ForCheckingValidity();
        /// <summary>
        /// Fors the initializing.
        /// </summary>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public bool ForInitializing();
        /// <summary>
        /// Fors the loading from blueprint definition.
        /// </summary>
        public void ForLoadingFromBlueprintDefinition();
        /// <summary>
        /// Fors the assembling.
        /// </summary>
        public void ForAssembling();
        /// <summary>
        /// Fors the loading templates.
        /// </summary>
        public void ForLoadingTemplates();

        /// <summary>
        /// Fors the loading tokens.
        /// </summary>
        public void ForLoadingTokens();

        /// <summary>
        /// Fors the building.
        /// </summary>
        public void ForBuilding();

        /// <summary>
        /// Fors the publishing.
        /// </summary>
        public void ForPublishing();

        /// <summary>
        /// Fors the disposing.
        /// </summary>
        public void ForDisposing();

    }
}
