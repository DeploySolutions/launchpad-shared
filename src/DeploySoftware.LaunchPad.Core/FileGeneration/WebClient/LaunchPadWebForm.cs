using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace DeploySoftware.LaunchPad.Core.FileGeneration
{
    /// <summary>
    /// Represents a form with multiple input fields and action buttons.
    /// </summary>  
    [Serializable]
    public partial class LaunchPadWebForm : LaunchPadWebClientObjectBase
    {
        /// <summary>
        /// Rows included in this form
        /// </summary>
        [JsonProperty("row")]
        public IList<LaunchPadRow> Rows { get; set; }

        public LaunchPadWebForm() : base()
        {
            Rows = new List<LaunchPadRow>();
        }
    }
}
