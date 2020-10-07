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

    /// <summary>
    /// Base class for Entities. Implements <see cref="IDomainEntity">IDomainEntity</see> and provides
    /// base functionality for many of its methods. Inherits from AspNetBoilerplate's Entity class.
    /// Implements AspNetBoilerplate's auditing interfaces.
    /// </summary>
    public abstract partial class DomainEntityBase<TIdType> : 
        FullAuditedEntity<TIdType>, IDomainEntity<TIdType>
        
    {

        /// <summary>
        /// The culture of this object
        /// </summary>
        [DataObjectField(true)]
        [XmlAttribute]
        public String Culture { get; set; }

        /// <summary>
        /// The display name of this object
        /// </summary>
        [DataObjectField(false)]
        [XmlAttribute]
        public String Name { get; set; }

        /// <summary>
        /// A short description for this entity
        /// </summary>
        [DataObjectField(false)]
        [XmlAttribute]
        public String DescriptionShort { get; set; }

        /// <summary>
        /// The full description for this entity
        /// </summary>
        [DataObjectField(false)]
        [XmlElement]
        public String DescriptionFull { get; set; }

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
        public TIdType TranslatedFromId { get; set; }

        [DataObjectField(false)]
        [XmlAttribute]
        public bool IsActive { get; set; }



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
            Culture = ApplicationInformation<TIdType>.DEFAULT_CULTURE;
            //TenantId = 0; // default tenant
            Tags = new List<MetadataTag>();
            IsDeleted = false;
            IsActive = true; 
            Name = String.Empty;
            DescriptionShort = String.Empty;
            DescriptionFull = String.Empty;

        }

        /// <summary>
        /// Creates a new instance of the <see cref="DomainEntityBase">Entity</see> class given a key, and some metadata. 
        /// </summary>
        /// <param name="culture">The culture for this entity</param>
        protected DomainEntityBase(TIdType id) : base()
        {
            Id = id;
            Culture = ApplicationInformation<TIdType>.DEFAULT_CULTURE;
           
            CreatorUserId = 1; // TODO - default user account?
            IsDeleted = false;
            IsActive = true;
            Tags = new List<MetadataTag>();
            Name = String.Empty;
            DescriptionShort = String.Empty;
            DescriptionFull = String.Empty;
        }

        /// <summary>
        /// Creates a new instance of the <see cref="DomainEntityBase">Entity</see> class given a key, and some metadata. 
        /// </summary>
        /// <param name="culture">The culture for this entity</param>
        protected DomainEntityBase(TIdType id, string culture) : base()
        {
            Id = id;
            Culture = culture;
            CreatorUserId = 1; // TODO - default user account?
            IsDeleted = false;
            IsActive = true;
            Tags = new List<MetadataTag>();
            Name = String.Empty;
            DescriptionShort = String.Empty;
            DescriptionFull = String.Empty;
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
            info.AddValue("Name", Name);
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
            return Name.CompareTo(other.Name);
        }

        /// <summary>  
        /// Displays information about the <c>Field</c> in readable format.  
        /// </summary>  
        /// <returns>A string representation of the object.</returns>
        public override String ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("[DomainEntityBase: ");
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
            // LaunchPAD RAD properties
            sb.AppendFormat("Id={0};", Id);
            sb.AppendFormat("Name={0};", Name);
            sb.AppendFormat("DescriptionShort={0};", DescriptionShort);
            sb.AppendFormat("DescriptionFull={0};", DescriptionFull);
            sb.AppendFormat("TranslatedFromId={0};", TranslatedFromId);
            sb.AppendFormat(" Tags={0};", Tags.ToString());
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
                        && IsActive.Equals(obj.IsActive) && IsDeleted.Equals(obj.IsDeleted);
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
                + Id.GetHashCode();
        }

    }
}
