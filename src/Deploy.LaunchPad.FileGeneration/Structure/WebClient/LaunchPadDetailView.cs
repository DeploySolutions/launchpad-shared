using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Deploy.LaunchPad.FileGeneration.Structure
{
    /// <summary>
    /// Represents a form with multiple input fields and action buttons.
    /// </summary>  
    [Serializable]
    public partial class LaunchPadDetailView : LaunchPadWebClientObjectBase
    {
        /// <summary>
        /// Domain entity representing the data in the view
        /// </summary>
        [JsonProperty("domainEntity")]
        public string DomainEntity { get; set; }

        /// <summary>
        /// Rows included in this view
        /// </summary>
        [JsonProperty("rows")]
        public IList<LaunchPadRow> Rows { get; set; }

        public LaunchPadDetailView() : base()
        {
            Rows = new List<LaunchPadRow>();
        }
    }
}
