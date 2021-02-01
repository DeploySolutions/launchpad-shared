using System;
using System.Collections.Generic;
using System.Text;

namespace DeploySoftware.LaunchPad.Core.FileGeneration
{
    [Serializable]
    public partial class LaunchPadGeneratedVisualStudioModuleConfiguration : LaunchPadGeneratedConfigurationBase
    {
        /// <summary>
        /// The name of the Visual Studio Solution in which this generated module will belong.
        /// </summary>
        public string VisualStudioSolutionName { get; set; }

        /// <summary>
        /// The namespace of the generated item.
        /// </summary>
        public string SolutionBaseNamespace { get; set; }


        public LaunchPadGeneratedVisualStudioModuleConfiguration() : base()
        {
            VisualStudioSolutionName = string.Empty;
            SolutionBaseNamespace = string.Empty;
        }
    }
}
