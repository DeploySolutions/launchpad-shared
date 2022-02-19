using DeploySoftware.LaunchPad.FileGeneration.Stages;
using System;
using System.Collections.Generic;

namespace DeploySoftware.LaunchPad.FileGeneration.Structure
{
    public interface IVisualStudioBlueprintDefinitionInstructions : ILaunchPadGeneratedObjectBlueprintDefinitionInstructions
    {
        IDictionary<string, LaunchPadGeneratedMethod> CustomMethodInsertsOrUpdates { get; set; }
        IDictionary<string, LaunchPadGeneratedProperty> CustomPropertyInsertsOrUpdates { get; set; }
        HashSet<Tuple<string, string>> DependencyInstructions { get; set; }
    }
}