using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace DeploySoftware.LaunchPad.FileGeneration.Structure
{
    /// <summary>
    /// Define the data that will show up on the popupover box.
    /// </summary>  
    [Serializable]
    [XmlRoot(ElementName = "MapLayerPopover")]
    [JsonObject(MemberSerialization.OptIn)]
    public partial class LaunchPadMapPopover : LaunchPadWebClientObjectBase
    {
        /// <summary>
        /// The data field representing the popover data
        /// </summary>
        [JsonProperty("data")]
        public string Data { get; set; }

        /// <summary>
        /// Data type of the popover field to help frontend know how to format the field
        /// </summary>
        [JsonProperty("dataType")]
        public string DataType { get; set; }

        public LaunchPadMapPopover() : base()
        {
        }
    }
}
