﻿//LaunchPad Shared
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

    /// <summary>
    /// Base class for Entities. Implements <see cref="IDomainEntity">IDomainEntity</see> and provides
    /// base functionality for many of its methods. Inherits from AspNetBoilerplate's Entity class.
    /// Implements AspNetBoilerplate's auditing interfaces.
    /// </summary>
    public abstract partial class DomainEntityBase<TIdType> : 
        FullAuditedEntity<TIdType>, IDomainEntity<TIdType>, IMayHaveTenant
        
    {

        private TIdType id;
        private string culture;
        private int? tenantId;
        private TIdType translatedFromId;
        private string name;
        private string descriptionShort;
        private string descriptionFull;
        private DateTime creationTime;
        private Int64? creatorUserId;
        private DateTime? lastModificationTime;
        private Int64? lastModifierUserId;
        private Int64? deleterUserId;
        private DateTime? deletionTime;
        private Boolean isActive;
        private Boolean isDeleted;

        /// <summary>
        /// The culture of this object
        /// </summary>
        [DataObjectField(true)]
        [XmlAttribute]
        public String Culture
        {
            get { return culture; }
            set
            {
                culture = value;
                Metadata.Culture = value;
            }
        }

        /// <summary>
        /// The display name of this object
        /// </summary>
        [DataObjectField(false)]
        [XmlAttribute]
        public String Name {
            get { return name; }
            set
            {
                name = value;
                Metadata.Name = value;
            }
        }

        /// <summary>
        /// A short description for this entity
        /// </summary>
        [DataObjectField(false)]
        [XmlAttribute]
        public String DescriptionShort {
            get { return descriptionShort; }
            set
            {
                descriptionShort = value;
                Metadata.DescriptionShort = value;
            }
        }

        /// <summary>
        /// The full description for this entity
        /// </summary>
        [DataObjectField(false)]
        [XmlElement]
        public String DescriptionFull {
            get { return descriptionFull; }
            set
            {
                descriptionFull = value;
                Metadata.DescriptionFull = value;
            }
        }

        /// <summary>
        /// Each entity can have an open-ended set of metadata applied to it, that helps to describe it.
        /// </summary>
        [DataObjectField(false)]
        [XmlAttribute]
        [NotMapped]
        public MetadataInformation Metadata { get; set; }


        /// <summary>
        /// Each entity can have an open-ended set of tags applied to it, that help users find, markup, and display its information
        /// </summary>
        [DataObjectField(false)]
        [XmlAttribute]
        public IEnumerable<MetadataTag> Tags { get; set; }


        /// <summary>
        /// If this object is not a translation this field will be null. 
        /// If this object is a translation, this id references the parent object.
        /// </summary>
        [DataObjectField(true)]
        [XmlAttribute]
        public TIdType TranslatedFromId
        {
            get { return translatedFromId; }
            set
            {
                translatedFromId = value;
            }
        }

        /// <summary>
        /// The id of the tenant that domain entity this belongs to (null if not known/applicable)
        /// </summary>
        [DataObjectField(false)]
        [XmlAttribute]
        [ForeignKey(nameof(TenantId))]
        public int? TenantId
        {
            get { return tenantId; }
            set
            {
                tenantId = value;
                Metadata.TenantId = value;
            }
        }


        #region Implementation of ASP.NET Boilerplate's domain entity interfaces

        [DataObjectField(false)]
        [XmlAttribute]
        public bool IsActive
        {
            get { return isActive; }
            set
            {
                isActive = value;
                Metadata.IsActive = value;
            }
        }

        #endregion


        /// <summary>
        /// A convenience readonly method to get a <see cref="CultureInfo">CultureInfo</see> instance from the current 
        /// culture code
        /// </summary>
        [DataObjectField(false)]
        [XmlAttribute]
        [NotMapped]
        public CultureInfo GetCultureInfo { get { return new CultureInfo(Culture); } }

        /// <summary>  
        /// Initializes a new instance of the <see cref="DomainEntityBase">Entity</see> class
        /// </summary>
        protected DomainEntityBase() : base()
        {
            Metadata = new MetadataInformation();
            Culture = ApplicationInformation<TIdType>.DEFAULT_CULTURE;
            TenantId = 1; // default tenant
            Tags = new List<MetadataTag>();
            IsDeleted = false;
            IsActive = true; 
            Name = String.Empty;
        }

        /// <summary>  
        /// Initializes a new instance of the <see cref="DomainEntityBase">Entity</see> class
        /// </summary>
        protected DomainEntityBase(int? tenantId) : base()
        {
            Metadata = new MetadataInformation();
            Culture = ApplicationInformation<TIdType>.DEFAULT_CULTURE;
            if (tenantId != null)
            { 
                TenantId = tenantId; 
            } 
            else
            {
                TenantId = 1;
            }
            CreatorUserId = 1; // TODO - default user account?
            Name = String.Empty;
            IsDeleted = false;
            IsActive = true;
        }


        /// <summary>
        /// Creates a new instance of the <see cref="DomainEntityBase">Entity</see> class given a key, and some metadata. 
        /// </summary>
        /// <param name="culture">The culture for this entity</param>
        protected DomainEntityBase(int? tenantId, TIdType id) : base()
        {
            Metadata = new MetadataInformation();
            Id = id;
            Culture = ApplicationInformation<TIdType>.DEFAULT_CULTURE;
            if (tenantId != null)
            {
                TenantId = tenantId;
            }
            else
            {
                TenantId = 1;
            }
            
            CreatorUserId = 1; // TODO - default user account?
            IsDeleted = false;
            IsActive = true;
            Tags = new List<MetadataTag>();
            Name = String.Empty;
        }

        /// <summary>
        /// Creates a new instance of the <see cref="DomainEntityBase">Entity</see> class given a key, and some metadata. 
        /// </summary>
        /// <param name="culture">The culture for this entity</param>
        protected DomainEntityBase(int? tenantId, TIdType id, string culture) : base()
        {
            Metadata = new MetadataInformation();
            Id = id;
            Culture = culture;
            if (tenantId != null)
            {
                TenantId = tenantId;
            }
            else
            {
                TenantId = 1;
            }
            CreatorUserId = 1; // TODO - default user account?
            IsDeleted = false;
            IsActive = true;
            Tags = new List<MetadataTag>();
            Name = String.Empty;
        }

        /// <summary>
        /// Creates a new instance of the <see cref="DomainEntityBase">Entity</see> class given a key, and using the exact values
        /// passed in via the metadata object.
        /// </summary>
        /// <param name="culture">The culture for this entity</param>
        /// <param name="metadata">The metadata containing the values for this entity</param>
        protected DomainEntityBase(int? tenantId, TIdType id, MetadataInformation metadata) : base()
        {
            if (tenantId != null)
            {
                TenantId = tenantId;
            }
            else
            {
                TenantId = 1;
            }
            Id = id;
            Culture = metadata.Culture;
            // populate the fields with any values that are passed in via the metadata
            CreatorUserId = metadata.CreatorUserId;
            CreationTime = metadata.CreationTime;
            descriptionShort = metadata.DescriptionShort;
            descriptionFull = metadata.DescriptionFull;
            Name = metadata.Name;
            LastModificationTime = metadata.LastModificationTime;
            LastModifierUserId = metadata.LastModifierUserId;
            DeleterUserId = metadata.DeleterUserId;
            DeletionTime = metadata.DeletionTime;
            IsDeleted = metadata.IsDeleted;
            IsActive = metadata.IsActive;
            Tags = new List<MetadataTag>();

        }

        /// <summary>
        /// Serialization constructor used for deserialization
        /// </summary>
        /// <param name="info">The serialization info</param>
        /// <param name="context">The context of the stream</param>
        protected DomainEntityBase(SerializationInfo info, StreamingContext context)
        {
            Id = (TIdType)info.GetValue("Id", typeof(TIdType));
            Culture = info.GetString("Culture");
            TenantId = info.GetInt32("TenantId");
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
            //Metadata = (MetadataInformation)info.GetValue("Metadata", typeof(MetadataInformation));

        }

        /// <summary>
        /// The method required for implementing ISerializable
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
        public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Id", Id);
            info.AddValue("Culture", Culture);
            info.AddValue("TenantId", TenantId);
            info.AddValue("DisplayName", Name);
            info.AddValue("DescriptionShort", DescriptionShort);
            info.AddValue("DescriptionFull", DescriptionFull);
            info.AddValue("Tags", Tags);
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
            return Metadata.Name.CompareTo(other.Metadata.Name);
        }

        /// <summary>  
        /// Displays information about the <c>Field</c> in readable format.  
        /// </summary>  
        /// <returns>A string representation of the object.</returns>
        public override String ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("[DomainEntity: ");
            sb.Append(ToStringBaseProperties());
            sb.Append("]");
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
            sb.AppendFormat("TenantId={0};", TenantId);
            sb.AppendFormat("Metadata={0};", Metadata);
            sb.AppendFormat("IsActive={0};", IsActive);
            sb.AppendFormat("IsDeleted={0};", IsDeleted);
            sb.AppendFormat("DeleterUserId={0};", DeleterUserId);
            sb.AppendFormat("DeletionTime={0};", DeletionTime);
            sb.AppendFormat("TranslatedFromId={0};", TranslatedFromId);
            sb.AppendFormat(" Tags={0};", Tags.ToString());
            return sb.ToString();
        }

        /// <summary>
        /// Override the legacy Equals. Must cast obj in this case.
        /// </summary>
        /// <param name="obj">A type to check equivalency of (hopefully) an Entity</param>
        /// <returns>True if the entities are the same according to business key value</returns>
        public override bool Equals(object obj)
        {
            if (obj != null && obj is DomainEntityBase<TIdType>)
            {
                return Equals(obj as DomainEntityBase<TIdType>);
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
        /// <returns></returns>
        public virtual bool Equals(DomainEntityBase<TIdType> obj)
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
                    // For safe equality we need to match on business key equality.
                    // Base domain entities are functionally equal if their key and metadata are equal.
                    // Subclasses should extend to include their own enhanced equality checks, as required.
                    return Id.Equals(obj.Id) && Culture.Equals(obj.Culture)
                        && IsActive.Equals(obj.IsActive) && IsDeleted.Equals(obj.IsDeleted)
                        && TenantId.Equals(obj.TenantId);
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
        public static bool operator ==(DomainEntityBase<TIdType> x, DomainEntityBase<TIdType> y)
        {
            if (System.Object.ReferenceEquals(x, null))
            {
                if (System.Object.ReferenceEquals(y, null))
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
        public static bool operator !=(DomainEntityBase<TIdType> x, DomainEntityBase<TIdType> y)
        {
            return !(x == y);
        }

        /// <summary>  
        /// Computes and retrieves a hash code for an object.  
        /// </summary>  
        /// <remarks>  
        /// This method implements the <see cref="Object">Object</see> method.  
        /// </remarks>  
        /// <returns>A hash code for an object.</returns>
        public override int GetHashCode()
        {
            return Culture.GetHashCode()
                + Id.GetHashCode()
                + TenantId.GetHashCode();
        }

    }
}
