using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeploySoftware.LaunchPad.Core.FileGeneration
{
    public interface ILaunchPadGeneratedConfiguration
    {
        /// <summary>
        /// The folder in which this item can be located, relative to its parent (LaunchPadGeneratedObject) object's folder.
        /// If it's empty, it is located in the same folder as its parent object.
        /// </summary>
        public string RelativeFolderPathFromParentObjectFolder { get; set; }

        /// <summary>
        /// The version of this module
        /// </summary>
        public string Version { get; set; }

        public SourceControlRepository Repository { get; set; }

    }
}
