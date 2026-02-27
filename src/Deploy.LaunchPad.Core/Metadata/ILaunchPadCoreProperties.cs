// ***********************************************************************
// Assembly         : Deploy.LaunchPad.Core
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 09-11-2023
// ***********************************************************************
// <copyright file="ILaunchPadCommonProperties.cs" company="Deploy Software Solutions, inc.">
//     2018-2024 Deploy Software Solutions, inc.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Deploy.LaunchPad.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Deploy.LaunchPad.Core.Metadata
{
    /// <summary>
    /// Defines the minimum properties LaunchPad expects to have for a Domain Entity or Value Object.
    /// Note these deliberately correspond 1:1 to many of the properties found in various ABP domain entity interfaces, which would also be inherited by implementing classes.
    /// </summary>
    public partial interface ILaunchPadCoreProperties : 
        ILaunchPadMinimalProperties,         
        IMayHaveTags,
        IMayHaveChecksumValue,
        IMayHaveCreatorUserId,
        IMustHaveCreationTime,
        IMayHaveDeleterUserId,
        IMayHaveDeletionTime,
        IMayHaveLastModifierUserId,
        IMayHaveModificationTime,
        IHavePassivable,
        IMayHaveSequenceNumber
    {
        
        /// <summary>
        /// The name of the deleting user
        /// </summary>
        /// <value>The name of the deleter user.</value>
        [MaxLength(256, ErrorMessageResourceName = "Validation_Name_256CharsOrLess", ErrorMessageResourceType = typeof(Deploy_LaunchPad_Core_Resources))]
        [DataObjectField(false)]
        [XmlAttribute]
        public string? DeleterUserName { get;}

        /// <summary>
        /// Used for preserving deletion status for a domain entity, obviously a Value Object can't be deleted.
        /// </summary>
        /// <value><c>true</c> if this instance is deleted; otherwise, <c>false</c>.</value>
        [DataObjectField(false)]
        [XmlElement]
        public bool IsDeleted { get; }

        /// <summary>
        /// The name of the creating user
        /// </summary>
        /// <value>The name of the creator user.</value>
        [MaxLength(256, ErrorMessageResourceName = "Validation_Name_256CharsOrLess", ErrorMessageResourceType = typeof(Deploy_LaunchPad_Core_Resources))]
        [DataObjectField(false)]
        [XmlAttribute]
        public string? CreatorUserName { get; }

        /// <summary>
        /// The name of the modifying user
        /// </summary>
        /// <value>The last name of the modifier user.</value>
        [MaxLength(256, ErrorMessageResourceName = "Validation_Name_256CharsOrLess", ErrorMessageResourceType = typeof(Deploy_LaunchPad_Core_Resources))]
        [DataObjectField(false)]
        [XmlAttribute]
        public string? LastModifierUserName { get;}

    }
}
