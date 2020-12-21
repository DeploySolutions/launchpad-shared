using System;
using System.Collections.Generic;
using System.Text;

namespace DeploySoftware.LaunchPad.Core.Util
{
    public class LaunchPadGeneratedVisualStudioConfiguration
    {
        
        /// <summary>
        /// The name of the Visual Studio Solution in which this generated object will belong.
        /// </summary>
        public string Solution { get; set; }

        /// <summary>
        /// The name of the Visual Studio project in which this generated object will belong.
        /// </summary>
        public string Project { get; set; }

        /// <summary>
        /// The name of the Visual Studio project folder, in which this generated object will belong.
        /// </summary>
        public string SubFolder { get; set; }

        public LaunchPadGeneratedVisualStudioConfiguration()
        {
            Solution = string.Empty;
            Project = string.Empty;
            SubFolder = string.Empty;
        }
    }
}
