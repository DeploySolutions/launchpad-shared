using Deploy.LaunchPad.Core.Elements;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Xml.Serialization;

namespace Deploy.LaunchPad.Core.Entities
{
    public partial interface ILaunchPadMinimalProperties
    {

        /// <summary>
        /// The name of this object
        /// </summary>
        /// <value>The name.</value>
        [Required]
        [DataObjectField(false)]
        [XmlAttribute]
        public ElementName Name { get; set; }

        /// <summary>
        /// The description of this object
        /// </summary>
        /// <value>The description.</value>
        [DataObjectField(false)]
        [XmlAttribute]
        public ElementDescription Description { get; set; }

    }
}
