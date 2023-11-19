// ***********************************************************************
// Assembly         : Deploy.LaunchPad.FileGeneration
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 10-01-2023
// ***********************************************************************
// <copyright file="LaunchPadCascadeEnum.cs" company="Deploy Software Solutions, inc.">
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
    public partial class LaunchPadCascadeEnum : LaunchPadWebClientObjectBase
    {
        /// <summary>
        /// Value of the dropdown
        /// </summary>
        /// <value>The value.</value>
        [JsonProperty("value")]
        public string Value { get; set; }

        /// <summary>
        /// Represents the domain entity field to display as the data in this column.
        /// </summary>
        /// <value>The data field.</value>
        [JsonProperty("dataField")]
        public string DataField { get; set; }

        /// <summary>
        /// Action buttons appears on all rows of this column
        /// </summary>
        /// <value>The children.</value>
        [JsonProperty("children")]
        public IList<LaunchPadCascadeEnum> Children { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="LaunchPadCascadeEnum"/> class.
        /// </summary>
        public LaunchPadCascadeEnum() : base()
        {
            Value = string.Empty;
            DataField = string.Empty;
            Children = new List<LaunchPadCascadeEnum>();  
        }
    }
}
