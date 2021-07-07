using DeploySoftware.LaunchPad.Core.Util;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace DeploySoftware.LaunchPad.Core.FileGeneration
{
    /// <summary>
    /// Represents a state when a table is empty.
    /// </summary>  
    [Serializable]
    public partial class LaunchPadEmptyState : LaunchPadWebClientObjectBase
    {
        /// <summary>
        /// SVG icon displayed in the empty state UI design
        /// </summary>
        [JsonProperty("icon")]
        public string Icon { get; set; }

        /// <summary>
        /// Label of the button to redirect the user to create new data
        /// </summary>
        [JsonProperty("newButtonLabel")]
        [JsonConverter(typeof(LocalizedJsonConverter<string>))]
        public string NewButtonLabel { get; set; }

        public LaunchPadEmptyState() : base()
        {
        }
    }
}
