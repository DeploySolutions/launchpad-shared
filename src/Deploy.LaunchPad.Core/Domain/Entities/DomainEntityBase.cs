// ***********************************************************************
// Assembly         : Deploy.LaunchPad.Core.Abp
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 10-27-2023
// ***********************************************************************
// <copyright file="LaunchPadDomainEntityBase.cs" company="Deploy Software Solutions, inc.">
//     2018-2024 Deploy Software Solutions, inc.
// </copyright>
// <summary></summary>
// ***********************************************************************

#region license
//Licensed under the Apache License, Version 2.0 (the "License"); 
//you may not use this file except in compliance with the License. 
//You may obtain a copy of the License at 

//http://www.apache.org/licenses/LICENSE-2.0 

//Unless required by applicable law or agreed to in writing, software 
//distributed under the License is distributed on an "AS IS" BASIS, 
//WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. 
//See the License for the specific language governing permissions and 
//limitations under the License. 
#endregion


using Deploy.LaunchPad.Core.Domain.Entities.Auditing;
using Deploy.LaunchPad.Util;
using Deploy.LaunchPad.Util.Elements;
using Newtonsoft.Json;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Runtime.Serialization;
using System.Text;
using System.Xml.Serialization;

namespace Deploy.LaunchPad.Core.Domain.Entities
{

