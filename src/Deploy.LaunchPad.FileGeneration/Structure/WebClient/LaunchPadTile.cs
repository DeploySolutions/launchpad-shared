// ***********************************************************************
// Assembly         : Deploy.LaunchPad.FileGeneration
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 01-08-2023
// ***********************************************************************
// <copyright file="LaunchPadTile.cs" company="Deploy Software Solutions, inc.">
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
    /// Enum TileType
    /// </summary>
    public enum TileType { Route }

    /// <summary>
    /// Represents a tile, which is a block of label and icon with a link to other page. We want to support multiple type of tile later on, not just tile that open a new route. This could be data tile, image tile, chart tile etc.
    /// </summary>
    [Serializable]
    [XmlRoot(ElementName = "Tile")]
    [JsonObject(MemberSerialization.OptIn)]
    public partial class LaunchPadTile : LaunchPadWebClientObjectBase
    {
        /// <summary>
        /// We only support `route` as a type at the moment
        /// </summary>
        /// <value>The type.</value>
        [XmlAttribute("type")]
        public TileType Type { get; set; }

        /// <summary>
        /// At the moment we limit the icon to our set of available SVG. We may be able to support other format of icons later.
        /// </summary>
        /// <value>The icon.</value>
        [XmlAttribute("icon")]
        public string Icon { get; set; }

        /// <summary>
        /// Background image of the tile.
        /// </summary>
        /// <value>The background.</value>
        [XmlAttribute("background")]
        [JsonProperty("background")]
        public string Background { get; set; }

        /// <summary>
        /// Link to external resource/website.
        /// </summary>
        /// <value>The URL.</value>
        [XmlAttribute("url")]
        [JsonProperty("url")]
        public string URL { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="LaunchPadTile"/> class.
        /// </summary>
        public LaunchPadTile() : base()
        {
            Type = TileType.Route;
            Icon = string.Empty;
        }
    }
}
