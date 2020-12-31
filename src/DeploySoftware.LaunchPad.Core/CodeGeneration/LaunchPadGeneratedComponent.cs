﻿using System;
using System.Collections.Generic;

namespace DeploySoftware.LaunchPad.Core.CodeGeneration
{
    /// <summary>
    /// Represents a component generated by LaunchPad Framework.
    /// </summary>    
    [Serializable]
    public partial class LaunchPadGeneratedComponent : LaunchPadGeneratedObjectBase
    {
        /// <summary>
        /// Contains information related to this object's position with a Visual Studio solution
        /// </summary>
        public LaunchPadGeneratedVisualStudioComponentConfiguration VisualStudioConfig { get; set; }

        /// <summary>
        /// The version of this component
        /// </summary>
        public string Version { get; set; }

        /// <summary>
        /// The id type of the entities (if any).
        /// </summary>
        public string EntityIdType { get; set; }

        /// <summary>
        /// The list of domain entities that belong to this component (if any).
        /// </summary>
        public IList<LaunchPadGeneratedClass> DomainEntities { get; set; }

        public LaunchPadGeneratedComponent() : base()
        {
            Version = string.Empty;
            EntityIdType = string.Empty;
            DomainEntities = new List<LaunchPadGeneratedClass>();
            VisualStudioConfig = new LaunchPadGeneratedVisualStudioComponentConfiguration();
        }
    }
}
