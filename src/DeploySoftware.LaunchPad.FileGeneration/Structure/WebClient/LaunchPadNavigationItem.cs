using Newtonsoft.Json;
using System;
using System.Xml.Serialization;

namespace DeploySoftware.LaunchPad.FileGeneration.Structure
{
    /// <summary>
    /// Represents a navigation menu item on the UI
    /// </summary>  
    [Serializable]
    [XmlRoot(ElementName = "NavigationItem")]
    [JsonObject(MemberSerialization.OptIn)]
    public partial class LaunchPadNavigationItem : LaunchPadWebClientObjectBase
    {
        /// <summary>
        /// User-friendly label for the navigation item
        /// </summary>
        [JsonProperty("componentName")]
        public string ComponentName { get; set; }

        /// <summary>
        /// Route to the page to open from this menu
        /// </summary>
        [JsonProperty("route")]
        public string Route { get; set; }

        /// <summary>
        /// At the moment we limit the icon to our set of available SVG. We may be able to support other format of icons later.
        /// </summary>
        [JsonProperty("icon")]
        public string Icon { get; set; }

        /// <summary>
        /// Name of the navigation menu, uses as a label
        /// </summary>
        public LaunchPadPage Page { get; set; }

        public LaunchPadNavigationItem() : base()
        {
            Page = new LaunchPadPage();
        }
    }
}
