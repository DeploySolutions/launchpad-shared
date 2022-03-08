using DeploySoftware.LaunchPad.FileGeneration.Stages;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace DeploySoftware.LaunchPad.FileGeneration.Structure
{
    [Serializable]
    [JsonObject(MemberSerialization.OptIn)]
    public partial class WebClientBlueprintDefinitionSettings : 
        LaunchPadGeneratedObjectBlueprintDefinitionSettings
    {
        /// <summary>
        /// Brand configuration of the app. This could include name, icon, logo and/or theme color.
        /// </summary>
        [XmlElement]
        [JsonProperty]
        public LaunchPadBrand Brand { get; set; }

        /// <summary>
        /// Map configuration of the app.
        /// </summary>
        [XmlElement]
        [JsonProperty]
        public LaunchPadMapConfiguration Map { get; set; }

        /// <summary>
        /// The top section of the side navigation bar
        /// </summary>
        [XmlElement]
        [JsonProperty]
        public IList<LaunchPadNavigationItem> TopNavigations { get; set; }

        /// <summary>
        /// The bottom section of the side navigation bar
        /// </summary>
        [XmlElement]
        [JsonProperty]
        public IList<LaunchPadNavigationItem> BottomNavigations { get; set; }

        /// <summary>
        /// The list of static web pages that belong to this module.
        /// </summary>
        [XmlElement]
        public virtual IDictionary<string, LaunchPadGeneratedStaticWebPage> StaticWebPages { get; set; }

        /// <summary>
        /// The dictionary of dynamic stylesheets (.less or .sass) that belong to this module.
        /// </summary>
        [XmlElement]
        public virtual IDictionary<string, LaunchPadGeneratedStylesheet> Stylesheets { get; set; }

        public WebClientBlueprintDefinitionSettings() : base()
        {
        }
    }
}
