// ***********************************************************************
// Assembly         : Deploy.LaunchPad.FileGeneration
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 04-21-2023
// ***********************************************************************
// <copyright file="LaunchPadMap.cs" company="Deploy Software Solutions, inc.">
//     2018-2023 Deploy Software Solutions, inc.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using static System.Formats.Asn1.AsnWriter;

namespace Deploy.LaunchPad.FileGeneration.Structure
{
    /// <summary>
    /// Represents a the map display definition
    /// </summary>
    [Serializable]
    [XmlRoot(ElementName = "Map")]
    [JsonObject(MemberSerialization.OptIn)]
    public partial class LaunchPadMap : LaunchPadWebClientObjectBase
    {
        /// <summary>
        /// Icon of the map layer. Displayed on the map menu.
        /// </summary>
        /// <value>The icon.</value>
        [JsonProperty("icon")]
        public string Icon { get; set; } = string.Empty;

        /// <summary>
        /// Map library token (MapBox, GoogleMap, ESRI, etc)
        /// </summary>
        /// <value>The source.</value>
        [JsonProperty("source")]
        public LaunchPadMapSource Source { get; set; }

        /// <summary>
        /// Layer definition
        /// </summary>
        /// <value>The layer.</value>
        [JsonProperty("layer")]
        public LaunchPadMapLayer Layer { get; set; }

        /// <summary>
        /// Popover definition. This is displayed when the user clicks on the item on the map layer.
        /// </summary>
        /// <value>The popovers.</value>
        [JsonProperty("popover")]
        public IList<LaunchPadMapPopover> Popovers { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="LaunchPadMap"/> class.
        /// </summary>
        public LaunchPadMap() : base()
        {
            Popovers = new List<LaunchPadMapPopover>();
            Source = new LaunchPadMapSource();
            Layer = new LaunchPadMapLayer();
        }

        /// <summary>
        /// Serialization constructor used for deserialization
        /// </summary>
        /// <param name="info">The serialization info</param>
        /// <param name="context">The context of the stream</param>
        protected LaunchPadMap(SerializationInfo info, StreamingContext context) :base(info, context)
        {
            Icon = info.GetString("Icon");
            Source = (LaunchPadMapSource)info.GetValue("Source", typeof(LaunchPadMapSource));
            Layer = (LaunchPadMapLayer)info.GetValue("Layer", typeof(LaunchPadMapLayer));
            Popovers = (IList<LaunchPadMapPopover>)info.GetValue("Popovers", typeof(List<LaunchPadMapPopover>));

        }

        /// <summary>
        /// The method required for implementing ISerializable
        /// </summary>
        /// <param name="info">The information.</param>
        /// <param name="context">The context.</param>
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue("Icon", Icon);
            info.AddValue("Source", Source);
            info.AddValue("Layer", Layer);
            info.AddValue("Popovers", Popovers);

        }
    }
}
