// ***********************************************************************
// Assembly         : Deploy.LaunchPad.Core.Abp
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 01-08-2023
// ***********************************************************************
// <copyright file="ISchemaDetails.cs" company="Deploy Software Solutions, inc.">
//     2018-2024 Deploy Software Solutions, inc.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Deploy.LaunchPad.Core.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;

namespace Deploy.LaunchPad.Core.Abp.Domain
{
    /// <summary>
    /// Interface ISchemaDetails
    /// </summary>
    public partial interface ISchemaDetails
    {
        /// <summary>
        /// The name of this schema
        /// </summary>
        /// <value>The name.</value>
        [DataObjectField(false)]
        [XmlAttribute]
        public String Name { get; set; }

        /// <summary>
        /// The version of this schema
        /// </summary>
        /// <value>The version.</value>
        [DataObjectField(false)]
        [XmlAttribute]
        public String Version { get; set; }

        /// <summary>
        /// A short description for this entity
        /// </summary>
        /// <value>The description short.</value>
        [DataObjectField(false)]
        [XmlAttribute]
        public String DescriptionShort { get; set; }

        /// <summary>
        /// The full description for this entity
        /// </summary>
        /// <value>The description full.</value>
        [DataObjectField(false)]
        [XmlElement]
        public String DescriptionFull { get; set; }

        /// <summary>
        /// Each entity can have an open-ended set of tags applied to it, that help users find, markup, and display its information
        /// </summary>
        /// <value>The tags.</value>
        [DataObjectField(false)]
        [XmlAttribute]
        public IEnumerable<MetadataTag> Tags { get; set; }
    }
}
