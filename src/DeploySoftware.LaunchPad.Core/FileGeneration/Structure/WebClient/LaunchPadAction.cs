﻿using Newtonsoft.Json;
using System;
using System.Xml.Serialization;

namespace DeploySoftware.LaunchPad.Core.FileGeneration
{
    /// <summary>
    /// Represents an action buttons on the UI
    /// </summary>  
    [Serializable]
    public partial class LaunchPadAction : LaunchPadWebClientObjectBase
    {
        /// <summary>
        /// Route action. Takes the user to this route when they clicks on the button.
        /// </summary>
        [JsonProperty("route")]
        public string Route { get; set; }

        /// <summary>
        /// At the moment we limit the icon to our set of available SVG. We may be able to support other format of icons later.
        /// </summary>
        [JsonProperty("icon")]
        public string Icon { get; set; }

        public LaunchPadAction() : base()
        {
            Route = string.Empty;
            Icon = string.Empty;
        }
    }
}