    /// <summary>
    /// Base class for Domain Entities. Implements <see cref="IDomainEntity">IDomainEntity</see> and provides
    /// base functionality for many of its methods. Inherits from FullAuditedEntity (and further upstream FrameworkEntityBase class which is for AspNetBoilerplate's elements).
    /// Implements AspNetBoilerplate's auditing interfaces.
    /// </summary>
    /// <typeparam name="TPrimaryKey">The type of the t identifier type.</typeparam>
    [DebuggerDisplay("{_debugDisplay}")]
    [Serializable]
    public abstract partial class DomainEntityBase<TPrimaryKey> : FullAuditedEntity<TPrimaryKey>,
         IDomainEntity<TPrimaryKey>
    {
        ///// <summary>
        ///// Controls the DebuggerDisplay attribute presentation (above). This will only appear during VS debugging sessions and should never be logged.
        ///// </summary>
        ///// <value>The debug display.</value>
        protected override string _debugDisplay => $"Name {Name}. Description {Description}";

        /// <summary>
        /// A  description for this entity
        /// </summary>
        /// <value>The description.</value>
        [DataObjectField(false)]
        [XmlAttribute]
        //[JsonPropertyName("description")]
        public virtual ElementDescription Description { get; set; }

        protected TPrimaryKey? _translatedFromId;
        /// <summary>
        /// If this object is not a translation this field will be null.
        /// If this object is a translation, this id references the parent object.
        /// </summary>
        /// <value>The translated from identifier.</value>
        [DataObjectField(false)]
        [DataMember(Name = "translatedFromId", EmitDefaultValue = false)]
        [XmlAttribute]
        public virtual TPrimaryKey? TranslatedFromId
        {
            get { return _translatedFromId; }
            set { _translatedFromId = value; }
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

        protected CultureInfo _culture;
        /// <summary>
        /// The culture of this object
        /// </summary>
        /// <value>The culture.</value>
        [Required]
        [MaxLength(5, ErrorMessageResourceName = "Validation_Culture_5CharsOrLess", ErrorMessageResourceType = typeof(Deploy_LaunchPad_Core_Resources))]
        [DataObjectField(false)]
        [DataMember(Name = "culture", EmitDefaultValue = false)]
        [XmlAttribute]
        public virtual CultureInfo Culture
        {
            get { return _culture; }
            set { _culture = value; }
        }

        protected string _creatorUserName;
        /// <summary>
        /// The name of the creating user
        /// </summary>
        /// <value>The name of the creator user.</value>
        [MaxLength(255, ErrorMessageResourceName = "Validation_255CharsOrLess", ErrorMessageResourceType = typeof(Deploy_LaunchPad_Core_Resources))]
        [DataObjectField(false)]
        [XmlAttribute]
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string? CreatorUserName
        {
            get { return _creatorUserName; }
            set { _creatorUserName = value; }
        }

        protected string _lastModifierUserName;
        /// <summary>
        /// The name of the modifying user
        /// </summary>
        /// <value>The last name of the modifier user.</value>
        [MaxLength(255, ErrorMessageResourceName = "Validation_255CharsOrLess",
            ErrorMessageResourceType = typeof(Deploy_LaunchPad_Core_Resources))]
        [DataObjectField(false)]
        [XmlAttribute]
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string? LastModifierUserName
        {
            get { return _lastModifierUserName; }
            set { _lastModifierUserName = value; }
        }

        protected string _deleterUserName;
        /// <summary>
        /// The name of the deleting user
        /// </summary>
        /// <value>The name of the deleter user.</value>
        [MaxLength(255, ErrorMessageResourceName = "Validation_255CharsOrLess", ErrorMessageResourceType = typeof(Deploy_LaunchPad_Core_Resources))]
        [DataObjectField(false)]
        [XmlAttribute]
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string? DeleterUserName
        {
            get { return _deleterUserName; }
            set { _deleterUserName = value; }
        }

        public override EntityType EntityType { get; } = EntityType.DomainEntity;


        /// <summary>
        /// Initializes a new instance of the <see cref="DomainEntityBase">Entity</see> class
        /// </summary>
        protected DomainEntityBase() : base()
        {
            Culture = new CultureInfo("en-CA");
            IsDeleted = false;
            Name = string.Empty;
            Description = new ElementDescription(string.Empty, string.Empty);

        }

        /// <summary>
        /// Creates a new instance of the <see cref="DomainEntityBase">Entity</see> class given a key, and some metadata.
        /// </summary>
        /// <param name="id">The identifier.</param>
        [SetsRequiredMembers]
        protected DomainEntityBase(TPrimaryKey id) : base(id)
        {
            Culture = new CultureInfo("en-CA");
            CreatorUserId = Guid.NewGuid(); // TODO - default user account?
            IsDeleted = false;
            Name = Id.ToString();
            Description = new ElementDescription(string.Empty, string.Empty);
        }

        /// <summary>
        /// Creates a new instance of the <see cref="DomainEntityBase">Entity</see> class given a key, and some metadata.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="name">The name of the object.</param>
        /// <param name="description">The description for this entity</param>
        [SetsRequiredMembers]
        protected DomainEntityBase(TPrimaryKey id, ElementName name) : base(id)
        {
            Culture = new CultureInfo("en-CA");
            CreatorUserId = Guid.NewGuid(); // TODO - default user account?
            IsDeleted = false;
            Name = name.Name;
            Description = new ElementDescription(name.Name);
        }

        /// <summary>
        /// Creates a new instance of the <see cref="DomainEntityBase">Entity</see> class given a key, and some metadata.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="name">The name of the object.</param>
        /// <param name="description">The description for this entity</param>
        [SetsRequiredMembers]
        protected DomainEntityBase(TPrimaryKey id, ElementName name, ElementDescription description) : base()
        {
            Culture = new CultureInfo("en-CA");
            CreatorUserId = Guid.NewGuid(); // TODO - default user account?
            IsDeleted = false;
            Name = name.Name;
            Description = description;
        }


        /// <summary>
        /// Creates a new instance of the <see cref="DomainEntityBase">Entity</see> class given a key, and some metadata.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="name">The name of the object.</param>
        [SetsRequiredMembers]
        protected DomainEntityBase(TPrimaryKey id, string name) : base(id,name)
        {
            Culture = new CultureInfo("en-CA");
            CreatorUserId = Guid.NewGuid(); // TODO - default user account?
            IsDeleted = false;
            Description = new ElementDescription(string.Empty, string.Empty);
        }

        /// <summary>
        /// Creates a new instance of the <see cref="DomainEntityBase">Entity</see> class given a key, and some metadata.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="name">The name of the object.</param>
        /// <param name="culture">The culture for this entity</param>
        [SetsRequiredMembers]   
        protected DomainEntityBase(TPrimaryKey id, string name, CultureInfo culture) : base(id, name, culture)
        {
            CreatorUserId = Guid.NewGuid(); // TODO - default user account?
            IsDeleted = false;
            Description = new ElementDescription(string.Empty, string.Empty);
        }

        /// <summary>
        /// Creates a new instance of the <see cref="DomainEntityBase">Entity</see> class given a key, and some metadata.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="name">The name of the object.</param>
        /// <param name="culture">The culture for this entity</param>
        protected DomainEntityBase(TPrimaryKey id, ElementName name, CultureInfo culture) : base()
        {
            Id = id;
            Culture = culture;
            CreatorUserId = Guid.NewGuid(); // TODO - default user account?
            IsDeleted = false;
            Name = name.Name;
            Description = new ElementDescription(string.Empty, string.Empty);
        }

        /// <summary>
        /// Creates a new instance of the <see cref="DomainEntityBase">Entity</see> class given a key, and some metadata.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="name">The name of the object.</param>
        /// <param name="culture">The culture for this entity</param>
        protected DomainEntityBase(TPrimaryKey id, ElementName name, ElementDescription description, CultureInfo culture) : base()
        {
            Id = id;
            Culture = culture;
            CreatorUserId = Guid.NewGuid(); // TODO - default user account?
            IsDeleted = false;
            Name = name.Name;
            Description = description;
        }

        /// <summary>
        /// Serialization constructor used for deserialization
        /// </summary>
        /// <param name="info">The serialization info</param>
        /// <param name="context">The context of the stream</param>
        protected DomainEntityBase(SerializationInfo info, StreamingContext context)
        {
            Id = (TPrimaryKey)info.GetValue("Id", typeof(TPrimaryKey));
            Culture = (CultureInfo)info.GetValue("Culture", typeof(CultureInfo));
            Name = (string)info.GetValue("Name", typeof(string));
            Description = (ElementDescription)info.GetValue("Description", typeof(ElementDescription));
            Checksum = info.GetString("Checksum");
            Tags = info.GetString("Tags");
            CreationTime = info.GetDateTime("CreationTime");
            CreatorUserId = (Guid)info.GetValue("CreatorUserId", typeof(Guid));
            LastModificationTime = info.GetDateTime("LastModificationTime");
            LastModifierUserId = (Guid)info.GetValue("LastModifierUserId", typeof(Guid));
            IsDeleted = info.GetBoolean("IsDeleted");
            DeleterUserId = (Guid?)info.GetValue("DeleterUserId", typeof(Guid?));
            DeletionTime = info.GetDateTime("DeletionTime");
            SeqNum = info.GetInt32("SeqNum");
        }

        /// <summary>
        /// The method required for implementing ISerializable
        /// </summary>
        /// <param name="info">The information.</param>
        /// <param name="context">The context.</param>
        public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Id", Id);
            info.AddValue("Culture", Culture);
            info.AddValue("Name", Name);
            info.AddValue("Description", Description);
            info.AddValue("Checksum", Checksum);
            info.AddValue("Tags", Tags);
            info.AddValue("CreationTime", CreationTime);
            info.AddValue("CreatorUserId", CreatorUserId);
            info.AddValue("LastModificationTime", LastModificationTime);
            info.AddValue("LastModifierUserId", LastModifierUserId);
            info.AddValue("IsDeleted", IsDeleted);
            info.AddValue("DeleterUserId", DeleterUserId);
            info.AddValue("DeletionTime", DeletionTime);
            info.AddValue("SeqNum", SeqNum);

        }

        /// <summary>
        /// Event called once deserialization constructor finishes.
        /// Useful for reattaching connections and other finite resources that
        /// can't be serialized and deserialized.
        /// </summary>
        /// <param name="sender">The object that has been deserialized</param>
        public virtual void OnDeserialization(object sender)
        {
            // reconnect connection strings and other resources that won't be serialized
        }

        public virtual DomainEntityBase<TPrimaryKey> CloneGeneric()
        {
            var clone = (DomainEntityBase<TPrimaryKey>)this.MemberwiseClone();
            // Deep clone reference-type fields as needed
            clone.Name = Name;
            clone.Description = Description?.CloneGeneric(); // assuming ElementDescription has a Clone() method
                                                        // ...repeat for other reference-type fields if needed
            return clone;
        }

        object ICloneable.Clone() => CloneGeneric();

        /// <summary>
        /// Comparison method between two objects of the same type, used for sorting.
        /// Because the CompareTo method is strongly typed by generic constraints,
        /// it is not necessary to test for the correct object type.
        /// </summary>
        /// <param name="other">The other object of this type we are comparing to</param>
        /// <returns>System.Int32.</returns>
        public virtual int CompareTo(DomainEntityBase<TPrimaryKey> other)
        {
            // put comparison of properties in here 
            // for base object we'll just sort by DisplayName
            return Name.CompareTo(other.Name);
        }

        /// <summary>
        /// Displays information about the <c>Field</c> in readable format.
        /// </summary>
        /// <returns>A string representation of the object.</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("[DomainEntityBase: ");
            sb.Append(ToStringBaseProperties());
            sb.Append(']');
            return sb.ToString();
        }

