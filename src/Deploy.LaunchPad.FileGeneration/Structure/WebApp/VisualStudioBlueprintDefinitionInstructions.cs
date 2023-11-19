// ***********************************************************************
// Assembly         : Deploy.LaunchPad.FileGeneration
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 06-20-2023
// ***********************************************************************
// <copyright file="VisualStudioBlueprintDefinitionInstructions.cs" company="Deploy Software Solutions, inc.">
//     2018-2023 Deploy Software Solutions, inc.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Castle.Core.Logging;
using Deploy.LaunchPad.FileGeneration.Stages;
using Deploy.LaunchPad.FileGeneration.Stages.Building;
using System;
using System.Collections.Generic;

namespace Deploy.LaunchPad.FileGeneration.Structure
{
    /// <summary>
    /// The set of instruction methods that will explain how the various stages in the factory process apply to this visual studio component.
    /// </summary>
    [Serializable]
    public partial class VisualStudioBlueprintDefinitionInstructions : LaunchPadGeneratedObjectBlueprintDefinitionInstructionsBase, IVisualStudioBlueprintDefinitionInstructions
    {

        /// <summary>
        /// Gets or sets the enum file creations.
        /// </summary>
        /// <value>The enum file creations.</value>
        public virtual IDictionary<string, LaunchPadGeneratedEnum> EnumFileCreations { get; set; }
        /// <summary>
        /// Gets or sets the class file creations.
        /// </summary>
        /// <value>The class file creations.</value>
        public virtual IDictionary<string, LaunchPadGeneratedCustomClassFile> ClassFileCreations { get; set; }
        /// <summary>
        /// Gets or sets the class file modifications.
        /// </summary>
        /// <value>The class file modifications.</value>
        public virtual IDictionary<string, LaunchPadGeneratedCustomClassFile> ClassFileModifications { get; set; }

        /// <summary>
        /// Gets or sets the custom method inserts or updates.
        /// </summary>
        /// <value>The custom method inserts or updates.</value>
        public virtual IDictionary<string, LaunchPadGeneratedMethod> CustomMethodInsertsOrUpdates { get; set; }

        /// <summary>
        /// Gets or sets the custom property inserts or updates.
        /// </summary>
        /// <value>The custom property inserts or updates.</value>
        public virtual IDictionary<string, LaunchPadGeneratedProperty> CustomPropertyInsertsOrUpdates { get; set; }

        /// <summary>
        /// Gets or sets the add package references to vs project.
        /// </summary>
        /// <value>The add package references to vs project.</value>
        public virtual IDictionary<string, AddPackageReferenceToVsProjectInstruction> AddPackageReferencesToVsProject { get; set; }

        /// <summary>
        /// Gets or sets the post build text replacements.
        /// </summary>
        /// <value>The post build text replacements.</value>
        public virtual IList<PostBuildTextReplacement> PostBuildTextReplacements { get; set; }

        /// <summary>
        /// Used for identifying dependencies between items during a build.
        /// </summary>
        /// <value>The dependency instructions.</value>
        public virtual HashSet<Tuple<string, string>> DependencyInstructions { get; set; } = new HashSet<Tuple<string, string>>();

        /// <summary>
        /// Gets or sets the relative folder path from output root.
        /// </summary>
        /// <value>The relative folder path from output root.</value>
        public virtual string RelativeFolderPathFromOutputRoot { get; set; } = string.Empty;

        /// <summary>
        /// Initializes a new instance of the <see cref="VisualStudioBlueprintDefinitionInstructions"/> class.
        /// </summary>
        public VisualStudioBlueprintDefinitionInstructions() : base()
        {
            var comparer = StringComparer.OrdinalIgnoreCase;
            ClassFileCreations = new Dictionary<string, LaunchPadGeneratedCustomClassFile>(comparer);
            ClassFileModifications = new Dictionary<string, LaunchPadGeneratedCustomClassFile>(comparer);
            EnumFileCreations = new Dictionary<string, LaunchPadGeneratedEnum>(comparer);
            CustomMethodInsertsOrUpdates = new Dictionary<string, LaunchPadGeneratedMethod>(comparer);
            CustomPropertyInsertsOrUpdates = new Dictionary<string, LaunchPadGeneratedProperty>(comparer);
            PostBuildTextReplacements = new List<PostBuildTextReplacement>(); 
            AddPackageReferencesToVsProject = new Dictionary<string, AddPackageReferenceToVsProjectInstruction>(comparer);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="VisualStudioBlueprintDefinitionInstructions"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        public VisualStudioBlueprintDefinitionInstructions(ILogger logger) : base(logger)
        {
            var comparer = StringComparer.OrdinalIgnoreCase;
            ClassFileCreations = new Dictionary<string, LaunchPadGeneratedCustomClassFile>(comparer);
            ClassFileModifications = new Dictionary<string, LaunchPadGeneratedCustomClassFile>(comparer);
            EnumFileCreations = new Dictionary<string, LaunchPadGeneratedEnum>(comparer);
            CustomMethodInsertsOrUpdates = new Dictionary<string, LaunchPadGeneratedMethod>(comparer);
            CustomPropertyInsertsOrUpdates = new Dictionary<string, LaunchPadGeneratedProperty>(comparer);
            PostBuildTextReplacements = new List<PostBuildTextReplacement>();
            AddPackageReferencesToVsProject = new Dictionary<string, AddPackageReferenceToVsProjectInstruction>(comparer);
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
