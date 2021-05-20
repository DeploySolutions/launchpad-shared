using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace DeploySoftware.LaunchPad.Core.FileGeneration
{
    /// <summary>
    /// Represents a input field in the form
    /// </summary>  
    [Serializable]
    public partial class LaunchPadInputField : LaunchPadWebClientObjectBase
    {
        /// <summary>
        /// Represents the domain entity field to display/save the data in this field.
        /// </summary>
        [JsonProperty("dataField")]
        public string DataField { get; set; }

        /// <summary>
        /// Type of data to apply field format in the client UI
        /// </summary>
        [JsonProperty("dataType")]
        public string DataType { get; set; }

        public LaunchPadInputField() : base()
        {
            DataField = string.Empty;
            DataType = string.Empty;
        }
    }
}
