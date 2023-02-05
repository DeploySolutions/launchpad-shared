using Deploy.LaunchPad.FileGeneration.Stages;
using Deploy.LaunchPad.FileGeneration.Stages.Building;
using System;
using System.Collections.Generic;

namespace Deploy.LaunchPad.FileGeneration.Structure
{
    public interface IVisualStudioBlueprintDefinitionInstructions : ILaunchPadGeneratedObjectBlueprintDefinitionInstructions
    {
        IDictionary<string, LaunchPadGeneratedCustomClassFile> ClassFileCreations { get; set; }

        IDictionary<string, LaunchPadGeneratedCustomClassFile> ClassFileModifications { get; set; }

        IDictionary<string, LaunchPadGeneratedMethod> CustomMethodInsertsOrUpdates { get; set; }
        IDictionary<string, LaunchPadGeneratedProperty> CustomPropertyInsertsOrUpdates { get; set; }

        public IDictionary<string, AddPackageReferenceToVsProjectInstruction> AddPackageReferencesToVsProject { get; set; }

        HashSet<Tuple<string, string>> DependencyInstructions { get; set; }
        public  IList<PostBuildTextReplacement> PostBuildTextReplacements { get; set; }
    }
}