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

        public virtual IDictionary<string, LaunchPadGeneratedCustomClassFile> CustomClasses { get; set; }

        public virtual IDictionary<string, LaunchPadGeneratedMethod> CustomMethodInsertsOrUpdates { get; set; }

        public virtual IDictionary<string, LaunchPadGeneratedProperty> CustomPropertyInsertsOrUpdates { get; set; }

        public virtual IDictionary<string, AddPackageReferenceToVsProjectInstruction> AddPackageReferencesToVsProject { get; set; }

        public virtual IList<PostBuildTextReplacement> PostBuildTextReplacements { get; set; }

        /// <summary>
        /// Used for identifying dependencies between items during a build.
        /// </summary>
        public virtual HashSet<Tuple<string, string>> DependencyInstructions { get; set; } = new HashSet<Tuple<string, string>>();

        public VisualStudioBlueprintDefinitionInstructions() : base()
        {
            var comparer = StringComparer.OrdinalIgnoreCase;
            CustomClasses = new Dictionary<string, LaunchPadGeneratedCustomClassFile>(comparer);
            CustomMethodInsertsOrUpdates = new Dictionary<string, LaunchPadGeneratedMethod>(comparer);
            CustomPropertyInsertsOrUpdates = new Dictionary<string, LaunchPadGeneratedProperty>(comparer);
            PostBuildTextReplacements = new List<PostBuildTextReplacement>(); 
            AddPackageReferencesToVsProject = new Dictionary<string, AddPackageReferenceToVsProjectInstruction>(comparer);
        }

        public VisualStudioBlueprintDefinitionInstructions(ILogger logger) : base(logger)
        {
            var comparer = StringComparer.OrdinalIgnoreCase;
            CustomClasses = new Dictionary<string, LaunchPadGeneratedCustomClassFile>(comparer);
            CustomMethodInsertsOrUpdates = new Dictionary<string, LaunchPadGeneratedMethod>(comparer);
            CustomPropertyInsertsOrUpdates = new Dictionary<string, LaunchPadGeneratedProperty>(comparer);
            PostBuildTextReplacements = new List<PostBuildTextReplacement>();
            AddPackageReferencesToVsProject = new Dictionary<string, AddPackageReferenceToVsProjectInstruction>(comparer);
        }

        public override void ForAssembling()
        {
            throw new NotImplementedException();
        }

        public override void ForBuilding()
        {
            throw new NotImplementedException();
        }

        public override bool ForCheckingValidity()
        {
            throw new NotImplementedException();
        }

        public override void ForDisposing()
        {
            throw new NotImplementedException();
        }

        public override bool ForInitializing()
        {
            throw new NotImplementedException();
        }

        public override void ForLoadingFromBlueprintDefinition()
        {
            throw new NotImplementedException();
        }

        public override void ForLoadingTemplates()
        {
            throw new NotImplementedException();
        }

        public override void ForLoadingTokens()
        {
            throw new NotImplementedException();
        }

        public override void ForPublishing()
        {
            throw new NotImplementedException();
        }
    }
}
