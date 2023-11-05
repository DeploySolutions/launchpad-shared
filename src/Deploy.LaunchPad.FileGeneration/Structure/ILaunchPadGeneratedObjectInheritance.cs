using System.Collections.Generic;

namespace Deploy.LaunchPad.FileGeneration.Structure
{
    public interface ILaunchPadGeneratedObjectInheritance
    {

        public string Type { get; set; }

        public string FullyQualifiedType { get; set; }

        public string AssemblyFullyQualifiedName { get; set; }

        string ParentFullyQualifiedType { get; set; }
        IDictionary<string, string> ChildrenFullyQualifiedTypes { get; }
        IDictionary<string, string> InheritsFrom { get; set; }
    }
}