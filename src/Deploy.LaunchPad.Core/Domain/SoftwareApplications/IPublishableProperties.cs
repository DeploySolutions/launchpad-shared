﻿// ***********************************************************************
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
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Xml.Serialization;

namespace Deploy.LaunchPad.Core.Domain.Model
{
    /// <summary>
    /// Defines the minimum properties LaunchPad expects to have for a publishable Domain Entity or Value Object.
    /// </summary>
    /// <typeparam name="TIdType">The type of the t identifier type.</typeparam>
    public partial interface IPublishableProperties: ILaunchPadCommonProperties
    {

        /// <summary>
        /// Is this entity published?
        /// </summary>        
        [DataObjectField(false)]
        [XmlAttribute]
        public bool IsPublished { get; set; }

        /// <summary>
        /// Which user published this entity?
        /// </summary>
        [DataObjectField(false)]
        [XmlAttribute]
        public long? PublisherUserId { get; set; }

        /// <summary>
        /// Publishing time of this entity.
        /// </summary>
        [DataObjectField(false)]
        [XmlAttribute]
        public  DateTime? PublishedTime { get; set; }


    }
}