        /// <summary>
        /// This method makes it easy for any child class to generate a ToString() representation of
        /// the common base properties
        /// </summary>
        /// <returns>A string description of the entity</returns>
        protected virtual string ToStringBaseProperties()
        {
            StringBuilder sb = new StringBuilder();
            // LaunchPAD RAD properties
            sb.AppendFormat("Id={0};", Id);
            sb.AppendFormat("Name={0};", Name);
            sb.AppendFormat("Description={0};", Description);
            sb.AppendFormat("Checksum={0};", Checksum);
            sb.AppendFormat(" Tags={0};", Tags.ToString());
            sb.AppendFormat("SeqNum={0};", SeqNum);

            // ABP properties
            sb.AppendFormat("CreationTime={0};", CreationTime);
            sb.AppendFormat("CreatorUserId={0};", CreatorUserId);
            sb.AppendFormat("LastModificationTime={0};", LastModificationTime);
            sb.AppendFormat("LastModifierUserId={0};", LastModifierUserId);
            sb.AppendFormat("IsDeleted={0};", IsDeleted);
            sb.AppendFormat("DeleterUserId={0};", DeleterUserId);
            sb.AppendFormat("DeletionTime={0};", DeletionTime);
            return sb.ToString();
        }

        /// <summary>
        /// Override the legacy Equals. Must cast obj in this case.
        /// </summary>
        /// <param name="obj">A type to check equivalency of (hopefully) an Entity</param>
        /// <returns>True if the entities are the same according to business key value</returns>
        public override bool Equals(object obj)
        {
            if (obj != null && obj is DomainEntityBase<TPrimaryKey>)
            {
                return Equals(obj as DomainEntityBase<TPrimaryKey>);
            }
            return false;
        }

