using System;
using System.Collections.Generic;
using System.Text;

namespace DeploySoftware.LaunchPad.Core.Util
{
    /// <summary>
    /// Represent a C# method parameter
    /// </summary>
    public class LaunchPadGeneratedMethodParameter : LaunchPadGeneratedObjectBase
    {
        /// <summary>
        /// This is the C# type of the generated property. 
        /// </summary>
        public string DataType { get; set; } = "System.String";

        /// <summary>
        /// If no value is set, is there a default? If none, the default value is set to empty string.
        /// </summary>
        public string DefaultValue { get; set; } = string.Empty;

        /// <summary>
        /// Show an example valid value, for testing or documentation purposes
        /// </summary>
        public string ExampleValue { get; set; } = string.Empty;

        public bool IsOptional { get; set; }

    }
}
