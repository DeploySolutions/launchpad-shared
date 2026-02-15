// ***********************************************************************
// Assembly         : Deploy.LaunchPad.Core
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 10-27-2023
// ***********************************************************************
// <copyright file="ILaunchPadAggregateChildProperties.cs" company="Deploy Software Solutions, inc.">
//     2018-2024 Deploy Software Solutions, inc.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Serialization;

namespace Deploy.LaunchPad.Domain.Metadata
{
    /// <summary>
    /// Defines the minimum properties LaunchPad expects to have for a child entity of an Aggregate Root.
    /// </summary>
    /// <typeparam name="TIdType">The type of the t identifier type.</typeparam>
    public partial interface ILaunchPadAggregateChildProperties<TIdType> : ILaunchPadDomainEntityProperties<TIdType>
    {
        /// <summary>
        /// The fully qualified type name of the parent Aggregate Root
        /// </summary>
        /// <value>The type of the parent fully qualified.</value>
        [DataObjectField(false)]
        [XmlAttribute]
        public string ParentFullyQualifiedType { get; }


    }
}
