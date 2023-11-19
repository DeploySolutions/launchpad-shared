// ***********************************************************************
// Assembly         : Deploy.LaunchPad.FileGeneration
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 05-07-2023
// ***********************************************************************
// <copyright file="LaunchPadDataTable.cs" company="Deploy Software Solutions, inc.">
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
    /// Class LaunchPadDataTable.
    /// Implements the <see cref="Deploy.LaunchPad.FileGeneration.Structure.LaunchPadWebClientObjectBase" />
    /// </summary>
    /// <seealso cref="Deploy.LaunchPad.FileGeneration.Structure.LaunchPadWebClientObjectBase" />
    /// <font color="red">Badly formed XML comment.</font>
    [Serializable]
    public partial class LaunchPadDataTable : LaunchPadWebClientObjectBase
    {
        /// <summary>
        /// Domain entity representing the data listed in the table
        /// </summary>
        /// <value>The domain entity.</value>
        [JsonProperty("domainEntity")]
        public string DomainEntity { get; set; }

        /// <summary>
        /// Route to follow when the user clicks on each row of data
        /// </summary>
        /// <value><c>true</c> if selectable; otherwise, <c>false</c>.</value>
        [JsonProperty("selectable")]
        public bool Selectable { get; set; }

        /// <summary>
        /// Route to follow when the user clicks on each row of data
        /// </summary>
        /// <value>The route.</value>
        [JsonProperty("route")]
        public string Route { get; set; }

        /// <summary>
        /// Primary filters displaying at the top of the datatable
        /// </summary>
        /// <value>The filters.</value>
        [JsonProperty("filters")]
        public IList<LaunchPadFilter> Filters { get; set; }

        /// <summary>
        /// Secondary filters displayed when the user clicks on "More Filter".
        /// </summary>
        /// <value>The more filters.</value>
        [JsonProperty("moreFilters")]
        public IList<LaunchPadFilter> MoreFilters { get; set; }

        /// <summary>
        /// Action buttons appears at the top of the data table
        /// </summary>
        /// <value>The actions.</value>
        [JsonProperty("actions")]
        public IList<LaunchPadAction> Actions { get; set; }

        /// <summary>
        /// Action buttons appears at the top of the data table, only applies to the selected rows
        /// </summary>
        /// <value>The selectable actions.</value>
        [JsonProperty("selectableActions")]
        public IList<LaunchPadAction> SelectableActions { get; set; }

        /// <summary>
        /// List of columns on the datatable
        /// </summary>
        /// <value>The columns.</value>
        [JsonProperty("columns")]
        public IList<LaunchPadColumn> Columns { get; set; }

        /// <summary>
        /// Domain entity representing the data listed in the table
        /// </summary>
        /// <value>The empty state.</value>
        [JsonProperty("emptyState")]
        public LaunchPadEmptyState EmptyState { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="LaunchPadDataTable"/> class.
        /// </summary>
        public LaunchPadDataTable() : base()
        {
            DomainEntity = String.Empty;
            Filters = new List<LaunchPadFilter>();
            MoreFilters = new List<LaunchPadFilter>();
            Actions = new List<LaunchPadAction>();
            Columns = new List<LaunchPadColumn>();
        }
    }
}
