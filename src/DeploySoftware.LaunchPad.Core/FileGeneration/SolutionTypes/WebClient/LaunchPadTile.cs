using System;
using System.Xml.Serialization;

namespace DeploySoftware.LaunchPad.Core.FileGeneration
{
    public enum TileType { Route }

    /// <summary>
    /// Represents a tile, which is a block of label and icon with a link to other page. We want to support multiple type of tile later on, not just tile that open a new route. This could be data tile, image tile, chart tile etc.
    /// </summary>  
    [Serializable]
    [XmlRoot(ElementName = "Tile")]
    public partial class LaunchPadTile : LaunchPadWebClientObjectBase
    {
        /// <summary>
        /// We only support `route` as a type at the moment
        /// </summary>
        [XmlAttribute("type")]
        public TileType Type { get; set; }

        /// <summary>
        /// At the moment we limit the icon to our set of available SVG. We may be able to support other format of icons later.
        /// </summary>
        [XmlAttribute("icon")]
        public string Icon { get; set; }

        public LaunchPadTile()
        {
            Name = string.Empty;
            Type = TileType.Route;
            Icon = string.Empty;
        }
    }
}
