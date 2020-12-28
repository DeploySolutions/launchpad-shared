using System;
using System.Collections.Generic;
using System.Text;

namespace DeploySoftware.LaunchPad.Core.Util
{
    public partial class LaunchPadGeneratedVisualStudioConfiguration
    {
        
        /// <summary>
        /// The name of the Visual Studio Solution in which this generated object will belong.
        /// </summary>
        public string SolutionName { get; set; }

        /// <summary>
        /// The name of the Visual Studio project in which this generated object will belong.
        /// </summary>
        public string ProjectName { get; set; }

        /// <summary>
        /// The name of the Visual Studio project folder, in which this generated object will belong.
        /// </summary>
        public string SubFolderName { get; set; }

        public LaunchPadGeneratedVisualStudioConfiguration()
        {
            SolutionName = string.Empty;
            ProjectName = string.Empty;
            SubFolderName = string.Empty;
        }
    }
}
