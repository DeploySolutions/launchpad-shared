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
using Deploy.LaunchPad.Core.Metadata;
using Schema.NET;
using System;
using System.ComponentModel;
using System.Xml.Serialization;

namespace Deploy.LaunchPad.Core.Domain.Entities
{
    /// <summary>
    /// Defines the minimum properties LaunchPad expects to have for a Domain Entity Object.
    /// Note these deliberately correspond 1:1 to many of the properties found in various ABP domain entity interfaces, which would also be inherited by implementing classes.
    /// </summary>
    /// <typeparam name="TPrimaryKey">The type of the primary key.</typeparam>
    public partial interface IDomainEntityProperties<TPrimaryKey> : ILaunchPadEntityBaseProperties<TPrimaryKey>,
        ILaunchPadCoreProperties,
        IMustHaveElementDescription,
        IMustHaveCulture, 
        IMayHaveTranslationFromId<TPrimaryKey>
    {
        /// <summary>
        /// If this object is a regular domain entity, an aggregate root, or an aggregate child
        /// </summary>
        /// <value>The type of the entity.</value>
        [DataObjectField(false)]
        [XmlAttribute]
        public EntityType EntityType { get; }

    }
}
