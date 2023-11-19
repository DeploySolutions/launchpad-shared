// ***********************************************************************
// Assembly         : Deploy.LaunchPad.FileGeneration
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 04-21-2023
// ***********************************************************************
// <copyright file="LaunchPadMapLayer.cs" company="Deploy Software Solutions, inc.">
//     2018-2023 Deploy Software Solutions, inc.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Newtonsoft.Json;
using System;
using System.Xml.Serialization;

namespace Deploy.LaunchPad.FileGeneration.Structure
{
    /// <summary>
    /// Define the layer style and popover.
    /// </summary>
    [Serializable]
    [XmlRoot(ElementName = "MapLayer")]
    [JsonObject(MemberSerialization.OptIn)]
    public partial class LaunchPadMapLayer : LaunchPadWebClientObjectBase
    {
        /// <summary>
        /// Type: "marker" or "circle". Marker displays a pin image on the location, while circle draws a circle.
        /// </summary>
        /// <value>The type.</value>
        [JsonProperty("type")]
        public string Type { get; set; }

        /// <summary>
        /// Style of the layer
        /// </summary>
        /// <value>The style.</value>
        [JsonProperty("style")]
        public LaunchPadMapLayerStyle Style { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="LaunchPadMapLayer"/> class.
        /// </summary>
        public LaunchPadMapLayer() : base()
        {
            Style = new LaunchPadMapLayerStyle();
        }
    }
}
