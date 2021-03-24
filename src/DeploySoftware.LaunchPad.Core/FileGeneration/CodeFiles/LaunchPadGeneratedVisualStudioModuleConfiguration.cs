using System;
using System.Collections.Generic;
using System.Text;

namespace DeploySoftware.LaunchPad.Core.FileGeneration
{
    [Serializable]
    public partial class LaunchPadGeneratedVisualStudioModuleConfiguration : LaunchPadGeneratedConfigurationBase
    {
        /// <summary>
        /// The name of the Visual Studio solution (.sln) in which this generated module will belong.
        /// Note: this solution configuration is deliberately placed at the Visual Studio Module level, 
        /// and is not the same as a LaunchPadGeneratedSolution object.
        /// </summary>
        public virtual string VisualStudioSolutionName { get; set; }

        /// <summary>
        /// The namespace of the generated item.
        /// </summary>
        public virtual string BaseNamespace { get; set; }


        public LaunchPadGeneratedVisualStudioModuleConfiguration() : base()
        {
            VisualStudioSolutionName = string.Empty;
            BaseNamespace = string.Empty;
        }
    }
}
