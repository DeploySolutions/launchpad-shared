using Castle.Core.Logging;
using System;
using System.Collections.Generic;

namespace DeploySoftware.LaunchPad.Core.FileGeneration
{
    /// <summary>
    /// The set of instruction methods that will explain how the various stages in the factory process apply to this visual studio component.    
    /// </summary>    
    [Serializable]
    public partial class VisualStudioBlueprintDefinitionInstructions : LaunchPadGeneratedObjectBlueprintDefinitionInstructionsBase, IVisualStudioBlueprintDefinitionInstructions
    { 

        public IDictionary<string, LaunchPadGeneratedMethod> CustomMethods { get; set; }

        public IDictionary<string, LaunchPadGeneratedProperty> CustomProperties { get; set; }

        /// <summary>
        /// Used for identifying dependencies between items during a build.
        /// </summary>
        public HashSet<Tuple<string, string>> DependencyInstructions { get; set; } = new HashSet<Tuple<string, string>>();

        public VisualStudioBlueprintDefinitionInstructions() : base()
        {
            var comparer = StringComparer.OrdinalIgnoreCase;
            CustomMethods = new Dictionary<string, LaunchPadGeneratedMethod>(comparer);
            CustomProperties = new Dictionary<string, LaunchPadGeneratedProperty>(comparer);
        }

        public VisualStudioBlueprintDefinitionInstructions(ILogger logger) : base(logger)
        {
            var comparer = StringComparer.OrdinalIgnoreCase;
            CustomMethods = new Dictionary<string, LaunchPadGeneratedMethod>(comparer);
            CustomProperties = new Dictionary<string, LaunchPadGeneratedProperty>(comparer);
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
