﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeploySoftware.LaunchPad.Core.FileGeneration
{
    public abstract partial class LaunchPadGeneratedConfigurationBase : ILaunchPadGeneratedConfiguration
    {
        /// <summary>
        /// The folder in which this item can be located, relative to its parent (LaunchPadGeneratedObject) object's folder.
        /// If it's empty, it is located in the same folder as its parent object.
        /// </summary>
        public string RelativeFolderPathFromParentObjectFolder { get; set; }

        /// <summary>
        /// The version of this module
        /// </summary>
        public virtual string Version { get; set; }

        public virtual SourceControlRepository Repository { get; set; }

        public LaunchPadGeneratedConfigurationBase()
        {
            Repository = new SourceControlRepository();
            RelativeFolderPathFromParentObjectFolder = string.Empty;
            Version = string.Empty;
        }

    }
}
