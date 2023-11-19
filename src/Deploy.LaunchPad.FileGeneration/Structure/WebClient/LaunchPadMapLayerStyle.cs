// ***********************************************************************
// Assembly         : Deploy.LaunchPad.FileGeneration
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 01-08-2023
// ***********************************************************************
// <copyright file="LaunchPadMapLayerStyle.cs" company="Deploy Software Solutions, inc.">
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
    /// Define the style of the layer of the map. The attributes maybe different depending on the type of the layer.
    /// </summary>
    [Serializable]
    [XmlRoot(ElementName = "MapLayerStyle")]
    [JsonObject(MemberSerialization.OptIn)]
    public partial class LaunchPadMapLayerStyle : LaunchPadWebClientObjectBase
    {
        /// <summary>
        /// Fill color of any symbol on the map
        /// </summary>
        /// <value>The color.</value>
        [JsonProperty("color")]
        public string Color { get; set; }

        /// <summary>
        /// If the layer type is circle, this specify the radius of the circle.
        /// </summary>
        /// <value>The radius.</value>
        [JsonProperty("radius")]
        public int Radius { get; set; }

        /// <summary>
        /// Set the size of the marker or any SVG symbol
        /// </summary>
        /// <value>The size.</value>
        [JsonProperty("size")]
        public string Size { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="LaunchPadMapLayerStyle"/> class.
        /// </summary>
        public LaunchPadMapLayerStyle() : base()
        {
        }
    }
}
