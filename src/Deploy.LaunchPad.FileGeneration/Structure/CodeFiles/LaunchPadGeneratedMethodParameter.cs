using System;

namespace Deploy.LaunchPad.FileGeneration.Structure
{
    /// <summary>
    /// Represent a C# method parameter
    /// </summary>    
    [Serializable]
    public partial class LaunchPadGeneratedMethodParameter : LaunchPadGeneratedMethodFieldBase
    {
        /// <summary>
        /// The C# type of the parameter.
        /// </summary>
        public virtual string DataType { get; set; } = "System.String";

        /// <summary>
        /// If no value is set, is there a default? If none, the default value is set to empty string.
        /// </summary>
        public virtual string DefaultValue { get; set; } = string.Empty;

        /// <summary>
        /// Show an example valid value, for testing or documentation purposes
        /// </summary>
        public virtual string ExampleValue { get; set; } = string.Empty;

        public virtual bool IsRequired { get; set; } = false;

        public LaunchPadGeneratedMethodParameter() : base()
        {
            DataType = string.Empty;
            DefaultValue = string.Empty;
            ExampleValue = string.Empty;
            IsRequired = false;
        }

    }
}
