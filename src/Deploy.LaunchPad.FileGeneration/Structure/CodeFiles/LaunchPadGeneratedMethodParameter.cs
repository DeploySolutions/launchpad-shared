// ***********************************************************************
// Assembly         : Deploy.LaunchPad.FileGeneration
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 04-20-2023
// ***********************************************************************
// <copyright file="LaunchPadGeneratedMethodParameter.cs" company="Deploy Software Solutions, inc.">
//     2018-2023 Deploy Software Solutions, inc.
// </copyright>
// <summary></summary>
// ***********************************************************************
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
        /// <value>The type of the data.</value>
        public virtual string DataType { get; set; } = "System.String";

        /// <summary>
        /// If no value is set, is there a default? If none, the default value is set to empty string.
        /// </summary>
        /// <value>The default value.</value>
        public virtual string DefaultValue { get; set; } = string.Empty;

        /// <summary>
        /// Show an example valid value, for testing or documentation purposes
        /// </summary>
        /// <value>The example value.</value>
        public virtual string ExampleValue { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets a value indicating whether this instance is required.
        /// </summary>
        /// <value><c>true</c> if this instance is required; otherwise, <c>false</c>.</value>
        public virtual bool IsRequired { get; set; } = true;

        /// <summary>
        /// Initializes a new instance of the <see cref="LaunchPadGeneratedMethodParameter"/> class.
        /// </summary>
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
        /// <param name="info">The information.</param>
        /// <param name="context">The context.</param>
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
