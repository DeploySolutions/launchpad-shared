using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using static System.Formats.Asn1.AsnWriter;

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
            Popovers = new List<LaunchPadMapPopover>();
        }

        /// <summary>
        /// Serialization constructor used for deserialization
        /// </summary>
        /// <param name="info">The serialization info</param>
        /// <param name="context">The context of the stream</param>
        protected LaunchPadMap(SerializationInfo info, StreamingContext context) :base(info, context)
        {
            Icon = info.GetString("Icon");
            Source = (LaunchPadMapSource)info.GetValue("Source", typeof(LaunchPadMapSource));
            Layer = (LaunchPadMapLayer)info.GetValue("Layer", typeof(LaunchPadMapLayer));
            Popovers = (IList<LaunchPadMapPopover>)info.GetValue("Popovers", typeof(IList<LaunchPadMapPopover>));

        }

        /// <summary>
        /// The method required for implementing ISerializable
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Icon", Icon);
            info.AddValue("Source", Source);
            info.AddValue("Layer", Layer);
            info.AddValue("Popovers", Popovers);

        }
    }
}
