using Newtonsoft.Json;
using System;
using System.Xml.Serialization;

namespace Deploy.LaunchPad.FileGeneration.Structure
{
    /// <summary>
    /// Define the source of the layer of the map. Depending on where the data come from, this could be a GeoJSON or an API call.
    /// </summary>  
    [Serializable]
    [XmlRoot(ElementName = "MapSource")]
    [JsonObject(MemberSerialization.OptIn)]
    public partial class LaunchPadMapSource : LaunchPadWebClientObjectBase
    {
        /// <summary>
        /// Type: "geojson" or "api"
        /// </summary>
        [JsonProperty("type")]
        public string Type { get; set; }

        /// <summary>
        /// Location of the GeoJSON or the API endpoint
        /// </summary>
        [JsonProperty("location")]
        public string Location { get; set; }

        public LaunchPadMapSource() : base()
        {
        }
    }
}
