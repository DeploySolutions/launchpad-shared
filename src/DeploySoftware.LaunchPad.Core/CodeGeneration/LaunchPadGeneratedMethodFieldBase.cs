using System;
using System.Collections.Generic;
using System.Text;

namespace DeploySoftware.LaunchPad.Core.CodeGeneration
{
    /// <summary>
    /// Represent a C# method parameter or field - this is the top level element in the LaunchPad Generated object hierarchy.
    /// </summary>    
    [Serializable]
    public abstract partial class LaunchPadGeneratedMethodFieldBase
    {
        /// <summary>
        /// The unique id of the object (if present)
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// The singular name of the object 
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The prefix to apply to the name (if any).
        /// </summary>
        public string NamePrefix { get; set; }

        /// <summary>
        /// The suffix to apply to the name (if any).
        /// </summary>
        public string NameSuffix { get; set; }

        /// <summary>
        /// The description of the object
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Code annotations for the object
        /// </summary>
        public string Annotations { get; set; }

        public LaunchPadGeneratedMethodFieldBase()
        {
            Id = string.Empty;
            Name = string.Empty; 
            NamePrefix = string.Empty;
            NameSuffix = string.Empty;
            Description = string.Empty;
            Annotations = string.Empty;
        }

    }
}
