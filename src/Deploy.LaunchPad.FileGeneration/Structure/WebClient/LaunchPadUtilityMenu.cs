using Newtonsoft.Json;
using System;
using System.Xml.Serialization;

namespace Deploy.LaunchPad.FileGeneration.Structure
{
    /// <summary>
    /// Represents a the utility menu of the application
    /// </summary>  
    [Serializable]
    [XmlRoot(ElementName = "UtilityMenu")]
    [JsonObject(MemberSerialization.OptIn)]
    public partial class LaunchPadUtilityMenu : LaunchPadWebClientObjectBase
    {
        /// <summary>
        /// Configuration of where the menu should be: "top" or "side".
        /// </summary>
        [JsonProperty("location")]
        public string Location { get; set; }


        public LaunchPadUtilityMenu() : base()
        {
            Location = string.Empty;
        }
    }
}
