using System;
using System.Collections.Generic;
using System.Text;

namespace DeploySoftware.LaunchPad.Core.FileGeneration
{
    [Serializable]
    public partial class LaunchPadGeneratedVisualStudioComponentConfiguration : 
        LaunchPadGeneratedConfiguration
    {
        /// The name of the Visual Studio solution (.sln) in which this generated module will belong.
        /// Note: this solution configuration is deliberately placed at the Visual Studio Module level, 
        /// and is not the same as a LaunchPadGeneratedSolution object.
        /// </summary>
        public virtual string VisualStudioSolutionName { get; set; }

        /// <summary>
        /// The namespace of the generated item.
        /// </summary>
        public virtual string BaseNamespace { get; set; }

        /// <summary>
        /// The name of the Visual Studio project in which this generated object will belong.
        /// </summary>
        public virtual string ProjectName { get; set; }

        /// <summary>
        /// The name of the Visual Studio project folder, in which this generated object will belong.
        /// </summary>
        public virtual string SubFolderName { get; set; }

        /// <summary>
        /// The app service base class from which all app services inherit.
        /// </summary>
        public virtual string BaseAppServiceClass { get; set; }

        /// <summary>
        /// The annotations on the app service base class
        /// </summary>
        public virtual string BaseAppServiceClassAnnotations { get; set; }

        public LaunchPadGeneratedVisualStudioComponentConfiguration() : base()
        {
            VisualStudioSolutionName = string.Empty;
            ProjectName = string.Empty;
            SubFolderName = string.Empty;
            BaseNamespace = string.Empty;
            BaseAppServiceClass = string.Empty;
            BaseAppServiceClassAnnotations = string.Empty;
        }
    }
}
