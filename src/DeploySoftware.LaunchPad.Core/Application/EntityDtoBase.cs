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

using Abp.Application.Services.Dto;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Text;
using System.Xml.Serialization;


namespace DeploySoftware.LaunchPad.Core.Application
{
    /// <summary>
    /// Represents a Data Transfer Object
    /// </summary>
    /// <typeparam name="TIdType">The type of the Id</typeparam>
    public abstract class EntityDtoBase<TIdType> : EntityDto<TIdType>,
        IComparable<EntityDtoBase<TIdType>>, IEquatable<EntityDtoBase<TIdType>>
    {
        /// <summary>
        /// The id field for this object. Inherited from ABP. 
        /// Note that for our LaunchPad framework we actually require both an id and a culture field to uniquely identify an entity. 
        /// In the Domain Entities, this functionality is encapsulated in the <see cref="IKey">IKey</see> property.
        /// To keep our DTO simple, we will pass around Id and Culture as individual fields.
        /// </summary>
        [DataObjectField(true)]
        [XmlAttribute]
        [Required]
        public new TIdType Id { get; set; }

        /// <summary>
        /// The culture of this object
        /// </summary>
        [DataObjectField(true)]
        [XmlAttribute]
        [Required]
        public String Culture { get; set; }

        /// <summary>
        /// The id of the tenant that domain entity this belongs to (null if not known/applicable)
        /// </summary>
        [DataObjectField(false)]
        [XmlAttribute]
        [ForeignKey(nameof(TenantId))]
        public int? TenantId { get; set; }

        /// <summary>
        /// The display name that can be displayed as a label externally to users when referring to this object
        /// (rather than using a GUID, which is unfriendly but unique)
        /// </summary>
        [DataObjectField(false)]
        [XmlAttribute]
        public virtual String DisplayName { get; set; }

        /// <summary>
        /// A full description of this item.
        /// </summary>
        [DataObjectField(false)]
        [XmlAttribute]
        public virtual String DescriptionFull { get; set; }

        /// <summary>
        /// A short description of this item.
        /// </summary>
        [DataObjectField(false)]
        [XmlAttribute]
        public virtual String DescriptionShort { get; set; }

        /// <summary>
        /// The date and time that this object was created.
        /// </summary>
        [DataObjectField(false)]
        [XmlAttribute]
        public virtual DateTime CreationTime { get; set; }

        /// <summary>
        /// The id of the User Agent which created this entity
        /// </summary>
        [DataObjectField(false)]
        [XmlAttribute]
        [ForeignKey(nameof(CreatorUserId))]
        public virtual long? CreatorUserId { get; set; }

        /// <summary>
        /// The date and time that the location and/or properties of this object were last modified.
        /// </summary>
        [DataObjectField(false)]
        [XmlAttribute]
        public virtual DateTime? LastModificationTime { get; set; }

        /// <summary>
        /// The id of the User Agent which last modified this object.
        /// </summary>
        [DataObjectField(false)]
        [XmlAttribute]
        [ForeignKey(nameof(LastModifierUserId))]
        public virtual Int64? LastModifierUserId { get; set; }


        /// <summary>
        /// The date and time that this object was deleted.
        /// </summary>
        [DataObjectField(false)]
        [XmlAttribute]
        public virtual DateTime? DeletionTime { get; set; }

        /// <summary>
        /// The id of the user which deleted this entity
        /// </summary>
        [DataObjectField(false)]
        [XmlAttribute]
        [ForeignKey(nameof(DeleterUserId))]
        public virtual long? DeleterUserId { get; set; }

        [DataObjectField(false)]
        [XmlAttribute]
        public bool IsActive { get; set; }

        [DataObjectField(false)]
        [XmlAttribute]
        public bool IsDeleted { get; set; }

        #region "Constructors"
        
        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="tenantId"></param>
        protected EntityDtoBase() : base()
        {

        }

        /// <summary>
        /// Default constructor where the tenant id is known
        /// </summary>
        /// <param name="tenantId"></param>
        public EntityDtoBase(int? tenantId) : base()
        {

        }

        public EntityDtoBase(int? tenantId, TIdType id) : base()
        {
           
        }
     
        /// <summary>
        /// Serialization constructor used for deserialization
        /// </summary>
        /// <param name="info">The serialization info</param>
        /// <param name="context">The context of the stream</param>
        protected EntityDtoBase(SerializationInfo info, StreamingContext context) : base()
        {
            Id = (TIdType)info.GetValue("Id", typeof(TIdType));
            Culture = info.GetString("Culture");
            TenantId = info.GetInt32("TenantId");
            DisplayName = info.GetString("DisplayName");
            DescriptionFull = info.GetString("DescriptionFull");
            DescriptionShort = info.GetString("DescriptionShort");
            CreationTime = info.GetDateTime("CreationTime");
            CreatorUserId = info.GetInt64("CreatorUserId");
            LastModifierUserId = info.GetInt64("LastModifierUserId");
            LastModificationTime = info.GetDateTime("LastModificationTime");
            DeletionTime = info.GetDateTime("DeletionTime");
            DeleterUserId = info.GetInt64("DeleterUserId");
            IsDeleted = info.GetBoolean("IsDeleted");
            IsActive = info.GetBoolean("IsActive");
        }

#endregion

        /// <summary>
        /// The method required for implementing ISerializable
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Id", Id);
            info.AddValue("Culture", Culture);
            info.AddValue("TenantId", TenantId);
            info.AddValue("DisplayName", DisplayName);
            info.AddValue("DescriptionFull", DescriptionFull);
            info.AddValue("DescriptionShort", DescriptionShort);
            info.AddValue("CreationTime", CreationTime);
            info.AddValue("CreatorUserId", CreatorUserId);
            info.AddValue("LastModifierUserId", LastModifierUserId);
            info.AddValue("LastModificationTime", LastModificationTime);
            info.AddValue("DeleterUserId", DeleterUserId);
            info.AddValue("DeletionTime", DeletionTime);
            info.AddValue("IsDeleted", IsDeleted);
            info.AddValue("IsActive", IsActive);
        }

