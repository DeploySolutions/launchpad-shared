using DeploySoftware.LaunchPad.FileGeneration.Stages;
using DeploySoftware.LaunchPad.FileGeneration.Stages.Defining;
using System;
using System.Collections.Generic;
using System.Text;

namespace DeploySoftware.LaunchPad.FileGeneration.Structure
{
    [Serializable]
    public partial class VisualStudioBlueprintDefinitionSettings : 
        LaunchPadGeneratedObjectBlueprintDefinitionSettings
    {
        /// The name of the Visual Studio solution (.sln) in which this generated module will belong.
        /// Note: this solution configuration is deliberately placed at the Visual Studio Module level, 
        /// and is not the same as a LaunchPadGeneratedSolution object.
        /// </summary>
        public virtual string VisualStudioSolutionName { get; set; }

        /// <summary>
        /// The namespace of the generated item.
        /// </summary>
        public virtual string BaseNamespace { get; set; }

        /// <summary>
        /// The name of the Visual Studio project in which this generated object will belong.
        /// </summary>
        public virtual string ProjectName { get; set; }

        /// <summary>
        /// The name of the Visual Studio project folder, in which this generated object will belong.
        /// </summary>
        public virtual string SubFolderName { get; set; }

        /// <summary>
        /// The app service base class from which all app services inherit.
        /// </summary>
        public virtual string BaseAppServiceClass { get; set; }

        /// <summary>
        /// The annotations on the app service base class
        /// </summary>
        public virtual string BaseAppServiceClassAnnotations { get; set; }

        /// <summary>
        /// Contains a dictionary of Property Sets belonging to this component, keyed by the property set id
        /// </summary>
        public virtual IDictionary<string, LaunchPadGeneratedDtoPropertySet> PropertySets { get; set; }

        /// <summary>
        /// Contains a dictionary of Domain Entity definitions belonging to this component, keyed by the domain entity id
        /// </summary>
        public virtual IDictionary<string, LaunchPadGeneratedObjectBase> DomainEntityDefinitions { get; set; }

        /// <summary>
        /// Contains a dictionary of Application Service definitions belonging to this component, keyed by the domain entity id
        /// </summary>
        public virtual IDictionary<string, LaunchPadGeneratedObjectBase> AppServiceDefinitions { get; set; }


        /// <summary>
        /// Contains the appsettings JSON elements belonging to this component
        /// </summary>
        public virtual ILaunchPadGeneratedAppSettings AppSettings { get; set; }


        public VisualStudioBlueprintDefinitionSettings() : base()
        {
            VisualStudioSolutionName = string.Empty;
            ProjectName = string.Empty;
            SubFolderName = string.Empty;
            BaseNamespace = string.Empty;
            BaseAppServiceClass = string.Empty;
            BaseAppServiceClassAnnotations = string.Empty; 
            var comparer = StringComparer.OrdinalIgnoreCase;
            PropertySets = new Dictionary<string, LaunchPadGeneratedDtoPropertySet>(comparer);
            DomainEntityDefinitions = new Dictionary<string, LaunchPadGeneratedObjectBase>(comparer);
            AppServiceDefinitions = new Dictionary<string, LaunchPadGeneratedObjectBase>(comparer);
        }
    }
}
