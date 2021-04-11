using System;
using System.Collections.Generic;
using System.Text;

namespace DeploySoftware.LaunchPad.Core.FileGeneration
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

        public WebAppModuleSettings() : base()
        {
            WebAppName = string.Empty;
            VisualStudioSolutionName = string.Empty;
        }
    }
}
