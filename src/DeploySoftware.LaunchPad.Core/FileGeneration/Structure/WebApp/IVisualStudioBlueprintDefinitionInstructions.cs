using System;
using System.Collections.Generic;

namespace DeploySoftware.LaunchPad.Core.FileGeneration
{
    public interface IVisualStudioBlueprintDefinitionInstructions : ILaunchPadGeneratedObjectBlueprintDefinitionInstructions
    {
        IDictionary<string, LaunchPadGeneratedMethod> CustomMethods { get; set; }
        IDictionary<string, LaunchPadGeneratedProperty> CustomProperties { get; set; }
        HashSet<Tuple<string, string>> DependencyInstructions { get; set; }
    }
}