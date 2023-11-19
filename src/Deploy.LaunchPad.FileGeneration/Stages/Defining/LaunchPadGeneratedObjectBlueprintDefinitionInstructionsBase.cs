// ***********************************************************************
// Assembly         : Deploy.LaunchPad.FileGeneration
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 01-08-2023
// ***********************************************************************
// <copyright file="LaunchPadGeneratedObjectBlueprintDefinitionInstructionsBase.cs" company="Deploy Software Solutions, inc.">
//     2018-2023 Deploy Software Solutions, inc.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Castle.Core.Logging;
using Newtonsoft.Json;
using System;

namespace Deploy.LaunchPad.FileGeneration.Stages
{
    /// <summary>
    /// The set of instruction methods that will explain how the various stages in the factory process apply to this object.
    /// </summary>
    [Serializable]
    public abstract partial class LaunchPadGeneratedObjectBlueprintDefinitionInstructionsBase : ILaunchPadGeneratedObjectBlueprintDefinitionInstructions
    {
        /// <summary>
        /// Gets or sets the logger.
        /// </summary>
        /// <value>The logger.</value>
        [JsonIgnore]
        public ILogger Logger { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="LaunchPadGeneratedObjectBlueprintDefinitionInstructionsBase"/> class.
        /// </summary>
        protected LaunchPadGeneratedObjectBlueprintDefinitionInstructionsBase()
        {

            Logger = NullLogger.Instance;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LaunchPadGeneratedObjectBlueprintDefinitionInstructionsBase"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        protected LaunchPadGeneratedObjectBlueprintDefinitionInstructionsBase(ILogger logger)
        {

            Logger = logger;
        }

        /// <summary>
        /// Fors the checking validity.
        /// </summary>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public abstract bool ForCheckingValidity();
        /// <summary>
        /// Fors the initializing.
        /// </summary>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public abstract bool ForInitializing();
        /// <summary>
        /// Fors the loading from blueprint definition.
        /// </summary>
        public abstract void ForLoadingFromBlueprintDefinition();
        /// <summary>
        /// Fors the assembling.
        /// </summary>
        public abstract void ForAssembling();
        /// <summary>
        /// Fors the loading templates.
        /// </summary>
        public abstract void ForLoadingTemplates();

        /// <summary>
        /// Fors the loading tokens.
        /// </summary>
        public abstract void ForLoadingTokens();

        /// <summary>
        /// Fors the building.
        /// </summary>
        public abstract void ForBuilding();

        /// <summary>
        /// Fors the publishing.
        /// </summary>
        public abstract void ForPublishing();

        /// <summary>
        /// Fors the disposing.
        /// </summary>
        public abstract void ForDisposing();
    }
}
