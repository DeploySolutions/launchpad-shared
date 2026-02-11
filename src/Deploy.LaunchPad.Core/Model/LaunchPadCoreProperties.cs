using Deploy.LaunchPad.Util;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;
using System.Globalization;
using Deploy.LaunchPad.Util.Elements;

namespace Deploy.LaunchPad.Domain.Model
{
    public partial class LaunchPadCoreProperties : LaunchPadMinimalProperties, ILaunchPadCoreProperties
    {
        // List of common property names (including some like ID that are not present here but are common in domain entities)
        public static readonly IList<string> CommonProperties = new List<string>
        {
            "Id",
            "Name",
            "Description",
            "Culture",
            "Checksum",
            "SeqNum",
            "Tags",
            "IsActive",
            "DeletionTime",
            "DeleterUserId",
            "DeleterUserName",
            "IsDeleted",
            "CreationTime",
            "CreatorUserId",
            "CreatorUserName",
            "LastModifierUserId",
            "LastModificationTime",
            "LastModifierUserName",
            "TranslatedFromId",
            "DataSet",
            "DataSetId"
        };

        protected string _culture = "en";
        /// <summary>
        /// The culture of this object
        /// </summary>
        /// <value>The culture.</value>
        [Required]
        [MaxLength(5, ErrorMessageResourceName = "Validation_Culture_5CharsOrLess", ErrorMessageResourceType = typeof(Deploy_LaunchPad_Core_Resources))]
        [DataObjectField(true)]
        [DataMember(Name = "culture", EmitDefaultValue = false)]
        [XmlAttribute]
        public virtual string Culture
        {
            get { return _culture; }
            set { _culture = value; }
        }


        protected string _checksum;
        /// <summary>
        /// The checksum for this  object, if any
        /// </summary>
        /// <value>The checksum.</value>
        [MaxLength(40, ErrorMessageResourceName = "Validation_Checksum_40CharsOrLess", ErrorMessageResourceType = typeof(Deploy_LaunchPad_Core_Resources))]
        [DataObjectField(false)]
        [DataMember(Name = "checksum", EmitDefaultValue = false)]
        [XmlAttribute]
        public virtual string Checksum
        {
            get { return _checksum; }
            set { _checksum = value; }
        }

        protected int? _seqNum;
        /// <summary>
        /// The sequence number for this entity, if any (for sorting and ordering purposes).
        /// </summary>
        /// <value>The seq number.</value>
        [DataObjectField(false)]
        [DataMember(Name = "seqNum", EmitDefaultValue = false)]
        [XmlElement]
        public virtual int? SeqNum
        {
            get { return _seqNum; }
            set { _seqNum = value; }
        }

        protected string _tags = "{}";
        /// <summary>
        /// Each entity can have an open-ended set of tags applied to it, that help users find, markup, and display its information
        /// </summary>
        /// <value>The tags.</value>
        [DataObjectField(false)]
        [DataMember(Name = "tags", EmitDefaultValue = false)]
        [XmlAttribute]
        public virtual string Tags
        {
            get { return _tags; }
            set { _tags = value; }
        }

        protected bool _isActive = true;
        /// <summary>
        /// Gets or sets a value indicating whether this instance is active.
        /// </summary>
        /// <value><c>true</c> if this instance is active; otherwise, <c>false</c>.</value>
        [DataObjectField(false)]
        [XmlAttribute]
        public virtual bool IsActive
        {
            get { return _isActive; }
            set { _isActive = value; }
        }

        protected DateTime _creationTime = DateTime.UtcNow;
        /// <summary>
        /// Gets or sets the creation time.
        /// </summary>
        /// <value>The creation time.</value>
        [DataObjectField(false)]
        [XmlAttribute]
        public DateTime CreationTime
        {
            get { return _creationTime; }
            set { _creationTime = value; }
        }

        protected long? _creatorUserId;
        /// <summary>
        /// The id of the User Agent which created this value object
        /// </summary>
        /// <value>The creator user identifier.</value>
        [DataObjectField(false)]
        [XmlAttribute]
        public long? CreatorUserId
        {
            get { return _creatorUserId; }
            set { _creatorUserId = value; }
        }


        protected string _creatorUserName;
        /// <summary>
        /// The name of the creating user
        /// </summary>
        /// <value>The name of the creator user.</value>
        [MaxLength(256, ErrorMessageResourceName = "Validation_Name_256CharsOrLess", ErrorMessageResourceType = typeof(Deploy_LaunchPad_Core_Resources))]
        [DataObjectField(false)]
        [XmlAttribute]
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string? CreatorUserName
        {
            get { return _creatorUserName; }
            set { _creatorUserName = value; }
        }

        protected DateTime? _lastModificationTime;
        /// <summary>
        /// Gets or sets the last modification time.
        /// </summary>
        /// <value>The last modification time.</value>
        [DataObjectField(false)]
        [XmlAttribute]
        public DateTime? LastModificationTime
        {
            get { return _lastModificationTime; }
            set { _lastModificationTime = value; }
        }

