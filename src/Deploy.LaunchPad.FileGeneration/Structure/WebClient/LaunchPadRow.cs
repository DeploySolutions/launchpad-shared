// ***********************************************************************
// Assembly         : Deploy.LaunchPad.FileGeneration
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 11-07-2023
// ***********************************************************************
// <copyright file="LaunchPadRow.cs" company="Deploy Software Solutions, inc.">
//     2018-2023 Deploy Software Solutions, inc.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Runtime.Serialization;

namespace Deploy.LaunchPad.FileGeneration.Structure
{
    /// <summary>
    /// Represents a row in a form or page.
    /// </summary>
    [Serializable]
    public partial class LaunchPadRow : LaunchPadWebClientObjectBase
    {
        /// <summary>
        /// Form items (input fields or buttons) included in this form
        /// </summary>
        /// <value>The items.</value>
        [JsonProperty("items")]
        public IList<LaunchPadWebItem> Items { get; set; }

        /// <summary>
        /// Number of row span
        /// </summary>
        /// <value>The span.</value>
        [JsonProperty("span", NullValueHandling = NullValueHandling.Include)]
        public int Span { get; set; } = 1;

        /// <summary>
        /// Initializes a new instance of the <see cref="LaunchPadRow"/> class.
        /// </summary>
        public LaunchPadRow() : base()
        {
            Id = Guid.Empty;
            Name = null;
        }

        /// <summary>
        /// Serialization constructor used for deserialization
        /// </summary>
        /// <param name="info">The serialization info</param>
        /// <param name="context">The context of the stream</param>
        protected LaunchPadRow(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            Span = info.GetInt32("Span");
            Items = (IList<LaunchPadWebItem>)info.GetValue("Items", typeof(List<LaunchPadWebItem>));

        }

        /// <summary>
        /// The method required for implementing ISerializable
        /// </summary>
        /// <param name="info">The information.</param>
        /// <param name="context">The context.</param>
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue("Items", Items);
            info.AddValue("Span", Span);
        }
    }
}
