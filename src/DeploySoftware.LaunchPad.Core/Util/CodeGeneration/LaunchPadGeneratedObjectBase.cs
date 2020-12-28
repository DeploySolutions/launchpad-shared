using System;
using System.Collections.Generic;
using System.Text;

namespace DeploySoftware.LaunchPad.Core.Util
{
    /// <summary>
    /// The base class containing properties used for higher-level elements (classes, components, modules, etc) in LaunchPad RAD code generation processes.
    /// </summary>
    public abstract partial class LaunchPadGeneratedObjectBase : LaunchPadGeneratedMethodFieldBase
    {
        /// <summary>
        /// Contains information related to this object's position with a Visual Studio solution
        /// </summary>
        public LaunchPadGeneratedVisualStudioConfiguration VisualStudioConfig { get; set; }

        /// <summary>
        /// The namespace of the generated item.
        /// </summary>
        public string Namespace { get; set; }

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
            Namespace = string.Empty;
            ObjectType = string.Empty;
            IdType = string.Empty;
            VisualStudioConfig = new LaunchPadGeneratedVisualStudioConfiguration();
        }

    }
}
