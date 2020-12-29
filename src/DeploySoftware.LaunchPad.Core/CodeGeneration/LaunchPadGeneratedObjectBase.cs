using System;
using System.Collections.Generic;
using System.Text;

namespace DeploySoftware.LaunchPad.Core.CodeGeneration
{
    /// <summary>
    /// The base class containing properties used for higher-level elements (classes, components, modules, etc) in LaunchPad RAD code generation processes.
    /// </summary>
    public abstract partial class LaunchPadGeneratedObjectBase : LaunchPadGeneratedMethodFieldBase
    {
        
        /// <summary>
        /// The C# type of this object
        /// </summary>
        public string ObjectType { get; set; }

        /// <summary>
        /// The C# type of this object's id.
        /// </summary>
        public string IdType { get; set; } = "System.Int32";

        public LaunchPadGeneratedObjectBase() : base()
        {
            ObjectType = string.Empty;
            IdType = string.Empty;            
        }

    }
}
