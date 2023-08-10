using Deploy.LaunchPad.Core.Util;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Deploy.LaunchPad.FileGeneration.Structure
{
    /// <summary>
    /// Represents a input field in the form
    /// </summary>  
    [Serializable]
    public partial class LaunchPadWebItem : LaunchPadWebClientObjectBase
    {

        /// <summary>
        /// Type of this form item. Types can be "group", "view", "input", "textarea" or "button".
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
        /// local/relative or external/absolute endpoint for the search (lookup)
        /// </summary>
        [JsonProperty("endpoint")]
        public string Endpoint { get; set; }

        /// <summary>
        /// Number of column span for this field
        /// </summary>
        [JsonProperty("span")]
        public int Span { get; set; } = 1;

        /// <summary>
        /// Component for custom fields
        /// </summary>
        [JsonProperty("component")]
        [JsonConverter(typeof(JavaScriptObjectJsonConverter))]
        public string Component { get; set; }
        public string ComponentPath { get; set; }

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
        /// Possible enumurations for dropdown.
        /// </summary>
        [JsonProperty("enums")]
        public dynamic Enums { get; set; }

        /// <summary>
        /// Do we use dynamic enum(dropdown) values from the server, or static hardcoded array
        /// </summary>
        [JsonProperty("dynamicEnums")]
        public bool DynamicEnums { get; set; }

        /// <summary>
        /// The field to use as dynamic enums
        /// </summary>
        [JsonProperty("dynamicEnumsField")]
        public string DynamicEnumsField { get; set; }

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

        /// <summary>
        /// This field shows up in the form, but disabled. Do not allow the user to change. 
        /// </summary>
        [JsonProperty("readOnlyAlways")]
        public bool ReadOnlyAlways { get; set; }

        /// <summary>
        /// This field shows up on the form always, but disable the field on edit pages. 
        /// </summary>
        [JsonProperty("readOnlyEdit")]
        public bool ReadOnlyEdit { get; set; }

        /// <summary>
        /// domain entity for form item.
        /// </summary>
        [JsonProperty("domainEntity")]
        public string DomainEntity { get; set; }

        /// <summary>
        /// domain entity ID field for form item.
        /// </summary>
        [JsonProperty("domainEntityIdField")]
        public string DomainEntityIdField { get; set; }

        /// <summary>
        /// Rows can be nested within an item, if this item is a type of container, such as group
        /// </summary>
        [JsonProperty("rows", NullValueHandling = NullValueHandling.Ignore)]
        public IList<LaunchPadRow> Rows { get; set; }

        /// <summary>
        /// List of columns we want to display on the lookup modal table
        /// </summary>
        [JsonProperty("displayFields")]
        public IList<LaunchPadColumn> DisplayFields { get; set; }

        public LaunchPadWebItem() : base()
        {
            Id = null;
        }

        /// <summary>
        /// Serialization constructor used for deserialization
        /// </summary>
        /// <param name="info">The serialization info</param>
        /// <param name="context">The context of the stream</param>
        protected LaunchPadWebItem(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            ItemType = info.GetString("ItemType");
            DataField = info.GetString("DataField");
            DataType = info.GetString("DataType");
            Span = info.GetInt32("Span");
            Component = info.GetString("Component");
            ComponentPath = info.GetString("ComponentPath");
            LatDataField = info.GetString("LatDataField");
            LonDataField = info.GetString("LonDataField");
            StreetDataField = info.GetString("StreetDataField");
            CityDataField = info.GetString("CityDataField");
            ProvinceDataField = info.GetString("ProvinceDataField");

            PostalCodeDataField = info.GetString("PostalCodeDataField");
            ButtonType = info.GetString("ButtonType");
            Required = info.GetBoolean("Required");
            DomainEntity = info.GetString("DomainEntity");
            DomainEntityIdField = info.GetString("DomainEntityIdField");
            ProvinceDataField = info.GetString("ProvinceDataField");
            Rows = (IList<LaunchPadRow>)info.GetValue("Rows", typeof(List<LaunchPadRow>));
            DisplayFields = (IList<LaunchPadColumn>)info.GetValue("DisplayFields", typeof(List<LaunchPadColumn>));

        }

        /// <summary>
        /// The method required for implementing ISerializable
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue("ItemType", ItemType);
            info.AddValue("DataField", DataField);
            info.AddValue("DataType", DataType);
            info.AddValue("Span", Span);
            info.AddValue("Component", Component);
            info.AddValue("ComponentPath", ComponentPath);
            info.AddValue("LatDataField", LatDataField);
            info.AddValue("LonDataField", LonDataField);
            info.AddValue("StreetDataField", StreetDataField);
            info.AddValue("CityDataField", CityDataField);
            info.AddValue("StreetDataField", StreetDataField);
            info.AddValue("ProvinceDataField", ProvinceDataField);
            info.AddValue("PostalCodeDataField", PostalCodeDataField);
            info.AddValue("ButtonType", ButtonType);
            info.AddValue("Required", Required);
            info.AddValue("DomainEntity", DomainEntity);
            info.AddValue("DomainEntityIdField", DomainEntityIdField);
            info.AddValue("Rows", Rows);
            info.AddValue("DisplayFields", DisplayFields);
            info.AddValue("Enums", Enums);
        }
    }
}
