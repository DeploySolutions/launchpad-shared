using DeploySoftware.LaunchPad.FileGeneration.Stages;
using DeploySoftware.LaunchPad.FileGeneration.Stages.Defining;
using System;
using System.Collections.Generic;
using System.Text;

namespace DeploySoftware.LaunchPad.FileGeneration.Structure
{
    [Serializable]
    public partial class WebAppModuleSettings : LaunchPadGeneratedObjectBlueprintDefinitionSettings
    {
        /// <summary>
        /// The name of the web app
        /// </summary>
        public virtual string WebAppName { get; set; }

        /// The name of the Visual Studio solution (.sln) in which this generated module will belong.
        /// Note: this solution configuration is deliberately placed at the Visual Studio Module level, 
        /// and is not the same as a LaunchPadGeneratedSolution object.
        /// </summary>
        public virtual string VisualStudioSolutionName { get; set; }


        /// <summary>
        /// Contains the appsettings JSON elements belonging to this component
        /// </summary>
        public virtual ILaunchPadGeneratedAppSettings AppSettings { get; set; }

        public WebAppModuleSettings() : base()
        {
            WebAppName = string.Empty;
            VisualStudioSolutionName = string.Empty;
        }
    }
}
