// ***********************************************************************
// Assembly         : Deploy.LaunchPad.FileGeneration
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 04-21-2023
// ***********************************************************************
// <copyright file="LaunchPadColumn.cs" company="Deploy Software Solutions, inc.">
//     2018-2023 Deploy Software Solutions, inc.
// </copyright>
// <summary></summary>
// ***********************************************************************
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
        /// <value><c>true</c> if sortable; otherwise, <c>false</c>.</value>
        [JsonProperty("sortable")]
        public bool Sortable { get; set; }

        /// <summary>
        /// Represents the domain entity field to display as the data in this column.
        /// </summary>
        /// <value>The data field.</value>
        [JsonProperty("dataField")]
        public string DataField { get; set; }

        /// <summary>
        /// Type of data to apply field format in the client
        /// </summary>
        /// <value>The type of the data.</value>
        [JsonProperty("dataType")]
        public string DataType { get; set; }

        /// <summary>
        /// Action buttons appears on all rows of this column
        /// </summary>
        /// <value>The actions.</value>
        [JsonProperty("actions")]
        public IList<LaunchPadAction> Actions { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="LaunchPadColumn"/> class.
        /// </summary>
        public LaunchPadColumn() : base()
        {
            Sortable = false;
            DataField = string.Empty;
            DataType = string.Empty;
            Actions = new List<LaunchPadAction>();  
        }
    }
}
