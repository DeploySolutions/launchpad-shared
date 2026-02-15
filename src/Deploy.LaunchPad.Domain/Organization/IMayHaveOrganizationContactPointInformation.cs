using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Deploy.LaunchPad.Domain.Organization
{
    public partial interface IMayHaveOrganizationContactPointInformation
    {
        /// <summary>
        /// Gets or sets the contact point (if any)
        /// </summary>
        /// <value>Gets or sets the contact point (if any)</value>
        [DataObjectField(false)]
        [XmlAttribute]
        public OrganizationContactPoint? ContactPoint { get; set; }
    }
}
