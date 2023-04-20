using System;
using System.Runtime.Serialization;

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

        public virtual bool IsRequired { get; set; } = true;

        public LaunchPadGeneratedMethodParameter() : base()
        {
            DataType = string.Empty;
            DefaultValue = string.Empty;
            ExampleValue = string.Empty;
            IsRequired = false;
        }

        /// <summary>
        /// Serialization constructor used for deserialization
        /// </summary>
        /// <param name="info">The serialization info</param>
        /// <param name="context">The context of the stream</param>
        protected LaunchPadGeneratedMethodParameter(SerializationInfo info, StreamingContext context) : base(info,context)
        {
            DataType = info.GetString("DataType");
            DefaultValue = info.GetString("DefaultValue");
            ExampleValue = info.GetString("ExampleValue");
            IsRequired = info.GetBoolean("IsRequired");
        }

        /// <summary>
        /// The method required for implementing ISerializable
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue("DataType", DataType);
            info.AddValue("DefaultValue", DefaultValue);
            info.AddValue("ExampleValue", ExampleValue);
            info.AddValue("IsRequired", IsRequired);
        }
    }
}
