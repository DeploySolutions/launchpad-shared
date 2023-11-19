// ***********************************************************************
// Assembly         : Deploy.LaunchPad.FileGeneration
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 01-08-2023
// ***********************************************************************
// <copyright file="LaunchPadBrand.cs" company="Deploy Software Solutions, inc.">
//     2018-2023 Deploy Software Solutions, inc.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Newtonsoft.Json;
using System;
using System.Xml.Serialization;

namespace Deploy.LaunchPad.FileGeneration.Structure
{
    /// <summary>
    /// Represents a the branding of the application
    /// </summary>
    [Serializable]
    [XmlRoot(ElementName = "Brand")]
    [JsonObject(MemberSerialization.OptIn)]
    public partial class LaunchPadBrand : LaunchPadWebClientObjectBase
    {
        /// <summary>
        /// Logo image using in various places in the app, for example the login page. Suggested not larger than 512px x 512px. Supported formats are JPG, PNG and SVG.
        /// TODO: add a validation for file types
        /// </summary>
        /// <value>The logo.</value>
        [JsonProperty("logo")]
        public string Logo { get; set; }

        /// <summary>
        /// Small icon showing on the web browser tab. Suggested dimension 32x32. Supported formats are JPG, PNG and SVG.
        /// TODO: add a validation for file types
        /// </summary>
        /// <value>The favicon.</value>
        [JsonProperty("favicon")]
        public string Favicon { get; set; }

        /// <summary>
        /// Small icon showing on the web browser tab. Suggested dimension 32x32. Supported formats are JPG, PNG and SVG.
        /// TODO: add a validation for file types
        /// </summary>
        /// <value>The display name of the application.</value>
        [JsonProperty("appDisplayName")]
        public string AppDisplayName { get; set; }


        /// <summary>
        /// Initializes a new instance of the <see cref="LaunchPadBrand"/> class.
        /// </summary>
        public LaunchPadBrand() : base()
        {
            Logo = string.Empty;
            Favicon = string.Empty;
            AppDisplayName = string.Empty;
        }
    }
}
