// ***********************************************************************
// Assembly         : Deploy.LaunchPad.FileGeneration
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 11-07-2023
// ***********************************************************************
// <copyright file="LaunchPadWebItem.cs" company="Deploy Software Solutions, inc.">
//     2018-2023 Deploy Software Solutions, inc.
// </copyright>
// <summary></summary>
// ***********************************************************************
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
        /// <value>The type of the item.</value>
        [JsonProperty("itemType")]
        public string ItemType { get; set; }

        /// <summary>
        /// Represents the domain entity field to display/save the data in this input field.
        /// </summary>
        /// <value>The data field.</value>
        [JsonProperty("dataField")]
        public string DataField { get; set; }

        /// <summary>
        /// Type of data to apply input field format in the client UI
        /// </summary>
        /// <value>The type of the data.</value>
        [JsonProperty("dataType")]
        public string DataType { get; set; }

        /// <summary>
        /// local/relative or external/absolute endpoint for the search (lookup)
        /// </summary>
        /// <value>The endpoint.</value>
        [JsonProperty("endpoint")]
        public string Endpoint { get; set; }

        /// <summary>
        /// Number of column span for this field
        /// </summary>
        /// <value>The span.</value>
        [JsonProperty("span")]
        public int Span { get; set; } = 1;

        /// <summary>
        /// Component for custom fields
        /// </summary>
        /// <value>The component.</value>
        [JsonProperty("component")]
        [JsonConverter(typeof(JavaScriptObjectJsonConverter))]
        public string Component { get; set; }
        /// <summary>
        /// Gets or sets the component path.
        /// </summary>
        /// <value>The component path.</value>
        public string ComponentPath { get; set; }

        /// <summary>
        /// Specific data field for composit items with latitude.
        /// </summary>
        /// <value>The lat data field.</value>
        [JsonProperty("latDataField")]
        public string LatDataField { get; set; }

        /// <summary>
        /// Specific data field for composit items with logitude.
        /// </summary>
        /// <value>The lon data field.</value>
        [JsonProperty("lonDataField")]
        public string LonDataField { get; set; }

        /// <summary>
        /// Specific data fields for composit items with address field.
        /// </summary>
        /// <value>The street data field.</value>
        [JsonProperty("streetDataField")]
        public string StreetDataField { get; set; }
        /// <summary>
        /// Gets or sets the city data field.
        /// </summary>
        /// <value>The city data field.</value>
        [JsonProperty("cityDataField")]
        public string CityDataField { get; set; }
        /// <summary>
        /// Gets or sets the province data field.
        /// </summary>
        /// <value>The province data field.</value>
        [JsonProperty("provinceDataField")]
        public string ProvinceDataField { get; set; }
        /// <summary>
        /// Gets or sets the postal code data field.
        /// </summary>
        /// <value>The postal code data field.</value>
        [JsonProperty("postalCodeDataField")]
        public string PostalCodeDataField { get; set; }

        /// <summary>
        /// Possible enumurations for dropdown.
        /// </summary>
        /// <value>The enums.</value>
        [JsonProperty("enums")]
        public dynamic Enums { get; set; }

        /// <summary>
        /// Do we use dynamic enum(dropdown) values from the server, or static hardcoded array
        /// </summary>
        /// <value><c>true</c> if [dynamic enums]; otherwise, <c>false</c>.</value>
        [JsonProperty("dynamicEnums")]
        public bool DynamicEnums { get; set; }

        /// <summary>
        /// The field to use as dynamic enums
        /// </summary>
        /// <value>The dynamic enums field.</value>
        [JsonProperty("dynamicEnumsField")]
        public string DynamicEnumsField { get; set; }

        /// <summary>
        /// Type of this button. Types can be "reset" or "submit".
        /// </summary>
        /// <value>The type of the button.</value>
        [JsonProperty("buttonType")]
        public string ButtonType { get; set; }

        /// <summary>
        /// Tells whether this field is required, mostly used in form item, but can be used to indicate other type of item too.
        /// </summary>
        /// <value><c>true</c> if required; otherwise, <c>false</c>.</value>
        [JsonProperty("required")]
        public bool Required { get; set; }

        /// <summary>
        /// This field shows up in the form, but disabled. Do not allow the user to change.
        /// </summary>
        /// <value><c>true</c> if [read only always]; otherwise, <c>false</c>.</value>
        [JsonProperty("readOnlyAlways")]
        public bool ReadOnlyAlways { get; set; }

        /// <summary>
        /// This field shows up on the form always, but disable the field on edit pages.
        /// </summary>
        /// <value><c>true</c> if [read only edit]; otherwise, <c>false</c>.</value>
        [JsonProperty("readOnlyEdit")]
        public bool ReadOnlyEdit { get; set; }

        /// <summary>
        /// Only use for upload field. This specify if the uplaod item should be uploaded first, or the page submission should ahppen first. This is helpful if the upload process requires certain value based on the response of the submission.
        /// </summary>
        /// <value><c>true</c> if [upload before submit]; otherwise, <c>false</c>.</value>
        [JsonProperty("uploadBeforeSubmit")]
        public bool UploadBeforeSubmit { get; set; }

        /// <summary>
        /// domain entity for form item.
        /// </summary>
        /// <value>The domain entity.</value>
        [JsonProperty("domainEntity")]
        public string DomainEntity { get; set; }

        /// <summary>
        /// domain entity ID field for form item.
        /// </summary>
        /// <value>The domain entity identifier field.</value>
        [JsonProperty("domainEntityIdField")]
        public string DomainEntityIdField { get; set; }

        /// <summary>
        /// Rows can be nested within an item, if this item is a type of container, such as group
        /// </summary>
        /// <value>The rows.</value>
        [JsonProperty("rows", NullValueHandling = NullValueHandling.Ignore)]
        public IList<LaunchPadRow> Rows { get; set; }

        /// <summary>
        /// List of columns we want to display on the lookup modal table
        /// </summary>
        /// <value>The display fields.</value>
        [JsonProperty("displayFields")]
        public IList<LaunchPadColumn> DisplayFields { get; set; }

        /// <summary>
        /// In case the button is not of submit type, we have to specify where it goes next.
        /// </summary>
        /// <value>The next route.</value>
        [JsonProperty("nextRoute")]
        public string NextRoute { get; set; }

        /// <summary>
        /// The parameters to be passed to the next page.
        /// </summary>
        /// <value>The next route parameters.</value>
        [JsonProperty("nextRouteParams")]
        public Dictionary<string, string> NextRouteParams { get; set; }

        /// <summary>
        /// Cascade Enums represents a dropdown that depends on previous dropdown selected value
        /// </summary>
        /// <value>The cascade enums.</value>
        [JsonProperty("cascadeEnums")]
        public IList<LaunchPadCascadeEnum> CascadeEnums { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="LaunchPadWebItem"/> class.
        /// </summary>
        public LaunchPadWebItem() : base()
        {
            Id = Guid.Empty;
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
        /// <param name="info">The information.</param>
        /// <param name="context">The context.</param>
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
