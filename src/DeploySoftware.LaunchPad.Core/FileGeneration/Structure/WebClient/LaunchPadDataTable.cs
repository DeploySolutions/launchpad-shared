using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace DeploySoftware.LaunchPad.Core.FileGeneration
{
    /// <summary>
    /// Represents a datatable presenting a list of items with sorting, filter and pagination functionality. It could be displayed in an area such as a <div> or it can take up the whole page.
    /// </summary>  
    [Serializable]
    public partial class LaunchPadDataTable : LaunchPadWebClientObjectBase
    {
        /// <summary>
        /// Domain entity representing the data listed in the table
        /// </summary>
        [JsonProperty("domainEntity")]
        public string DomainEntity { get; set; }

        /// <summary>
        /// Route to follow when the user clicks on each row of data
        /// </summary>
        [JsonProperty("rowRoute")]
        public string RowRoute { get; set; }

        /// <summary>
        /// Primary filters displaying at the top of the datatable
        /// </summary>
        [JsonProperty("filters")]
        public IList<LaunchPadFilter> Filters { get; set; }

        /// <summary>
        /// Secondary filters displayed when the user clicks on "More Filter".
        /// </summary>
        [JsonProperty("moreFilters")]
        public IList<LaunchPadFilter> MoreFilters { get; set; }

        /// <summary>
        /// Action buttons appears at the top of the data table
        /// </summary>
        [JsonProperty("actions")]
        public IList<LaunchPadAction> Actions { get; set; }

        /// <summary>
        /// List of columns on the datatable
        /// </summary>
        [JsonProperty("columns")]
        public IList<LaunchPadColumn> Columns { get; set; }

        /// <summary>
        /// Domain entity representing the data listed in the table
        /// </summary>
        [JsonProperty("emptyState")]
        public LaunchPadEmptyState EmptyState { get; set; }

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
