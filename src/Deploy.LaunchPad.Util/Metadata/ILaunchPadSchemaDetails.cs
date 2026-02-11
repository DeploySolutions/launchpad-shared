// ***********************************************************************
// Assembly         : Deploy.LaunchPad.Core.Abp
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 01-08-2023
// ***********************************************************************
// <copyright file="ILaunchPadSchemaDetails.cs" company="Deploy Software Solutions, inc.">
//     2018-2024 Deploy Software Solutions, inc.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Deploy.LaunchPad.Util.Elements;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;

namespace Deploy.LaunchPad.Util.Metadata
{
    /// <summary>
    /// Interface ILaunchPadSchemaDetails
    /// </summary>
    public partial interface ILaunchPadSchemaDetails<TSchemaFormat> : ILaunchPadObject
    {

        /// <summary>
        /// The value of this schema in a particular format (Schema.Org json-ld, XmlSchemaSet, POJO, string, etc)
        /// </summary>
        /// <value>The definition of this schema.</value>
        [DataObjectField(false)]
        [XmlAttribute]
        public TSchemaFormat Value { get; set; }

        /// <summary>
        /// The version of this schema
        /// </summary>
        /// <value>The version.</value>
        [DataObjectField(false)]
        [XmlAttribute]
        public string Version { get; set; }

    }
}
