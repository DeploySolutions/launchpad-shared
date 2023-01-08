using Newtonsoft.Json;
using System;

namespace Deploy.LaunchPad.FileGeneration.Structure
{
    /// <summary>
    /// Represents an action buttons on the UI
    /// </summary>  
    [Serializable]
    public partial class LaunchPadAction : LaunchPadWebClientObjectBase
    {
        /// <summary>
        /// We support `edit` and `delete` as a type at the moment
        /// </summary>
        [JsonProperty("type")]
        public String Type { get; set; }
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

        /// <summary>
        /// Custom method called when they clicks on the button.
        /// </summary>
        [JsonProperty("function")]
        public string Function { get; set; }

        public LaunchPadAction() : base()
        {
            Route = string.Empty;
            Icon = string.Empty;
        }
    }
}
