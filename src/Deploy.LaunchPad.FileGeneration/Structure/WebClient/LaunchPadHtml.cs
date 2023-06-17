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
        [XmlAttribute("endpoint")]
        [JsonProperty("endpoint")]
        public string Background { get; set; }

        /// <summary>
        /// Embedded HTML content
        /// </summary>
        [XmlAttribute("content")]
        [JsonProperty("content")]
        public string Content { get; set; }

        public LaunchPadHtml() : base()
        {
        }
    }
}
