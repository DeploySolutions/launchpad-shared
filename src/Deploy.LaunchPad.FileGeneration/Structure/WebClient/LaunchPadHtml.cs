// ***********************************************************************
// Assembly         : Deploy.LaunchPad.FileGeneration
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 06-20-2023
// ***********************************************************************
// <copyright file="LaunchPadHtml.cs" company="Deploy Software Solutions, inc.">
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
    /// Represents HTML content. Could be a path to file, or embedded content
    /// </summary>
    [Serializable]
    [XmlRoot(ElementName = "Html")]
    [JsonObject(MemberSerialization.OptIn)]
    public partial class LaunchPadHtml : LaunchPadWebClientObjectBase
    {
        /// <summary>
        /// Endpoint to a file, this could be external or internal file.
        /// </summary>
        /// <value>The background.</value>
        [XmlAttribute("endpoint")]
        [JsonProperty("endpoint")]
        public string Background { get; set; }

        /// <summary>
        /// Embedded HTML content
        /// </summary>
        /// <value>The content.</value>
        [XmlAttribute("content")]
        [JsonProperty("content")]
        public string Content { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="LaunchPadHtml"/> class.
        /// </summary>
        public LaunchPadHtml() : base()
        {
        }
    }
}
