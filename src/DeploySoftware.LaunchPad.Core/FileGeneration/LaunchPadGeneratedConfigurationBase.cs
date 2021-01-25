using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeploySoftware.LaunchPad.Core.FileGeneration
{
    public abstract partial class LaunchPadGeneratedConfigurationBase
    {

        /// <summary>
        /// The name of the Solution in which this generated module will belong.
        /// </summary>
        public string SolutionName { get; set; }

        /// <summary>
        /// The file path to the root of the document set solution on the file system.
        /// </summary>
        public string SolutionRootFilePath { get; set; }

        public string RepositoryName { get; set; }

        public Uri RepositoryUri { get; set; }

        public LaunchPadGeneratedConfigurationBase()
        {
            SolutionName = string.Empty;
            SolutionRootFilePath = string.Empty;
            RepositoryName = string.Empty;
        }

    }
}
