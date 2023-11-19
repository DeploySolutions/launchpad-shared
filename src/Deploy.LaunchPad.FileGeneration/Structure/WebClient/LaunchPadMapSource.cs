// ***********************************************************************
// Assembly         : Deploy.LaunchPad.FileGeneration
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 01-08-2023
// ***********************************************************************
// <copyright file="LaunchPadMapSource.cs" company="Deploy Software Solutions, inc.">
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
        /// <value>The type.</value>
        [JsonProperty("type")]
        public string Type { get; set; }

        /// <summary>
        /// Location of the GeoJSON or the API endpoint
        /// </summary>
        /// <value>The location.</value>
        [JsonProperty("location")]
        public string Location { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="LaunchPadMapSource"/> class.
        /// </summary>
        public LaunchPadMapSource() : base()
        {
        }
    }
}
