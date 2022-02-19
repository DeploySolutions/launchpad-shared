using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace DeploySoftware.LaunchPad.FileGeneration.Structure
{
    /// <summary>
    /// Represents a the map configuration in the application
    /// </summary>  
    [Serializable]
    [XmlRoot(ElementName = "Map")]
    [JsonObject(MemberSerialization.OptIn)]
    public partial class LaunchPadMap : LaunchPadWebClientObjectBase
    {
        /// <summary>
        /// Default location when the user deny the location permission
        /// </summary>
        [JsonProperty("defaultLocation")]
        public double[] DefaultLocation { get; set; }

        /// <summary>
        /// Map library token (MapBox, GoogleMap, ESRI, etc)
        /// </summary>
        [JsonProperty("token")]
        public string Token { get; set; }


        public LaunchPadMap() : base()
        {
        }
    }
}
