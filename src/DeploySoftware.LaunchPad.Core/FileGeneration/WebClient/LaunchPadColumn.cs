using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace DeploySoftware.LaunchPad.Core.FileGeneration
{
    /// <summary>
    /// Represents a column on a datatable
    /// </summary>  
    [Serializable]
    [XmlRoot(ElementName = "Column")]
    public partial class LaunchPadColumn : LaunchPadWebClientObjectBase
    {
        /// <summary>
        /// Whether or not this column is sortable. We allow both ascending and descending.
        /// </summary>
        [XmlAttribute("sortable")]
        public bool Sortable { get; set; }

        /// <summary>
        /// Represents the domain entity field to display as the data in this column.
        /// </summary>
        [XmlAttribute("dataField")]
        public string DataField { get; set; }

        /// <summary>
        /// Type of data to apply field format in the client
        /// </summary>
        [XmlAttribute("dataType")]
        public string DataType { get; set; }

        /// <summary>
        /// Action buttons appears on all rows of this column
        /// </summary>
        public IDictionary<string, LaunchPadAction> Actions { get; set; }

        public LaunchPadColumn() : base()
        {
            Sortable = false;
            DataField = string.Empty;
            DataType = string.Empty;
            var comparer = StringComparer.OrdinalIgnoreCase;
            Actions = new Dictionary<string, LaunchPadAction>(comparer);
        }
    }
}
