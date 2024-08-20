// ***********************************************************************
// Assembly         : Deploy.LaunchPad.Core
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 09-11-2023
// ***********************************************************************
// <copyright file="ILaunchPadCommonProperties.cs" company="Deploy Software Solutions, inc.">
//     2018-2023 Deploy Software Solutions, inc.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Deploy.LaunchPad.Core.Domain.Model
{
    /// <summary>
    /// Defines the minimum properties LaunchPad expects to have for a Domain Entity or Value Object.
    /// Note these deliberately correspond 1:1 to many of the properties found in various ABP domain entity interfaces, which would also be inherited by implementing classes.
    /// </summary>
    public partial interface ILaunchPadCommonProperties
    {

        /// <summary>
        /// The name of this object
        /// </summary>
        /// <value>The name.</value>
        [Required]
        [DataObjectField(false)]
        [XmlAttribute]
        public EntityName Name { get; }

        /// <summary>
        /// The description of this object
        /// </summary>
        /// <value>The description.</value>
        [Required]
        [DataObjectField(false)]
        [XmlAttribute]
        public EntityDescription Description { get; }

        /// <summary>
        /// The culture of this object
        /// </summary>
        /// <value>The culture.</value>
        [Required]
        [MaxLength(5, ErrorMessageResourceName = "Validation_Culture_5CharsOrLess", ErrorMessageResourceType = typeof(Deploy_LaunchPad_Core_Resources))]
        [DataObjectField(true)]
        [XmlAttribute]
        public string Culture { get; }

        /// <summary>
        /// The checksum for this  object, if any
        /// </summary>
        /// <value>The checksum.</value>
        [DataObjectField(false)]
        [XmlElement]
        public string? Checksum { get; }

        /// <summary>
        /// The sequence number for this value object, if any (for sorting and ordering purposes). Defaults to 0 if not set.
        /// </summary>
        /// <value>The seq number.</value>
        [DataObjectField(false)]
        [XmlElement]
        public int SeqNum { get; }

        /// <summary>
        /// Each value object can have an open-ended set of tags applied to it, that help users find, markup, and display its information
        /// </summary>
        /// <value>The tags.</value>
        [DataObjectField(false)]
        [XmlAttribute]
        public HashSet<MetadataTag> Tags { get; }

        
        /// <summary>
        /// Gets or sets a value indicating whether this instance is active.
        /// </summary>
        /// <value><c>true</c> if this instance is active; otherwise, <c>false</c>.</value>
        [DataObjectField(false)]
        [XmlAttribute]
        public bool IsActive { get; }

        /// <summary>
        /// Used for preserving deletion time for a domain entity, obviously a Value Object can't be deleted.
        /// </summary>
        /// <value>The deletion time.</value>
        [DataObjectField(false)]
        [XmlElement]
        public DateTime? DeletionTime { get; }

        /// <summary>
        /// The id of the user which deleted. Used for preserving information for a domain entity, obviously a Value Object can't be deleted.
        /// </summary>
        /// <value>The deleter user identifier.</value>
        [DataObjectField(false)]
        [XmlAttribute]
        [ForeignKey(nameof(DeleterUserId))]
        public long? DeleterUserId { get; }

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
        /// Gets or sets the creation time.
        /// </summary>
        /// <value>The creation time.</value>
        [DataObjectField(false)]
        [XmlElement]
        public DateTime CreationTime { get;}

        /// <summary>
        /// The id of the User Agent which created this value object
        /// </summary>
        /// <value>The creator user identifier.</value>
        [DataObjectField(false)]
        [XmlAttribute]
        [ForeignKey(nameof(CreatorUserId))]
        public long? CreatorUserId { get; }

        /// <summary>
        /// The name of the creating user
        /// </summary>
        /// <value>The name of the creator user.</value>
        [MaxLength(256, ErrorMessageResourceName = "Validation_Name_256CharsOrLess", ErrorMessageResourceType = typeof(Deploy_LaunchPad_Core_Resources))]
        [DataObjectField(false)]
        [XmlAttribute]
        public string? CreatorUserName { get; }

        /// <summary>
        /// The id of the User Agent which last modified this object.
        /// </summary>
        /// <value>The last modifier user identifier.</value>
        [DataObjectField(false)]
        [XmlAttribute]
        [ForeignKey(nameof(LastModifierUserId))]
        public Int64? LastModifierUserId { get; }

        /// <summary>
        /// Gets or sets the last modification time.
        /// </summary>
        /// <value>The last modification time.</value>
        [DataObjectField(false)]
        [XmlElement]
        public DateTime? LastModificationTime { get;}

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
