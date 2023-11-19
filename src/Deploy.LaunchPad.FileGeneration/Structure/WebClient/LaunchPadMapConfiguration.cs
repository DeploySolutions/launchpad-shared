// ***********************************************************************
// Assembly         : Deploy.LaunchPad.FileGeneration
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 01-08-2023
// ***********************************************************************
// <copyright file="LaunchPadMapConfiguration.cs" company="Deploy Software Solutions, inc.">
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
    /// Represents a the map configuration in the application
    /// </summary>
    [Serializable]
    [XmlRoot(ElementName = "MapConfiguration")]
    [JsonObject(MemberSerialization.OptIn)]
    public partial class LaunchPadMapConfiguration : LaunchPadWebClientObjectBase
    {
        /// <summary>
        /// Default location when the user deny the location permission
        /// </summary>
        /// <value>The default location.</value>
        [JsonProperty("defaultLocation")]
        public double[] DefaultLocation { get; set; }

        /// <summary>
        /// Map library token (MapBox, GoogleMap, ESRI, etc)
        /// </summary>
        /// <value>The token.</value>
        [JsonProperty("token")]
        public string Token { get; set; }


        /// <summary>
        /// Initializes a new instance of the <see cref="LaunchPadMapConfiguration"/> class.
        /// </summary>
        public LaunchPadMapConfiguration() : base()
        {
        }
    }
}
