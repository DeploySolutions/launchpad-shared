using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Deploy.LaunchPad.FileGeneration.Structure
{
    /// <summary>
    /// Represents a form with multiple input fields and action buttons.
    /// </summary>  
    [Serializable]
    public partial class LaunchPadWebForm : LaunchPadWebClientObjectBase
    {
        /// <summary>
        /// Domain entity representing the data in the form
        /// </summary>
        [JsonProperty("domainEntity")]
        public string DomainEntity { get; set; }

        /// <summary>
        /// Rows included in this form
        /// </summary>
        [JsonProperty("rows")]
        public IList<LaunchPadRow> Rows { get; set; }

        public LaunchPadWebForm() : base()
        {
            Rows = new List<LaunchPadRow>();
        }
    }
}
