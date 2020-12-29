using System;
using System.Collections.Generic;
using System.Text;

namespace DeploySoftware.LaunchPad.Core.CodeGeneration
{
    public partial class LaunchPadGeneratedVisualStudioClassConfiguration : LaunchPadGeneratedVisualStudioSolutionConfiguration
    {
       
        /// <summary>
        /// The name of the Visual Studio project in which this generated object will belong.
        /// </summary>
        public string ProjectName { get; set; }

        /// <summary>
        /// The name of the Visual Studio project folder, in which this generated object will belong.
        /// </summary>
        public string SubFolderName { get; set; }

        public LaunchPadGeneratedVisualStudioClassConfiguration() : base()
        {
            ProjectName = string.Empty;
            SubFolderName = string.Empty;
        }
    }
}
