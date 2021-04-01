using System;
using System.Xml.Serialization;

namespace DeploySoftware.LaunchPad.Core.FileGeneration
{
    /// <summary>
    /// Represents a navigation menu item on the UI
    /// </summary>  
    [Serializable]
    [XmlRoot(ElementName = "Navigation")]
    public partial class LaunchPadNavigation : LaunchPadWebClientObjectBase
    {
        /// <summary>
        /// Route to the page to open from this menu
        /// </summary>
        [XmlAttribute("route")]
        public string Route { get; set; }

        /// <summary>
        /// At the moment we limit the icon to our set of available SVG. We may be able to support other format of icons later.
        /// </summary>
        [XmlAttribute("icon")]
        public string Icon { get; set; }

        /// <summary>
        /// Name of the navigation menu, uses as a label
        /// </summary>
        [XmlElement]
        public LaunchPadPage Page { get; set; }


        public LaunchPadNavigation()
        {
            Name = string.Empty;
            Route = string.Empty;
            Icon = string.Empty;
        }
    }
}
