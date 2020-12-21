using System;
using System.Collections.Generic;
using System.Text;

namespace DeploySoftware.LaunchPad.Core.Util
{
    /// <summary>
    /// The base class containing properties used for LaunchPad RAD code generation processes.
    /// </summary>
    public abstract partial class LaunchPadGeneratedObjectBase
    {
        /// <summary>
        /// The singular name of the object 
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The description of the object
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Code annotations for the object
        /// </summary>
        public string Annotations { get; set; }
    }
}
