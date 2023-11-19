// ***********************************************************************
// Assembly         : Deploy.LaunchPad.FileGeneration
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 01-08-2023
// ***********************************************************************
// <copyright file="LaunchPadNavigationItem.cs" company="Deploy Software Solutions, inc.">
//     2018-2023 Deploy Software Solutions, inc.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Deploy.LaunchPad.FileGeneration.Structure
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
        /// <value>The name of the component.</value>
        [JsonProperty("componentName")]
        public string ComponentName { get; set; }

        /// <summary>
        /// Route to the page to open from this menu
        /// </summary>
        /// <value>The route.</value>
        [JsonProperty("route")]
        public string Route { get; set; }

        /// <summary>
        /// At the moment we limit the icon to our set of available SVG. We may be able to support other format of icons later.
        /// </summary>
        /// <value>The icon.</value>
        [JsonProperty("icon")]
        public string Icon { get; set; }

        /// <summary>
        /// Name of the navigation menu, uses as a label
        /// </summary>
        /// <value>The page.</value>
        public LaunchPadPage Page { get; set; }

        /// <summary>
        /// Nested page. This creates a submenu.
        /// </summary>
        /// <value>The sub menu.</value>
        [XmlElement]
        [JsonProperty("submenu")]
        public IList<LaunchPadNavigationItem> SubMenu { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="LaunchPadNavigationItem"/> class.
        /// </summary>
        public LaunchPadNavigationItem() : base()
        {
            Page = new LaunchPadPage();
        }
    }
}
