//LaunchPad Shared
// Copyright (c) 2016-2021 Deploy Software Solutions, inc. 

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

namespace DeploySoftware.LaunchPad.Core.Domain
{
    using Abp.Domain.Entities;
    using System;
    using System.ComponentModel;
    using System.Reflection;
    using System.Runtime.Serialization;
    using System.Security.Permissions;
    using System.Text;
    using System.Xml.Serialization;
    
    using Abp.Domain.Entities.Auditing;
    using System.Globalization;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Collections.Generic;
    using System.Linq;
    using Newtonsoft.Json;
    using Abp.Domain.Values;

    /// <summary>
    /// Base class for transient / value objects, ie. those that are not Domain Entities.
    /// By definition this value object has no specific identity, and is transient / not persisted. 
    /// Implements <see cref="IValueObject">IValueObject</see> and inherits from ABP's ValueObject base class, which provides
    /// base functionality for many of its methods. Inherits from AspNetBoilerplate's Entity class.
    /// Implements AspNetBoilerplate's auditing interfaces.
    /// </summary>
    [Serializable]
    public abstract partial class LaunchPadValueObjectBase<TIdType> : ValueObject, ILaunchPadValueObject<TIdType>
    {

        /// <summary>
        /// The culture of this object
        /// </summary>
        [Required]
        [MaxLength(5, ErrorMessageResourceName = "Validation_Culture_5CharsOrLess", ErrorMessageResourceType = typeof(DeploySoftware_LaunchPad_Core_Resources))]
        [DataObjectField(true)]
        [XmlAttribute]
        public virtual String Culture { get; set; }


        /// <summary>
        /// The display name of this object
        /// </summary>
        [Required]
        [MaxLength(100, ErrorMessageResourceName = "Validation_Name_100CharsOrLess", ErrorMessageResourceType = typeof(DeploySoftware_LaunchPad_Core_Resources))]
        [DataObjectField(false)]
        [XmlAttribute]
        public virtual String Name { get; set; }

        /// <summary>
        /// The external ID stored in a client system (if any). Can be any type on client system, but retained here as text.
        /// </summary>
        [MaxLength(36, ErrorMessageResourceName = "Validation_ExternalId_36CharsOrLess", ErrorMessageResourceType = typeof(DeploySoftware_LaunchPad_Core_Resources))]
        [DataObjectField(false)]
        [XmlAttribute]
        public virtual String ExternalId { get; set; }

        /// <summary>
        /// A short description for this value object
        /// </summary>
        [Required]
        [MaxLength(256, ErrorMessageResourceName = "Validation_DescriptionShort_256CharsOrLess", ErrorMessageResourceType = typeof(DeploySoftware_LaunchPad_Core_Resources))]
        [DataObjectField(false)]
        [XmlAttribute]
        public virtual String DescriptionShort { get; set; }

        /// <summary>
        /// The full description for this value object
        /// </summary>
        [MaxLength(8096, ErrorMessageResourceName = "Validation_DescriptionFull_8096CharsOrLess", ErrorMessageResourceType = typeof(DeploySoftware_LaunchPad_Core_Resources))]
        [DataObjectField(false)]
        [XmlElement]
        public virtual string? DescriptionFull { get; set; }

        /// <summary>
        /// The sequence number for this value object, if any (for sorting and ordering purposes). Defaults to 0 if not set.
        /// </summary>
        [DataObjectField(false)]
        [XmlElement]
        public virtual Int32 SeqNum { get; set; } = 0;

        /// <summary>
        /// Each value object can have an open-ended set of tags applied to it, that help users find, markup, and display its information
        /// </summary>
        [DataObjectField(false)]
        [XmlAttribute]
        public virtual IEnumerable<MetadataTag> Tags { get; set; }


        /// <summary>
        /// If this object is not a translation this field will be null. 
        /// If this object is a translation, this id references the parent object.
        /// </summary>
        [DataObjectField(true)]
        [XmlAttribute]
        public virtual TIdType TranslatedFromId { get; set; }

        [DataObjectField(false)]
        [XmlAttribute]
        public virtual bool IsActive { get; set; } = true;

