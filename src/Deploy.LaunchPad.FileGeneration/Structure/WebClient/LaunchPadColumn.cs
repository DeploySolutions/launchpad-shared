using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Deploy.LaunchPad.FileGeneration.Structure
{
    /// <summary>
    /// Represents a column on a datatable
    /// </summary>  
    [Serializable]
    public partial class LaunchPadColumn : LaunchPadWebClientObjectBase
    {
        /// <summary>
        /// Whether or not this column is sortable. We allow both ascending and descending.
        /// </summary>
        [JsonProperty("sortable")]
        public bool Sortable { get; set; }

        /// <summary>
        /// Represents the domain entity field to display as the data in this column.
        /// </summary>
        [JsonProperty("dataField")]
        public string DataField { get; set; }

        /// <summary>
        /// Type of data to apply field format in the client
        /// </summary>
        [JsonProperty("dataType")]
        public string DataType { get; set; }

        /// <summary>
        /// Action buttons appears on all rows of this column
        /// </summary>
        [JsonProperty("actions")]
        public IList<LaunchPadAction> Actions { get; set; }

        public LaunchPadColumn() : base()
        {
            Sortable = false;
            DataField = string.Empty;
            DataType = string.Empty;
            Actions = new List<LaunchPadAction>();  
        }
    }
}
