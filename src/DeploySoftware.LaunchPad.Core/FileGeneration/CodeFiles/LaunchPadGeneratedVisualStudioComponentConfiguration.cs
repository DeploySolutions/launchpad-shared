using System;
using System.Collections.Generic;
using System.Text;

namespace DeploySoftware.LaunchPad.Core.FileGeneration
{
    [Serializable]
    public partial class LaunchPadGeneratedVisualStudioComponentConfiguration : LaunchPadGeneratedVisualStudioModuleConfiguration
    {
       
        /// <summary>
        /// The name of the Visual Studio project in which this generated object will belong.
        /// </summary>
        public string ProjectName { get; set; }

        /// <summary>
        /// The name of the Visual Studio project folder, in which this generated object will belong.
        /// </summary>
        public string SubFolderName { get; set; }

        /// <summary>
        /// The app service base class from which all app services inherit.
        /// </summary>
        public string BaseAppServiceClass { get; set; }

        /// <summary>
        /// The annotations on the app service base class
        /// </summary>
        public string BaseAppServiceClassAnnotations { get; set; }

        public LaunchPadGeneratedVisualStudioComponentConfiguration() : base()
        {
            ProjectName = string.Empty;
            SubFolderName = string.Empty;
            BaseAppServiceClass = string.Empty;
            BaseAppServiceClassAnnotations = string.Empty;
        }
    }
}
