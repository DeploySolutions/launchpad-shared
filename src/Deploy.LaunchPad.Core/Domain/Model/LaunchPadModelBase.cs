// ***********************************************************************
// Assembly         : Deploy.LaunchPad.Core
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 10-27-2023
// ***********************************************************************
// <copyright file="LaunchPadModelBase.cs" company="Deploy Software Solutions, inc.">
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

namespace Deploy.LaunchPad.Core.Abp.Domain.Model
{
    using Deploy.LaunchPad.Core;
    using Deploy.LaunchPad.Core.Domain;
    using Deploy.LaunchPad.Core.Domain.Model;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Diagnostics;
    using System.Diagnostics.CodeAnalysis;
    using System.Globalization;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.Serialization;
    using System.Text;
    using System.Xml.Serialization;

    /// <summary>
    /// Base class for Entities. Implements <see cref="ILaunchPadCommonProperties">ILaunchPadBaseProperties</see> and provides
    /// base functionality for many of its properties.
    /// </summary>
    [DebuggerDisplay("{_debugDisplay}")]
    [Serializable]
    public abstract partial class LaunchPadModelBase :
        ILaunchPadCommonProperties, IEquatable<LaunchPadModelBase>, IComparable<LaunchPadModelBase>
    {

        /// <summary>
        /// Controls the DebuggerDisplay attribute presentation (above). This will only appear during VS debugging sessions and should never be logged.
        /// </summary>
        /// <value>The debug display.</value>
        protected virtual string _debugDisplay => $"Name {Name}. Description {Description}";
        
        protected ElementName _name;
        /// <summary>
        /// The name of this object
        /// </summary>
        /// <value>The name.</value>
        [Required]
        [DataObjectField(false)]
        [XmlAttribute]
        public virtual ElementName Name
        {
            get { return _name; }
            set { _name = value; }
        }

        protected ElementDescription _description;
        /// <summary>
        /// A  description for this entity
        /// </summary>
        /// <value>The description.</value>
        [Required]
        [DataObjectField(false)]
        [XmlAttribute]
        public virtual ElementDescription Description
        {
            get { return _description; }
            set { _description = value; }
        }

        protected string _culture;
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

        protected int _seqNum;
        /// <summary>
        /// The sequence number for this entity, if any (for sorting and ordering purposes). Defaults to 0 if not set.
        /// </summary>
        /// <value>The seq number.</value>
        [DataObjectField(false)]
        [DataMember(Name = "seqNum", EmitDefaultValue = true)]
        [XmlElement]
        public virtual int SeqNum
        {
            get { return _seqNum; }
            set { _seqNum = value; }
        }

        protected IList<string> _tags;
        /// <summary>
        /// Each entity can have an open-ended set of tags applied to it, that help users find, markup, and display its information
        /// </summary>
        /// <value>The tags.</value>
        [DataObjectField(false)]
        [DataMember(Name = "tags", EmitDefaultValue = false)]
        [XmlAttribute]
        public virtual IList<string> Tags
        {
            get { return _tags; }
            set { _tags = value; }
        }

        protected bool _isActive;
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

        protected DateTime _creationTime;
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
        public bool IsDeleted { get; set; }

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

        /// <summary>
        /// Initializes a new instance of the <see cref="DomainEntityBase">Entity</see> class
        /// </summary>
        protected LaunchPadModelBase() : base()
        {
            Name = new ElementName(string.Empty, string.Empty);
            Description = new ElementDescription(string.Empty, string.Empty);
            Culture = "en";
            //TenantId = 0; // default tenant
            Tags = new List<string>();
            IsDeleted = false;
            IsActive = true;

        }


        /// <summary>
        /// Creates a new instance of the <see cref="DomainEntityBase">Entity</see> class given a key, and some metadata.
        /// </summary>
        /// <param name="culture">The culture for this entity</param>
        protected LaunchPadModelBase(string culture) : base()
        {
            Name = new ElementName(string.Empty, string.Empty);
            Description = new ElementDescription(string.Empty, string.Empty);
            Culture = culture;
            CreatorUserId = 1; // TODO - default user account?
            IsDeleted = false;
            IsActive = true;
            Tags = new List<string>();
        }


        /// <summary>
        /// Serialization constructor used for deserialization
        /// </summary>
        /// <param name="info">The serialization info</param>
        /// <param name="context">The context of the stream</param>
        protected LaunchPadModelBase(SerializationInfo info, StreamingContext context)
        {
            Name = (ElementName)info.GetValue("Name", typeof(ElementName));
            Description = (ElementDescription)info.GetValue("Description", typeof(ElementDescription));
            Culture = info.GetString("Culture");
            Checksum = info.GetString("Checksum");
            Tags = (List<string>)info.GetValue("Metadata", typeof(List<string>));
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
        public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Name", Name);
            info.AddValue("Description", Description);
            info.AddValue("Culture", Culture);
            info.AddValue("Checksum", Checksum);
            info.AddValue("Tags", Tags);
            info.AddValue("SeqNum", SeqNum);
            info.AddValue("CreationTime", CreationTime);
            info.AddValue("CreatorUserId", CreatorUserId);
            info.AddValue("LastModificationTime", LastModificationTime);
            info.AddValue("LastModifierUserId", LastModifierUserId);
            info.AddValue("IsDeleted", IsDeleted);
            info.AddValue("DeleterUserId", DeleterUserId);
            info.AddValue("DeletionTime", DeletionTime);
            info.AddValue("IsActive", IsActive);
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

        /// <summary>
        /// Shallow clones the entity
        /// </summary>
        /// <typeparam name="TEntity">The source entity to clone</typeparam>
        /// <returns>A shallow clone of the entity and its serializable properties</returns>
        protected virtual TEntity Clone<TEntity>() where TEntity : ILaunchPadCommonProperties, new()
        {
            TEntity clone = new TEntity();
            foreach (PropertyInfo info in GetType().GetProperties())
            {
                // ensure the property type is serializable
                if (info.GetType().IsSerializable)
                {
                    PropertyInfo cloneInfo = GetType().GetProperty(info.Name);
                    cloneInfo.SetValue(clone, info.GetValue(this, null), null);
                }
            }
            return clone;
        }

        /// <summary>
        /// Comparison method between two objects of the same type, used for sorting.
        /// Because the CompareTo method is strongly typed by generic constraints,
        /// it is not necessary to test for the correct object type.
        /// </summary>
        /// <param name="other">The other object of this type we are comparing to</param>
        /// <returns>A value that indicates the relative order of the objects being compared. The return value has these meanings:
        /// <list type="table"><listheader><term> Value</term><description> Meaning</description></listheader><item><term> Less than zero</term><description> This instance precedes <paramref name="other" /> in the sort order.</description></item><item><term> Zero</term><description> This instance occurs in the same position in the sort order as <paramref name="other" />.</description></item><item><term> Greater than zero</term><description> This instance follows <paramref name="other" /> in the sort order.</description></item></list></returns>
        public virtual int CompareTo(LaunchPadModelBase other)
        {
            // put comparison of properties in here 
            // for base object we'll just sort by Name
            return Name.CompareTo(other.Name);
        }

        /// <summary>
        /// Displays information about the <c>Field</c> in readable format.
        /// </summary>
        /// <returns>A string representation of the object.</returns>
        public override String ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("[LaunchPadBase: ");
            sb.Append(ToStringBaseProperties());
            sb.Append(']');
            return sb.ToString();
        }

        /// <summary>
        /// This method makes it easy for any child class to generate a ToString() representation of
        /// the common base properties
        /// </summary>
        /// <returns>A string description of the entity</returns>
        protected virtual String ToStringBaseProperties()
        {
            StringBuilder sb = new StringBuilder();
            // LaunchPAD RAD properties
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
            sb.AppendFormat("IsActive={0};", IsActive);
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
            if (obj != null && obj is LaunchPadModelBase)
            {
                return Equals(obj as LaunchPadModelBase);
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
        public virtual bool Equals(LaunchPadModelBase obj)
        {
            if (obj != null)
            {

                // For safe equality we need to match on business key equality.
                // Base domain entities are functionally equal if their key and metadata are equal.
                // Subclasses should extend to include their own enhanced equality checks, as required.
                return Checksum.Equals(obj.Checksum) && Culture.Equals(obj.Culture)
                    && Name.Equals(obj.Name)
                    && Description.Equals(obj.Description)
                    && IsActive.Equals(obj.IsActive) && IsDeleted.Equals(obj.IsDeleted);

            }
            return false;
        }

        /// <summary>
        /// Override the == operator to test for equality
        /// </summary>
        /// <param name="x">The first value</param>
        /// <param name="y">The second value</param>
        /// <returns>True if both objects are fully equal based on the Equals logic</returns>
        public static bool operator ==(LaunchPadModelBase x, LaunchPadModelBase y)
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
        public static bool operator !=(LaunchPadModelBase x, LaunchPadModelBase y)
        {
            return !(x == y);
        }

        /// <summary>
        /// Computes and retrieves a hash code for an object.
        /// </summary>
        /// <returns>A hash code for an object.</returns>
        /// <remarks>This method implements the <see cref="Object">Object</see> method.</remarks>
        public override int GetHashCode()
        {
            return Culture.GetHashCode()
                + Checksum.GetHashCode()
                + Name.GetHashCode()
            ;
        }

        /// <summary>
        /// Implements the &lt; operator.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator <(LaunchPadModelBase left, LaunchPadModelBase right)
        {
            return ReferenceEquals(left, null) ? !ReferenceEquals(right, null) : left.CompareTo(right) < 0;
        }

        /// <summary>
        /// Implements the &lt;= operator.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator <=(LaunchPadModelBase left, LaunchPadModelBase right)
        {
            return ReferenceEquals(left, null) || left.CompareTo(right) <= 0;
        }

        /// <summary>
        /// Implements the &gt; operator.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator >(LaunchPadModelBase left, LaunchPadModelBase right)
        {
            return !ReferenceEquals(left, null) && left.CompareTo(right) > 0;
        }

        /// <summary>
        /// Implements the &gt;= operator.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator >=(LaunchPadModelBase left, LaunchPadModelBase right)
        {
            return ReferenceEquals(left, null) ? ReferenceEquals(right, null) : left.CompareTo(right) >= 0;
        }
    }
}
