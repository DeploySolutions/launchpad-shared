using System;
using System.Xml.Serialization;

namespace DeploySoftware.LaunchPad.Core.FileGeneration
{
    /// <summary>
    /// Represents a tile, which is a block of label and icon with a link to other page. We want to support multiple type of tile later on, not just tile that open a new route. This could be data tile, image tile, chart tile etc.
    /// </summary>  
    [Serializable]
    [XmlRoot(ElementName = "Filter")]
    public partial class LaunchPadFilter : LaunchPadWebClientObjectBase
    {
        /// <summary>
        /// Placeholder displayed in the filter input field
        /// </summary>
        [XmlAttribute("placeholder")]
        public string Placeholder { get; set; }

        /// <summary>
        /// Data field name representing the search. We use this field to call the search API with.
        /// </summary>
        [XmlAttribute("dataField")]
        public string DataField { get; set; }

        /// <summary>
        /// Type of data to apply field type validation in the client
        /// </summary>
        [XmlAttribute("dataType")]
        public string DataType { get; set; }

        public LaunchPadFilter() : base()
        {
            Placeholder = string.Empty;
            DataField = string.Empty;
            DataType = string.Empty;
        }
    }
}
