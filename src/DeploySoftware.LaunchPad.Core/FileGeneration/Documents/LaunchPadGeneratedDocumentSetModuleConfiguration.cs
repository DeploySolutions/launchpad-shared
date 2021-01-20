using System;
using System.Collections.Generic;
using System.Text;

namespace DeploySoftware.LaunchPad.Core.FileGeneration
{
    [Serializable]
    public partial class LaunchPadGeneratedDocumentSetModuleConfiguration
    {
        
        /// <summary>
        /// The name of the Document Set Solution in which this generated module will belong.
        /// </summary>
        public string SolutionName { get; set; }

        /// <summary>
        /// The file path to the root of the document set solution on the file system.
        /// </summary>
        public string SolutionRootFilePath { get; set; }



        public LaunchPadGeneratedDocumentSetModuleConfiguration() : base()
        {
            SolutionName = string.Empty;
            SolutionRootFilePath = string.Empty;
        }
    }
}
