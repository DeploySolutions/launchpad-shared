using System;
using System.Collections.Generic;
using System.Text;

namespace DeploySoftware.LaunchPad.Core.FileGeneration
{
    [Serializable]
    public partial class LaunchPadGeneratedVisualStudioModuleConfiguration : LaunchPadGeneratedConfigurationBase
    {
        
        /// <summary>
        /// The namespace of the generated item.
        /// </summary>
        public string SolutionBaseNamespace { get; set; }


        public LaunchPadGeneratedVisualStudioModuleConfiguration() : base()
        {
            SolutionBaseNamespace = string.Empty;
        }
    }
}
