using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Deploy.LaunchPad.FileGeneration.Structure
{
    /// <summary>
    /// Represents a row in a form or page.
    /// </summary>  
    [Serializable]
    public partial class LaunchPadRow : LaunchPadWebClientObjectBase
    {
        /// <summary>
        /// Form items (input fields or buttons) included in this form
        /// </summary>
        [JsonProperty("items")]
        public IList<LaunchPadWebItem> Items { get; set; }

        /// <summary>
        /// Number of row span
        /// </summary>
        [JsonProperty("span")]
        public int? Span { get; set; }

        public LaunchPadRow() : base()
        {
            Id = null;
            Name = null;
        }
    }
}
