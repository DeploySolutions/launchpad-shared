// ***********************************************************************
// Assembly         : Deploy.LaunchPad.Core
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 10-27-2023
// ***********************************************************************
// <copyright file="ILaunchPadDomainEntityProperties.cs" company="Deploy Software Solutions, inc.">
//     2018-2023 Deploy Software Solutions, inc.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Serialization;

namespace Deploy.LaunchPad.Core.Domain.Model
{
    /// <summary>
    /// Defines the minimum properties LaunchPad expects to have for a web-addressable Domain Entity or Value Object.
    /// </summary>
    /// <typeparam name="TIdType">The type of the t identifier type.</typeparam>
    public partial interface IWebAddressableProperties: ILaunchPadCommonProperties
    {
        // <summary>
        /// If this object is has a unique "slug" that can be used in a url to identify the object in an easy-to-read form.
        /// </summary>
        /// <value>The slug of the entity.</value>
        [DataObjectField(false)]
        [XmlAttribute]
        public string? Slug { get; }

        /// <summary>
        /// If this object does not have an abbreviation this will be null.
        /// </summary>
        /// <value>The abbreviation of the entity.</value>
        [DataObjectField(false)]
        [XmlAttribute]
        public string? Abbreviation { get; }


        /// <summary>
        /// The external ID stored in a client system (if any). Can be any type on client system, but retained here as text.
        /// </summary>
        /// <value>The external identifier.</value>
        [MaxLength(36, ErrorMessageResourceName = "Validation_ExternalId_36CharsOrLess", ErrorMessageResourceType = typeof(Deploy_LaunchPad_Core_Resources))]
        [DataObjectField(false)]
        [XmlAttribute]
        public string? ExternalId { get; }


    }
}
