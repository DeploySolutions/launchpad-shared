using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Deploy.LaunchPad.FileGeneration.Structure
{
    /// <summary>
    /// Represents a the map display definition
    /// </summary>  
    [Serializable]
    [XmlRoot(ElementName = "Map")]
    [JsonObject(MemberSerialization.OptIn)]
    public partial class LaunchPadMap : LaunchPadWebClientObjectBase
    {
        /// <summary>
        /// Icon of the map layer. Displayed on the map menu.
        /// </summary>
        [JsonProperty("icon")]
        public string Icon { get; set; }

        /// <summary>
        /// Map library token (MapBox, GoogleMap, ESRI, etc)
        /// </summary>
        [JsonProperty("source")]
        public LaunchPadMapSource Source { get; set; }

        /// <summary>
        /// Layer definition
        /// </summary>
        [JsonProperty("layer")]
        public LaunchPadMapLayer Layer { get; set; }

        /// <summary>
        /// Popover definition. This is displayed when the user clicks on the item on the map layer.
        /// </summary>
        [JsonProperty("popover")]
        public IList<LaunchPadMapPopover> Popovers { get; set; }

        public LaunchPadMap() : base()
        {
        }
    }
}
