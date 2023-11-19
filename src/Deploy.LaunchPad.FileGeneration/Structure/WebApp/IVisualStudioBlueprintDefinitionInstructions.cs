// ***********************************************************************
// Assembly         : Deploy.LaunchPad.FileGeneration
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 02-05-2023
// ***********************************************************************
// <copyright file="IVisualStudioBlueprintDefinitionInstructions.cs" company="Deploy Software Solutions, inc.">
//     2018-2023 Deploy Software Solutions, inc.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Deploy.LaunchPad.FileGeneration.Stages;
using Deploy.LaunchPad.FileGeneration.Stages.Building;
using System;
using System.Collections.Generic;

namespace Deploy.LaunchPad.FileGeneration.Structure
{
    /// <summary>
    /// Interface IVisualStudioBlueprintDefinitionInstructions
    /// Extends the <see cref="ILaunchPadGeneratedObjectBlueprintDefinitionInstructions" />
    /// </summary>
    /// <seealso cref="ILaunchPadGeneratedObjectBlueprintDefinitionInstructions" />
    public partial interface IVisualStudioBlueprintDefinitionInstructions : ILaunchPadGeneratedObjectBlueprintDefinitionInstructions
    {
        /// <summary>
        /// Gets or sets the class file creations.
        /// </summary>
        /// <value>The class file creations.</value>
        IDictionary<string, LaunchPadGeneratedCustomClassFile> ClassFileCreations { get; set; }

        /// <summary>
        /// Gets or sets the class file modifications.
        /// </summary>
        /// <value>The class file modifications.</value>
        IDictionary<string, LaunchPadGeneratedCustomClassFile> ClassFileModifications { get; set; }

        /// <summary>
        /// Gets or sets the custom method inserts or updates.
        /// </summary>
        /// <value>The custom method inserts or updates.</value>
        IDictionary<string, LaunchPadGeneratedMethod> CustomMethodInsertsOrUpdates { get; set; }
        /// <summary>
        /// Gets or sets the custom property inserts or updates.
        /// </summary>
        /// <value>The custom property inserts or updates.</value>
        IDictionary<string, LaunchPadGeneratedProperty> CustomPropertyInsertsOrUpdates { get; set; }

        /// <summary>
        /// Gets or sets the add package references to vs project.
        /// </summary>
        /// <value>The add package references to vs project.</value>
        public IDictionary<string, AddPackageReferenceToVsProjectInstruction> AddPackageReferencesToVsProject { get; set; }

        /// <summary>
        /// Gets or sets the dependency instructions.
        /// </summary>
        /// <value>The dependency instructions.</value>
        HashSet<Tuple<string, string>> DependencyInstructions { get; set; }
        /// <summary>
        /// Gets or sets the post build text replacements.
        /// </summary>
        /// <value>The post build text replacements.</value>
        public IList<PostBuildTextReplacement> PostBuildTextReplacements { get; set; }
    }
}