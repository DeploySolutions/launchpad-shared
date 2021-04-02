using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace DeploySoftware.LaunchPad.Core.FileGeneration
{
    /// <summary>
    /// Represents a the branding of the application
    /// </summary>  
    [Serializable]
    [XmlRoot(ElementName = "Brand")]
    public partial class LaunchPadBrand : LaunchPadWebClientObjectBase
    {
        /// <summary>
        /// Logo image using in various places in the app, for example the login page. Suggested not larger than 512px x 512px. Supported formats are JPG, PNG and SVG.
        /// TODO: add a validation for file types
        /// </summary>
        [XmlAttribute("logo")]
        public string Logo { get; set; }

        /// <summary>
        /// Small icon showing on the web browser tab. Suggested dimension 32x32. Supported formats are JPG, PNG and SVG.
        /// TODO: add a validation for file types
        /// </summary>
        [XmlAttribute("favicon")]
        public string Favicon { get; set; }


        public LaunchPadBrand() : base()
        {
            Logo = string.Empty;
            Favicon = string.Empty;
        }
    }
}
