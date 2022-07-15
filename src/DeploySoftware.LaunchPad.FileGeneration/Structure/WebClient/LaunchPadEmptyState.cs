using DeploySoftware.LaunchPad.Core.Util;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace DeploySoftware.LaunchPad.FileGeneration.Structure
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
        /// CSV Import action for non-readonly domain entiry
        /// </summary>
        [JsonProperty("import")]
        public LaunchPadAction Import { get; set; }

        public LaunchPadEmptyState() : base()
        {
        }
    }
}
