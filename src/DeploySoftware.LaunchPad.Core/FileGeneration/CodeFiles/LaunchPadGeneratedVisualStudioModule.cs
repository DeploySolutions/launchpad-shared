﻿using System;
using System.Collections.Generic;

namespace DeploySoftware.LaunchPad.Core.FileGeneration
{
    /// <summary>
    /// Represents a module generated by LaunchPad Framework.
    /// </summary>    
    [Serializable]
    public partial class LaunchPadGeneratedVisualStudioModule : LaunchPadGeneratedModuleBase
    {
        /// <summary>
        /// Contains information related to this object's position with a Visual Studio solution
        /// </summary>
        public LaunchPadGeneratedVisualStudioModuleConfiguration Config { get; set; }

        /// <summary>
        /// The id type of the entities (if any).
        /// </summary>
        public string EntityIdType { get; set; }

        /// <summary>
        /// The list of components that belong to this module.
        /// </summary>
        public IList<LaunchPadGeneratedVisualStudioComponent> Components { get; set; }

        public LaunchPadGeneratedVisualStudioModule() : base()
        {
            Version = string.Empty;
            EntityIdType = string.Empty;
            Components = new List<LaunchPadGeneratedVisualStudioComponent>();
            Config = new LaunchPadGeneratedVisualStudioModuleConfiguration();
        }
    }
}
