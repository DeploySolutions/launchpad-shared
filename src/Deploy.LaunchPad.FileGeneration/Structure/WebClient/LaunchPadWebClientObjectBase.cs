// ***********************************************************************
// Assembly         : Deploy.LaunchPad.FileGeneration
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 11-10-2023
// ***********************************************************************
// <copyright file="LaunchPadWebClientObjectBase.cs" company="Deploy Software Solutions, inc.">
//     2018-2023 Deploy Software Solutions, inc.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Newtonsoft.Json;
using System;
using System.Data;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Deploy.LaunchPad.FileGeneration.Structure
{
    /// <summary>
    /// The base class containing properties for all LaunchPad web client file generation processes.
    /// </summary>
    [Serializable]
    public abstract partial class LaunchPadWebClientObjectBase : LaunchPadGeneratedObjectBase
    {

        /// <summary>
        /// Id is always Guid, but in some cases we want to display an HTML id attribute with non-Guid text. This holds that value. So, for web client objects
        /// we serialize this item in json and override and hide the original id
        /// </summary>
        /// <value>The output identifier.</value>
        [JsonProperty("id")]
        public virtual string OutputId { get; set; } = string.Empty;

        /// <summary>
        /// The unique id of the object (if present)
        /// </summary>
        /// <value>The identifier.</value>
        [Newtonsoft.Json.JsonIgnore]
        public override Guid Id { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="LaunchPadWebClientObjectBase"/> class.
        /// </summary>
        public LaunchPadWebClientObjectBase() : base()
        {
        }

        /// <summary>
        /// Serialization constructor used for deserialization
        /// </summary>
        /// <param name="info">The serialization info</param>
        /// <param name="context">The context of the stream</param>
        protected LaunchPadWebClientObjectBase(SerializationInfo info, StreamingContext context) : base(info, context)
        {

        }

        /// <summary>
        /// The method required for implementing ISerializable
        /// </summary>
        /// <param name="info">The information.</param>
        /// <param name="context">The context.</param>
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
        }
    }
}
