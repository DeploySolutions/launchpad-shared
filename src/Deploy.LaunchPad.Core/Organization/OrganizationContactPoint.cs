using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Schema.NET;
using System.Text.Json.Serialization;
using System.Globalization;

namespace Deploy.LaunchPad.Core.Organization
{
    [Serializable]
    public partial class OrganizationContactPoint
    {
        /// <summary>
        /// The name of this contact point
        /// </summary>
        /// <value>The name.</value>
        [Required]
        [DataObjectField(false)]
        [XmlAttribute]
        public ElementNameLight Name { get; }

        /// <summary>
        /// The culture of this object
        /// </summary>
        /// <value>The culture.</value>
        [Required]
        [MaxLength(5, ErrorMessageResourceName = "Validation_Culture_5CharsOrLess", ErrorMessageResourceType = typeof(Deploy_LaunchPad_Core_Resources))]
        [DataObjectField(true)]
        [XmlAttribute]

        public string Culture { get; }

        /// <summary>
        /// A person or organization can have different contact points, for different purposes. For example, a sales contact point, a PR contact point and so on. This property is used to specify the kind of contact point.
        /// </summary>
        [JsonPropertyName("contactType")]
        [JsonConverter(typeof(ValuesJsonConverter))]
        public string ContactType { get; set; }

        /// <summary>
        /// Email address.
        /// </summary>
        [JsonPropertyName("email")]
        [JsonConverter(typeof(ValuesJsonConverter))]
        public List<string> Email { get; set; } = new List<string>();

        /// <summary>
        /// The fax number.
        /// </summary>
        [JsonPropertyName("faxNumber")]
        [JsonConverter(typeof(ValuesJsonConverter))]
        public string? FaxNumber { get; set; }

        /// <summary>
        /// The telephone number.
        /// </summary>
        [JsonPropertyName("telephone")]
        [JsonConverter(typeof(ValuesJsonConverter))]
        public List<string> Telephone { get; set; } = new List<string>();

        /// <summary>
        /// Available languages/cultures for the contact point; for example "en-CA", "es-ES" or "fr".
        /// </summary>
        [JsonPropertyName("email")]
        [JsonConverter(typeof(ValuesJsonConverter))]
        public List<CultureInfo> AvailableLanguages { get; set; } = new List<CultureInfo>();

        public OrganizationContactPoint()
        { 
        }

        public OrganizationContactPoint(Schema.NET.ContactPoint schemaDotOrgContactPoint)
        {

            // TODO: Convert schemaDotOrgContactPoint to OrganizationContactPoint properties
        }
    }
}
