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
    public partial class LaunchPadFormItem : LaunchPadWebClientObjectBase
    {

        /// <summary>
        /// Type of this form item. Types can be "input" or "button".
        /// </summary>
        [JsonProperty("itemType")]
        public string ItemType { get; set; }

        /// <summary>
        /// Represents the domain entity field to display/save the data in this input field.
        /// </summary>
        [JsonProperty("dataField")]
        public string DataField { get; set; }

        /// <summary>
        /// Type of data to apply input field format in the client UI
        /// </summary>
        [JsonProperty("dataType")]
        public string DataType { get; set; }

        /// <summary>
        /// Type of this button. Types can be "reset" or "submit".
        /// </summary>
        [JsonProperty("buttonType")]
        public string ButtonType { get; set; }

        public LaunchPadFormItem() : base()
        {
            Id = null;
        }
    }
}
