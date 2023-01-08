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
        [JsonProperty("type")]
        public string Type { get; set; }

        /// <summary>
        /// Style of the layer
        /// </summary>
        [JsonProperty("style")]
        public LaunchPadMapLayerStyle Style { get; set; }

        public LaunchPadMapLayer() : base()
        {
        }
    }
}