        /// <summary>
        /// Used for preserving deletion time for a domain entity, obviously a Value Object can't be deleted.
        /// </summary>
        [DataObjectField(false)]
        [XmlElement]
        public virtual DateTime? DeletionTime { get; set; }

        /// <summary>
        /// The id of the user which deleted. Used for preserving information for a domain entity, obviously a Value Object can't be deleted.
        /// </summary>
        [DataObjectField(false)]
        [XmlAttribute]
        [ForeignKey(nameof(DeleterUserId))]
        public virtual long? DeleterUserId { get; set; }


        /// <summary>
        /// Used for preserving deletion status for a domain entity, obviously a Value Object can't be deleted.
        /// </summary>
        [DataObjectField(false)]
        [XmlElement]
        public virtual bool IsDeleted { get; set; } = false;

        [DataObjectField(false)]
        [XmlElement]
        public virtual DateTime? LastModificationTime { get; set; }

        [DataObjectField(false)]
        [XmlElement]
        public virtual DateTime CreationTime { get; set; }

        /// <summary>
        /// The id of the User Agent which created this value object
        /// </summary>
        [DataObjectField(false)]
        [XmlAttribute]
        [ForeignKey(nameof(CreatorUserId))]
        public virtual long? CreatorUserId { get; set; }

        /// <summary>
        /// The id of the User Agent which last modified this object.
        /// </summary>
        [DataObjectField(false)]
        [XmlAttribute]
        [ForeignKey(nameof(LastModifierUserId))]
        public virtual Int64? LastModifierUserId { get; set; }

        /// <summary>
        /// A convenience readonly method to get a <see cref="CultureInfo">CultureInfo</see> instance from the current 
        /// culture code
        /// </summary>
        public virtual CultureInfo GetCultureInfo()
        {
            return new CultureInfo(Culture); 
        }

       
        /// <summary>
        /// Ensure the culture is one of the supported ones
        /// </summary>
        /// <param name="culture"></param>
        /// <returns></returns>
        private static bool IsValidCultureInfoName(string name)
        {
            return
                CultureInfo
                .GetCultures(CultureTypes.SpecificCultures)
                .Any(c => c.Name == name);
        }

        /// <summary>  
        /// Initializes a new instance of the <see cref="ValueObjectBase">ValueObject</see> class
        /// </summary>
        protected LaunchPadValueObjectBase() : base()
        {
            ExternalId = string.Empty;
            Culture = ApplicationDetails<TIdType>.DEFAULT_CULTURE;
            Tags = new List<MetadataTag>();
            IsDeleted = false;
            IsActive = true;
            Name = string.Empty;
            DescriptionShort = string.Empty;
            DescriptionFull = string.Empty;

        }

        /// <summary>  
        /// Initializes a new instance of the <see cref="ValueObjectBase">ValueObject</see> class with useful default values. 
        /// </summary>
        public LaunchPadValueObjectBase(string name, string descriptionShort, string descriptionFull) : base()
        {
            ExternalId = string.Empty;
            Culture = ApplicationDetails<TIdType>.DEFAULT_CULTURE;
            Tags = new List<MetadataTag>();
            IsDeleted = false;
            IsActive = true;
            Name = name;
            DescriptionShort = descriptionShort;
            DescriptionFull = descriptionFull;
        }

        /// <summary>  
        /// Initializes a new instance of the <see cref="ValueObjectBase">ValueObject</see> class with all values.
        /// </summary>
        public LaunchPadValueObjectBase(
            string name, 
            string descriptionShort, 
            string descriptionFull,
            string externalId,
            string culture,
            IList<MetadataTag> tags,
            DateTime creationTime,
            long creatorUserId,
            DateTime? lastModificationTime,
            long? lastModifierUserId,
            bool isDeleted,
            long? deleterUserId,
            DateTime? deletionTime,
            bool isActive,
            TIdType translatedFromId,
            int seqNum

        ) : base()
        {
            Name = name;
            DescriptionShort = descriptionShort;
            DescriptionFull = descriptionFull; 
            ExternalId = externalId;
            Culture = culture;
            Tags = tags;
            CreationTime = creationTime;
            CreatorUserId = creatorUserId;
            LastModificationTime = lastModificationTime;
            LastModifierUserId = lastModifierUserId;
            IsDeleted = isDeleted;
            DeleterUserId = deleterUserId;
            DeletionTime = deletionTime;
            IsActive = isActive;
            TranslatedFromId = translatedFromId;
            SeqNum = seqNum;
        }

