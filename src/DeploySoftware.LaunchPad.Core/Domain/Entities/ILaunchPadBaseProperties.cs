using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DeploySoftware.LaunchPad.Core.Domain
{
    /// <summary>
    /// Defines the minimum (read-only) properties LaunchPad expects to have for a Domain Entity or Value Object. 
    /// Note these deliberately correspond 1:1 to many of the properties found in various ABP domain entity interfaces, which would also be inherited by implementing classes. 
    /// </summary>
    /// <typeparam name="TIdType"></typeparam>
    public interface ILaunchPadBaseProperties<TIdType>
    {
        /// <summary>
        /// The culture of this object
        /// </summary>
        [Required]
        [MaxLength(5, ErrorMessageResourceName = "Validation_Culture_5CharsOrLess", ErrorMessageResourceType = typeof(DeploySoftware_LaunchPad_Core_Resources))]
        [DataObjectField(true)]
        [XmlAttribute]
        public String Culture { get; }


        /// <summary>
        /// The display name of this object
        /// </summary>
        [Required]
        [MaxLength(100, ErrorMessageResourceName = "Validation_Name_100CharsOrLess", ErrorMessageResourceType = typeof(DeploySoftware_LaunchPad_Core_Resources))]
        [DataObjectField(false)]
        [XmlAttribute]
        public String Name { get; }

        /// <summary>
        /// The external ID stored in a client system (if any). Can be any type on client system, but retained here as text.
        /// </summary>
        [MaxLength(36, ErrorMessageResourceName = "Validation_ExternalId_36CharsOrLess", ErrorMessageResourceType = typeof(DeploySoftware_LaunchPad_Core_Resources))]
        [DataObjectField(false)]
        [XmlAttribute]
        public String ExternalId { get; }

        /// <summary>
        /// A short description for this value object
        /// </summary>
        [Required]
        [MaxLength(256, ErrorMessageResourceName = "Validation_DescriptionShort_256CharsOrLess", ErrorMessageResourceType = typeof(DeploySoftware_LaunchPad_Core_Resources))]
        [DataObjectField(false)]
        [XmlAttribute]
        public String DescriptionShort { get; }

        /// <summary>
        /// The full description for this value object
        /// </summary>
        [MaxLength(8096, ErrorMessageResourceName = "Validation_DescriptionFull_8096CharsOrLess", ErrorMessageResourceType = typeof(DeploySoftware_LaunchPad_Core_Resources))]
        [DataObjectField(false)]
        [XmlElement]
        public string? DescriptionFull { get; }

        /// <summary>
        /// The sequence number for this value object, if any (for sorting and ordering purposes). Defaults to 0 if not set.
        /// </summary>
        [DataObjectField(false)]
        [XmlElement]
        public Int32 SeqNum { get; }

        /// <summary>
        /// Each value object can have an open-ended set of tags applied to it, that help users find, markup, and display its information
        /// </summary>
        [DataObjectField(false)]
        [XmlAttribute]
        public IEnumerable<MetadataTag> Tags { get; }


        /// <summary>
        /// If this object is not a translation this field will be null. 
        /// If this object is a translation, this id references the parent object.
        /// </summary>
        [DataObjectField(true)]
        [XmlAttribute]
        public TIdType TranslatedFromId { get; }

        [DataObjectField(false)]
        [XmlAttribute]
        public bool IsActive { get; }

        /// <summary>
        /// Used for preserving deletion time for a domain entity, obviously a Value Object can't be deleted.
        /// </summary>
        [DataObjectField(false)]
        [XmlElement]
        public DateTime? DeletionTime { get; }

        /// <summary>
        /// The id of the user which deleted. Used for preserving information for a domain entity, obviously a Value Object can't be deleted.
        /// </summary>
        [DataObjectField(false)]
        [XmlAttribute]
        [ForeignKey(nameof(DeleterUserId))]
        public long? DeleterUserId { get; }


        /// <summary>
        /// Used for preserving deletion status for a domain entity, obviously a Value Object can't be deleted.
        /// </summary>
        [DataObjectField(false)]
        [XmlElement]
        public bool IsDeleted { get; }

        [DataObjectField(false)]
        [XmlElement]
        public DateTime? LastModificationTime { get; }

        [DataObjectField(false)]
        [XmlElement]
        public DateTime CreationTime { get; }

        /// <summary>
        /// The id of the User Agent which created this value object
        /// </summary>
        [DataObjectField(false)]
        [XmlAttribute]
        [ForeignKey(nameof(CreatorUserId))]
        public long? CreatorUserId { get; }

        /// <summary>
        /// The id of the User Agent which last modified this object.
        /// </summary>
        [DataObjectField(false)]
        [XmlAttribute]
        [ForeignKey(nameof(LastModifierUserId))]
        public Int64? LastModifierUserId { get; }
    }
}
