// ***********************************************************************
// Assembly         : Deploy.LaunchPad.FileGeneration
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 01-08-2023
// ***********************************************************************
// <copyright file="WebClientBlueprintDefinitionInstructions.cs" company="Deploy Software Solutions, inc.">
//     2018-2023 Deploy Software Solutions, inc.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Castle.Core.Logging;
using Deploy.LaunchPad.FileGeneration.Stages;
using System;

namespace Deploy.LaunchPad.FileGeneration.Structure
{
    /// <summary>
    /// The set of instruction methods that will explain how the various stages in the factory process apply to this web client component.
    /// </summary>
    [Serializable]
    public partial class WebClientBlueprintDefinitionInstructions : LaunchPadGeneratedObjectBlueprintDefinitionInstructionsBase
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="WebClientBlueprintDefinitionInstructions"/> class.
        /// </summary>
        public WebClientBlueprintDefinitionInstructions() : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="WebClientBlueprintDefinitionInstructions"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        public WebClientBlueprintDefinitionInstructions(ILogger logger) : base(logger)
        {
        }

        /// <summary>
        /// Fors the assembling.
        /// </summary>
        /// <exception cref="System.NotImplementedException"></exception>
        public override void ForAssembling()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Fors the building.
        /// </summary>
        /// <exception cref="System.NotImplementedException"></exception>
        public override void ForBuilding()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Fors the checking validity.
        /// </summary>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public override bool ForCheckingValidity()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Fors the disposing.
        /// </summary>
        /// <exception cref="System.NotImplementedException"></exception>
        public override void ForDisposing()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Fors the initializing.
        /// </summary>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public override bool ForInitializing()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Fors the loading from blueprint definition.
        /// </summary>
        /// <exception cref="System.NotImplementedException"></exception>
        public override void ForLoadingFromBlueprintDefinition()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Fors the loading templates.
        /// </summary>
        /// <exception cref="System.NotImplementedException"></exception>
        public override void ForLoadingTemplates()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Fors the loading tokens.
        /// </summary>
        /// <exception cref="System.NotImplementedException"></exception>
        public override void ForLoadingTokens()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Fors the publishing.
        /// </summary>
        /// <exception cref="System.NotImplementedException"></exception>
        public override void ForPublishing()
        {
            throw new NotImplementedException();
        }
    }
}