        /// <summary>  
        /// Displays information about the class in readable format.  
        /// </summary>  
        /// <returns>A string representation of the object.</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("[EntityDtoBase : ");
            sb.AppendFormat("Id={0};", Id);
            sb.AppendFormat("Culture={0};", Culture);
            sb.AppendFormat("TenantId={0};", TenantId);
            sb.AppendFormat("CreatorId={0};", CreatorUserId);
            sb.AppendFormat(" DisplayName={0};", DisplayName);
            if (!String.IsNullOrEmpty(DescriptionFull))
            {
                sb.AppendFormat(" DescriptionFull={0};", DescriptionFull);
            }
            if (!String.IsNullOrEmpty(DescriptionShort))
            {
                sb.AppendFormat(" DescriptionShort={0};", DescriptionShort);
            }
            sb.AppendFormat(" LastModifiedById={0};", LastModifierUserId);

            sb.AppendFormat(" DateCreated={0};", CreationTime);
            sb.AppendFormat(" DateLastModified={0};", LastModificationTime);
            sb.AppendFormat(" IsActive={0};", IsActive);
            sb.AppendFormat(" IsDeleted={0};", IsDeleted);
            sb.Append("]");
            return sb.ToString();
        }

        /// <summary>
        /// Shallow clones the entity
        /// </summary>
        /// <typeparam name="TEntity">The source entity to clone</typeparam>
        /// <returns>A shallow clone of the entity and its serializable properties</returns>
        protected virtual TEntity Clone<TEntity>() where TEntity : EntityDtoBase<TIdType>, new()
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
        public virtual int CompareTo(EntityDtoBase<TIdType> other)
        {
            // put comparison of properties in here 
            // for base object we'll just sort by title
            return DisplayName.CompareTo(other.DisplayName);
        }

        /// <summary>
        /// Override the legacy Equals. Must cast obj in this case.
        /// </summary>
        /// <param name="obj">A type to check equivalency of (hopefully) an Entity</param>
        /// <returns>True if the entities are the same according to business key value</returns>
        public override bool Equals(object obj)
        {
            if (obj != null && obj is EntityDtoBase<TIdType>)
            {
                return Equals(obj as EntityDtoBase<TIdType>);
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
        public virtual bool Equals(EntityDtoBase<TIdType> obj)
        {
            if (obj != null)
            {
                // For safe equality we need to match on business key equality.
                // Base domain entities are functionally equal if their key and metadata are equal.
                // Subclasses should extend to include their own enhanced equality checks, as required.
                return Id.Equals(obj.Id) && Culture.Equals(obj.Culture) && IsActive.Equals(obj.IsActive)
                    && IsDeleted.Equals(obj.IsDeleted) && TenantId.Equals(obj.TenantId);
            }
            return false;
        }

        /// <summary>
        /// Override the == operator to test for equality
        /// </summary>
        /// <param name="x">The first value</param>
        /// <param name="y">The second value</param>
        /// <returns>True if both objects are fully equal based on the Equals logic</returns>
        public static bool operator ==(EntityDtoBase<TIdType> x, EntityDtoBase<TIdType> y)
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
        public static bool operator !=(EntityDtoBase<TIdType> x, EntityDtoBase<TIdType> y)
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
                + DisplayName.GetHashCode()
                + LastModifierUserId.GetHashCode()
                + LastModificationTime.GetHashCode();
        }

    }
}
