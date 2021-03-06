﻿using System;
using System.Collections.Generic;

namespace DeploySoftware.LaunchPad.Core.FileGeneration
{
    /// <summary>
    /// Represents an application service class generated by LaunchPad Framework, corresponding to a domain entity.
    /// </summary>    
    [Serializable]
    public partial class LaunchPadGeneratedApplicationService : LaunchPadGeneratedClassBase
    {
        /// <summary>
        /// The C# Type of the underlying Domain Entity this application service uses (if any)
        /// </summary>
        public virtual string DomainEntityType { get; set; }

        /// <summary>
        /// Contains a collection of Data Transfer Objects belonging to this component
        /// </summary>
        public virtual IDictionary<string,LaunchPadGeneratedDataTransferObject> DataTransferObjects { get; set; }

        public LaunchPadGeneratedApplicationService() : base()
        {
            DomainEntityType = string.Empty;
            var comparer = StringComparer.OrdinalIgnoreCase;
            DataTransferObjects = new Dictionary<string, LaunchPadGeneratedDataTransferObject>(comparer);
        }
    }
}
