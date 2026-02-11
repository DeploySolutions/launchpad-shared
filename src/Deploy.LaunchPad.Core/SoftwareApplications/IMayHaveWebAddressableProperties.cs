// ***********************************************************************
// Assembly         : Deploy.LaunchPad.Core
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 10-27-2023
// ***********************************************************************
// <copyright file="ILaunchPadDomainEntityProperties.cs" company="Deploy Software Solutions, inc.">
//     2018-2024 Deploy Software Solutions, inc.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Deploy.LaunchPad.Domain.Content;
using Deploy.LaunchPad.Domain.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Serialization;

namespace Deploy.LaunchPad.Domain.SoftwareApplications
{
    /// <summary>
    /// Defines the minimum properties LaunchPad expects to have for a web-addressable Domain Entity or Value Object.
    /// </summary>
    /// <typeparam name="TIdType">The type of the t identifier type.</typeparam>
    public partial interface IMayHaveWebAddressableProperties: ILaunchPadCoreProperties, IMayHaveASlug
    {

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
