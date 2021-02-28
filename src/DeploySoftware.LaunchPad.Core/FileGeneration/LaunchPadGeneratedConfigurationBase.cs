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
        /// The folder in which this item can be located, relative to its parent (LaunchPadGeneratedObject) object's folder.
        /// If it's empty, it is located in the same folder as its parent object.
        /// </summary>
        public string RelativeFolderPathFromParentObjectFolder { get; set; }

        public virtual SourceControlRepository Repository { get; set; }

        /// <summary>
        /// The version of this module
        /// </summary>
        public virtual string Version { get; set; }

        public LaunchPadGeneratedConfigurationBase()
        {
            RelativeFolderPathFromParentObjectFolder = string.Empty;
            Repository = new SourceControlRepository();
            Version = string.Empty;
        }

    }
}