        /// <summary>
        /// Serialization constructor used for deserialization
        /// </summary>
        /// <param name="info">The serialization info</param>
        /// <param name="context">The context of the stream</param>
        protected LaunchPadValueObjectBase(SerializationInfo info, StreamingContext context)
        {
            ExternalId = info.GetString("ExternalId"); 
            Culture = info.GetString("Culture");
            Name = info.GetString("DisplayName");
            DescriptionShort = info.GetString("DescriptionShort");
            DescriptionFull = info.GetString("DescriptionFull");
            Tags = (IEnumerable<MetadataTag>)info.GetValue("Metadata", typeof(IEnumerable<MetadataTag>));
            CreationTime = info.GetDateTime("CreationTime");
            CreatorUserId = info.GetInt64("CreatorUserId");
            LastModificationTime = info.GetDateTime("LastModificationTime");
            LastModifierUserId = info.GetInt64("LastModifierUserId");
            IsDeleted = info.GetBoolean("IsDeleted");
            DeleterUserId = info.GetInt64("DeleterUserId");
            DeletionTime = info.GetDateTime("DeletionTime");
            IsActive = info.GetBoolean("IsActive");
            TranslatedFromId = (TIdType)info.GetValue("TranslatedFromId", typeof(TIdType));
            SeqNum = info.GetInt32("SeqNum");

            //Metadata = (MetadataInformation)info.GetValue("Metadata", typeof(MetadataInformation));

        }

        //Requires to implement this method to return properties.
        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Name;
            yield return DescriptionShort;
            yield return DescriptionFull;
            yield return ExternalId;
            yield return Culture;
            yield return Tags;
            yield return CreationTime;
            yield return CreatorUserId;
            yield return LastModificationTime;
            yield return LastModifierUserId;
            yield return IsDeleted;
            yield return DeleterUserId;
            yield return DeletionTime;
            yield return IsActive;
            yield return TranslatedFromId;
            yield return SeqNum;
        }

        /// <summary>
        /// The method required for implementing ISerializable
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("ExternalId", ExternalId);
            info.AddValue("Culture", Culture);
            info.AddValue("Name", Name);
            info.AddValue("DescriptionShort", DescriptionShort);
            info.AddValue("DescriptionFull", DescriptionFull);
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
            info.AddValue("TranslatedFromId", TranslatedFromId);

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
        protected virtual TEntity Clone<TEntity>() where TEntity : IDomainEntity<TIdType>, new()
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
        /// <returns></returns>
        public virtual int CompareTo(DomainEntityBase<TIdType> other)
        {
            // put comparison of properties in here 
            // for base object we'll just sort by title
            return Name.CompareTo(other.Name);
        }

        /// <summary>  
        /// Displays information about the <c>Field</c> in readable format.  
        /// </summary>  
        /// <returns>A string representation of the object.</returns>
        public override String ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("[ValueObjectBase: ");
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
            sb.AppendFormat("ExternalId={0};", ExternalId);
            sb.AppendFormat("Name={0};", Name);
            sb.AppendFormat("DescriptionShort={0};", DescriptionShort);
            sb.AppendFormat("DescriptionFull={0};", DescriptionFull);
            sb.AppendFormat("TranslatedFromId={0};", TranslatedFromId);
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
        /// Computes and retrieves a hash code for a value object.  
        /// </summary>  
        /// <remarks>  
        /// This method implements the <see cref="Object">Object</see> method.  
        /// </remarks>  
        /// <returns>A hash code for an object.</returns>
        public override int GetHashCode()
        {
            return Name.GetHashCode() + DescriptionShort.GetHashCode() + DescriptionFull.GetHashCode() + Culture.GetHashCode() + CreationTime.GetHashCode()
                + ExternalId.GetHashCode();
        }

    }
}