        /// <summary>
        /// Equality method between two objects of the same type.
        /// Because the Equals method is strongly typed by generic constraints,
        /// it is not necessary to test for the correct object type.
        /// For safety we just want to match on business key value - in this case the fields
        /// that cannot be different between the two objects if they are supposedly equal.
        /// </summary>
        /// <param name="obj">The other object of this type that we are testing equality with</param>
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
        public virtual bool Equals(DomainEntityBase<TPrimaryKey> obj)
        {
            if (obj != null)
            {

                // Transient objects are not considered as equal
                if (IsTransient() && obj.IsTransient())
                {
                    return false;
                }
                else
                {
                    // if checksum is set, use it for equality
                    if (Checksum != null && obj.Checksum != null)
                    {
                        return Checksum.Equals(obj.Checksum) && IsDeleted.Equals(obj.IsDeleted);
                    }
                    else
                    {
                        // For safe equality we need to match on business key equality.
                        // Base domain entities are functionally equal if their key and metadata are equal.
                        // Subclasses should extend to include their own enhanced equality checks, as required.
                        return Id.Equals(obj.Id) && Culture.Equals(obj.Culture) 
                            && Name.Equals(obj.Name)
                            && Description.Equals(obj.Description)
                            && IsDeleted.Equals(obj.IsDeleted);
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// Override the == operator to test for equality
        /// </summary>
        /// <param name="x">The first value</param>
        /// <param name="y">The second value</param>
        /// <returns>True if both objects are fully equal based on the Equals logic</returns>
        public static bool operator ==(DomainEntityBase<TPrimaryKey> x, DomainEntityBase<TPrimaryKey> y)
        {
            if (x is null)
            {
                if (y is null)
                {
                    return true;
                }
                return false;
            }
            return x.Equals(y);
        }

        /// <summary>
        /// Override the != operator to test for inequality
        /// </summary>
        /// <param name="x">The first value</param>
        /// <param name="y">The second value</param>
        /// <returns>True if both objects are not equal based on the Equals logic</returns>
        public static bool operator !=(DomainEntityBase<TPrimaryKey> x, DomainEntityBase<TPrimaryKey> y)
        {
            return !(x == y);
        }

        /// <summary>
        /// Computes and retrieves a hash code for an object.
        /// </summary>
        /// <returns>A hash code for an object.</returns>
        /// <remarks>This method implements the <see cref="object">Object</see> method.</remarks>
        public override int GetHashCode()
        {
            return Culture.GetHashCode()
                + Id.GetHashCode()
                + Checksum.GetHashCode()
                + Name.GetHashCode()
                ;
        }

    }
}
