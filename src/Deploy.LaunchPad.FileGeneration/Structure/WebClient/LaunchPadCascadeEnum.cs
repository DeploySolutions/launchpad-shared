using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Deploy.LaunchPad.FileGeneration.Structure
{
    /// <summary>
    /// Represents a column on a datatable
    /// </summary>  
    [Serializable]
    public partial class LaunchPadCascadeEnum : LaunchPadWebClientObjectBase
    {
        /// <summary>
        /// Value of the dropdown
        /// </summary>
        [JsonProperty("value")]
        public bool Value { get; set; }

        /// <summary>
        /// Represents the domain entity field to display as the data in this column.
        /// </summary>
        [JsonProperty("dataField")]
        public string DataField { get; set; }

        /// <summary>
        /// Action buttons appears on all rows of this column
        /// </summary>
        [JsonProperty("children")]
        public IList<LaunchPadCascadeEnum> Children { get; set; }

        public LaunchPadCascadeEnum() : base()
        {
            Value = false;
            DataField = string.Empty;
            Children = new List<LaunchPadCascadeEnum>();  
        }
    }
}
