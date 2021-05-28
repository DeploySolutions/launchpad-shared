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
    public partial class LaunchPadWebItem : LaunchPadWebClientObjectBase
    {

        /// <summary>
        /// Type of this form item. Types can be "view", "input", "textarea" or "button".
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
        /// Specific data field for composit items with latitude.
        /// </summary>
        [JsonProperty("latDataField")]
        public string LatDataField { get; set; }

        /// <summary>
        /// Specific data field for composit items with logitude.
        /// </summary>
        [JsonProperty("lonDataField")]
        public string LonDataField { get; set; }

        /// <summary>
        /// Specific data fields for composit items with address field.
        /// </summary>
        [JsonProperty("streetDataField")]
        public string StreetDataField { get; set; }
        [JsonProperty("cityDataField")]
        public string CityDataField { get; set; }
        [JsonProperty("provinceDataField")]
        public string ProvinceDataField { get; set; }
        [JsonProperty("postalCodeDataField")]
        public string PostalCodeDataField { get; set; }

        /// <summary>
        /// Type of this button. Types can be "reset" or "submit".
        /// </summary>
        [JsonProperty("buttonType")]
        public string ButtonType { get; set; }

        /// <summary>
        /// Tells whether this field is required, mostly used in form item, but can be used to indicate other type of item too.
        /// </summary>
        [JsonProperty("required")]
        public bool Required { get; set; }

        public LaunchPadWebItem() : base()
        {
            Id = null;
        }
    }
}
