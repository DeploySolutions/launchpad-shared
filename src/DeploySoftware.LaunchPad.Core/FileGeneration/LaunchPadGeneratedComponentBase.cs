﻿using System;
using System.Collections.Generic;

namespace DeploySoftware.LaunchPad.Core.FileGeneration
{
    /// <summary>
    /// Represents a component generated by LaunchPad Framework.
    /// </summary>    
    [Serializable]
    public abstract partial class LaunchPadGeneratedComponentBase : LaunchPadGeneratedObjectBase
    {
        /// <summary>
        /// The version of this component
        /// </summary>
        public string Version { get; set; }

        /// <summary>
        /// The id type of the entities (if any).
        /// </summary>
        public string EntityIdType { get; set; }

        public LaunchPadGeneratedComponentBase() : base()
        {
            Version = string.Empty;
            EntityIdType = string.Empty;
        }
    }
}
