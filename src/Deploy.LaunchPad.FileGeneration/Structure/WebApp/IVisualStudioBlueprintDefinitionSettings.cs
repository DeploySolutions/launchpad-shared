using Deploy.LaunchPad.FileGeneration.Stages.Defining;
using Deploy.LaunchPad.FileGeneration.Structure.WebApp;
using System.Collections.Generic;

namespace Deploy.LaunchPad.FileGeneration.Structure
{
    public interface IVisualStudioBlueprintDefinitionSettings
    {
        IDictionary<string, LaunchPadGeneratedObjectBase> AppServiceDefinitions { get; set; }
        ILaunchPadGeneratedAppSettings AppSettings { get; set; }
        string BaseAppServiceClass { get; set; }
        string BaseAppServiceClassAnnotations { get; set; }
        string BaseNamespace { get; set; }
        ICustomHttpHeaders CustomHeaders { get; set; }
        IDictionary<string, LaunchPadGeneratedObjectBase> DomainEntityDefinitions { get; set; }
        string ProjectName { get; set; }
        IDictionary<string, LaunchPadGeneratedDtoPropertySet> PropertySets { get; set; }
        string SubFolderName { get; set; }
        string VisualStudioSolutionName { get; set; }
    }
}