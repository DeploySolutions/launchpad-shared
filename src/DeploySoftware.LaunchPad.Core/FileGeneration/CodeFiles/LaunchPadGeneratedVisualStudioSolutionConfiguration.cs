using System;
using System.Collections.Generic;
using System.Text;

namespace DeploySoftware.LaunchPad.Core.FileGeneration
{
    [Serializable]
    public partial class LaunchPadGeneratedVisualStudioSolutionConfiguration : LaunchPadGeneratedSolutionConfiguration
    {
        /// <summary>
        /// The name of the Visual Studio solution (.sln) in which this generated module will belong.
        /// Note: this solution configuration is not the same as a LaunchPadGeneratedSolution object.
        /// </summary>
        public virtual string VisualStudioSolutionName { get; set; }

        /// <summary>
        /// The namespace of the generated item.
        /// </summary>
        public virtual string SolutionBaseNamespace { get; set; }


        public LaunchPadGeneratedVisualStudioSolutionConfiguration() : base()
        {
            VisualStudioSolutionName = string.Empty;
            SolutionBaseNamespace = string.Empty;
        }
    }
}
