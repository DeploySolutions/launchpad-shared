using Deploy.LaunchPad.Core.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;

namespace Deploy.LaunchPad.Core.Abp.Domain
{
    public interface ISchemaDetails
    {
        /// <summary>
        /// The name of this schema
        /// </summary>
        [DataObjectField(false)]
        [XmlAttribute]
        public String Name { get; set; }

        /// <summary>
        /// The version of this schema
        /// </summary>
        [DataObjectField(false)]
        [XmlAttribute]
        public String Version { get; set; }

        /// <summary>
        /// A short description for this entity
        /// </summary>
        [DataObjectField(false)]
        [XmlAttribute]
        public String DescriptionShort { get; set; }

        /// <summary>
        /// The full description for this entity
        /// </summary>
        [DataObjectField(false)]
        [XmlElement]
        public String DescriptionFull { get; set; }

        /// <summary>
        /// Each entity can have an open-ended set of tags applied to it, that help users find, markup, and display its information
        /// </summary>
        [DataObjectField(false)]
        [XmlAttribute]
        public IEnumerable<MetadataTag> Tags { get; set; }
    }
}