        protected long? _lastModifierUserId;
        /// <summary>
        /// The id of the User Agent which last modified this object.
        /// </summary>
        /// <value>The last modifier user identifier.</value>
        [DataObjectField(false)]
        [XmlAttribute]
        public long? LastModifierUserId
        {
            get { return _lastModifierUserId; }
            set { _lastModifierUserId = value; }
        }


        protected string _lastModifierUserName;
        /// <summary>
        /// The name of the modifying user
        /// </summary>
        /// <value>The last name of the modifier user.</value>
        [MaxLength(256, ErrorMessageResourceName = "Validation_Name_256CharsOrLess",
            ErrorMessageResourceType = typeof(Deploy_LaunchPad_Core_Resources))]
        [DataObjectField(false)]
        [XmlAttribute]
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string? LastModifierUserName
        {
            get { return _lastModifierUserName; }
            set { _lastModifierUserName = value; }
        }

        protected DateTime? _deletionTime;
        /// <summary>
        /// Used for preserving deletion time for a domain entity, obviously a Value Object can't be deleted.
        /// </summary>
        /// <value>The deletion time.</value>
        [DataObjectField(false)]
        [XmlAttribute]
        public DateTime? DeletionTime
        {
            get { return _deletionTime; }
            set { _deletionTime = value; }
        }


        protected long? _deleterUserId;
        /// <summary>
        /// The id of the user which deleted. Used for preserving information for a domain entity, obviously a Value Object can't be deleted.
        /// </summary>
        /// <value>The deleter user identifier.</value>
        [DataObjectField(false)]
        [XmlAttribute]
        public long? DeleterUserId
        {
            get { return _deleterUserId; }
            set { _deleterUserId = value; }
        }

        /// <summary>
        /// Used for preserving deletion status for a domain entity, obviously a Value Object can't be deleted.
        /// </summary>
        /// <value><c>true</c> if this instance is deleted; otherwise, <c>false</c>.</value>
        [DataObjectField(false)]
        [XmlAttribute]
        public bool IsDeleted { get; set; } = false;

        protected string _deleterUserName;
        /// <summary>
        /// The name of the deleting user
        /// </summary>
        /// <value>The name of the deleter user.</value>
        [MaxLength(256, ErrorMessageResourceName = "Validation_Name_256CharsOrLess", ErrorMessageResourceType = typeof(Deploy_LaunchPad_Core_Resources))]
        [DataObjectField(false)]
        [XmlAttribute]
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string? DeleterUserName
        {
            get { return _deleterUserName; }
            set { _deleterUserName = value; }
        }

        /// <summary>
        ///  constructor 
        /// </summary>
        public LaunchPadCoreProperties() : base()
        {
          
        }

        /// <summary>
        ///  constructor 
        /// </summary>
        public LaunchPadCoreProperties(string name) : base(new ElementName(name), new ElementDescription(name))
        {
        }

        /// <summary>
        /// Serialization constructor used for deserialization
        /// </summary>
        /// <param name="info">The serialization info</param>
        /// <param name="context">The context of the stream</param>
        protected LaunchPadCoreProperties(SerializationInfo info, StreamingContext context)
            : base(info,context)
        {
            Culture = info.GetString("Culture");
            Checksum = info.GetString("Checksum");
            Tags = info.GetString("Tags");
            CreationTime = info.GetDateTime("CreationTime");
            CreatorUserId = info.GetInt64("CreatorUserId");
            LastModificationTime = info.GetDateTime("LastModificationTime");
            LastModifierUserId = info.GetInt64("LastModifierUserId");
            IsDeleted = info.GetBoolean("IsDeleted");
            DeleterUserId = info.GetInt64("DeleterUserId");
            DeletionTime = info.GetDateTime("DeletionTime");
            IsActive = info.GetBoolean("IsActive");
            SeqNum = info.GetInt32("SeqNum");
        }

        /// <summary>
        /// The method required for implementing ISerializable
        /// </summary>
        /// <param name="info">The information.</param>
        /// <param name="context">The context.</param>
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue("Culture", Culture);
            info.AddValue("Checksum", Checksum);
            info.AddValue("Tags", Tags);
            info.AddValue("CreationTime", CreationTime);
            info.AddValue("CreatorUserId", CreatorUserId);
            info.AddValue("LastModificationTime", LastModificationTime);
            info.AddValue("LastModifierUserId", LastModifierUserId);
            info.AddValue("IsDeleted", IsDeleted);
            info.AddValue("DeleterUserId", DeleterUserId);
            info.AddValue("DeletionTime", DeletionTime);
            info.AddValue("IsActive", IsActive);
            info.AddValue("SeqNum", SeqNum);
        }

        /// <summary>
        /// A convenience readonly method to get a <see cref="CultureInfo">CultureInfo</see> instance from the current
        /// culture code
        /// </summary>
        /// <returns>CultureInfo.</returns>
        public virtual CultureInfo GetCultureInfo()
        {
            return new CultureInfo(Culture);
        }

        /// <summary>
        /// Ensure the culture is one of the supported ones
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns><c>true</c> if [is valid culture information name] [the specified name]; otherwise, <c>false</c>.</returns>
        private static bool IsValidCultureInfoName(string name)
        {
            return
                CultureInfo
                .GetCultures(CultureTypes.SpecificCultures)
                .Any(c => c.Name == name);
        }

    }
}
