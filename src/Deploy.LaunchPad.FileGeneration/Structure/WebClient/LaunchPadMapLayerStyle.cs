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
        [JsonProperty("color")]
        public string Color { get; set; }

        /// <summary>
        /// If the layer type is circle, this specify the radius of the circle.
        /// </summary>
        [JsonProperty("radius")]
        public int Radius { get; set; }

        /// <summary>
        /// Set the size of the marker or any SVG symbol
        /// </summary>
        [JsonProperty("size")]
        public string Size { get; set; }

        public LaunchPadMapLayerStyle() : base()
        {
        }
    }
}
