using System;
using System.Collections.Generic;
using System.Text;

namespace DeploySoftware.LaunchPad.Core.FileGeneration
{
    [Serializable]
    public partial class LaunchPadGeneratedVisualStudioModuleConfiguration
    {
        
        /// <summary>
        /// The name of the Visual Studio Solution in which this generated module will belong.
        /// </summary>
        public string SolutionName { get; set; }

        /// <summary>
        /// The file path to the root of the Visual Studio solution on the file system.
        /// </summary>
        public string SolutionRootFilePath { get; set; }

        /// <summary>
        /// The namespace of the generated item.
        /// </summary>
        public string SolutionBaseNamespace { get; set; }


        public LaunchPadGeneratedVisualStudioModuleConfiguration() : base()
        {
            SolutionName = string.Empty;
            SolutionRootFilePath = string.Empty;
            SolutionBaseNamespace = string.Empty;

        }
    }
}
