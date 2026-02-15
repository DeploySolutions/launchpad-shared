using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Text.Json.Serialization;
using System.Globalization;

namespace Deploy.LaunchPad.Domain.Organization
{
    [Serializable]
    public partial class OrganizationContactPoint : IOrganizationContactPoint
    {
        /// <summary>
        /// The name of this contact point
        /// </summary>
        /// <value>The name.</value>
        [Required]
        [DataObjectField(false)]
        [XmlAttribute]
        public virtual string Name { get; }

        /// <summary>
        /// A person or organization can have different contact points, for different purposes. For example, a sales contact point, a PR contact point and so on. This property is used to specify the kind of contact point.
        /// </summary>
        [JsonPropertyName("contactType")]
        public virtual string ContactType { get; set; }

        /// <summary>
        /// Email address.
        /// </summary>
        [JsonPropertyName("email")]
        public virtual List<string> Email { get; set; } = new List<string>();

        /// <summary>
        /// The fax number.
        /// </summary>
        [JsonPropertyName("faxNumber")]
        public virtual string? FaxNumber { get; set; }

        /// <summary>
        /// The telephone number.
        /// </summary>
        [JsonPropertyName("telephone")]
        public virtual List<string> Telephone { get; set; } = new List<string>();

        /// <summary>
        /// Available languages/cultures for the contact point; for example "en-CA", "es-ES" or "fr".
        /// </summary>
        [JsonPropertyName("availableLanguages")]
        public virtual List<string> AvailableLanguages { get; set; } = new List<string>();

        public OrganizationContactPoint()
        {
        }

    }
}
