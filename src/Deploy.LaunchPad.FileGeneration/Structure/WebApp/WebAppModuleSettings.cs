using Deploy.LaunchPad.FileGeneration.Stages;
using Deploy.LaunchPad.FileGeneration.Stages.Defining;
using System;

namespace Deploy.LaunchPad.FileGeneration.Structure
{
    [Serializable]
    public partial class WebAppModuleSettings : LaunchPadGeneratedObjectBlueprintDefinitionSettings
    {
        /// <summary>
        /// The name of the web app
        /// </summary>
        public virtual string WebAppName { get; set; } = string.Empty;

        /// The name of the Visual Studio solution (.sln) in which this generated module will belong.
        /// Note: this solution configuration is deliberately placed at the Visual Studio Module level, 
        /// and is not the same as a LaunchPadGeneratedSolution object.
        /// </summary>
        public virtual string VisualStudioSolutionName { get; set; } = string.Empty;

        /// <summary>
        /// The base namespace of the generated items in all the projects.
        /// </summary>
        public virtual string BaseNamespace { get; set; } = string.Empty;

        /// <summary>
        /// Contains the appsettings JSON elements belonging to this component
        /// </summary>
        public virtual ILaunchPadGeneratedAppSettings AppSettings { get; set; }

        public WebAppModuleSettings() : base()
        {
        }
    }
}
