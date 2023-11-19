// ***********************************************************************
// Assembly         : Deploy.LaunchPad.FileGeneration
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 02-03-2023
// ***********************************************************************
// <copyright file="WebClientBlueprintDefinitionSettings.cs" company="Deploy Software Solutions, inc.">
//     2018-2023 Deploy Software Solutions, inc.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Deploy.LaunchPad.FileGeneration.Stages;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Deploy.LaunchPad.FileGeneration.Structure
{
    /// <summary>
    /// Class WebClientBlueprintDefinitionSettings.
    /// Implements the <see cref="LaunchPadGeneratedObjectBlueprintDefinitionSettings" />
    /// </summary>
    /// <seealso cref="LaunchPadGeneratedObjectBlueprintDefinitionSettings" />
    [Serializable]
    [JsonObject(MemberSerialization.OptIn)]
    public partial class WebClientBlueprintDefinitionSettings :
        LaunchPadGeneratedObjectBlueprintDefinitionSettings
    {
        /// <summary>
        /// Brand configuration of the app. This could include name, icon, logo and/or theme color.
        /// </summary>
        /// <value>The brand.</value>
        [XmlElement]
        [JsonProperty]
        public LaunchPadBrand Brand { get; set; }

        /// <summary>
        /// Get generated base namespace from xml file.
        /// </summary>
        /// <value>The generated base namespace.</value>
        [XmlElement]
        public string GeneratedBaseNamespace { get; set; }

        /// <summary>
        /// Map configuration of the app.
        /// </summary>
        /// <value>The map.</value>
        [XmlElement]
        [JsonProperty]
        public LaunchPadMapConfiguration Map { get; set; }

        /// <summary>
        /// The top section of the side navigation bar
        /// </summary>
        /// <value>The top navigations.</value>
        [XmlElement]
        [JsonProperty]
        public IList<LaunchPadNavigationItem> TopNavigations { get; set; }

        /// <summary>
        /// The bottom section of the side navigation bar
        /// </summary>
        /// <value>The bottom navigations.</value>
        [XmlElement]
        [JsonProperty]
        public IList<LaunchPadNavigationItem> BottomNavigations { get; set; }

        /// <summary>
        /// The list of static web pages that belong to this module.
        /// </summary>
        /// <value>The static web pages.</value>
        [XmlElement]
        public virtual IDictionary<string, LaunchPadGeneratedStaticWebPage> StaticWebPages { get; set; }

        /// <summary>
        /// The dictionary of dynamic stylesheets (.less or .sass) that belong to this module.
        /// </summary>
        /// <value>The stylesheets.</value>
        [XmlElement]
        public virtual IDictionary<string, LaunchPadGeneratedStylesheet> Stylesheets { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="WebClientBlueprintDefinitionSettings"/> class.
        /// </summary>
        public WebClientBlueprintDefinitionSettings() : base()
        {
        }
    }
}
