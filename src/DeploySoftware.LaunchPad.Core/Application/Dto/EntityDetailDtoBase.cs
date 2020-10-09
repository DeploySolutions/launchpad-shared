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

using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Text;
using System.Xml.Serialization;


namespace DeploySoftware.LaunchPad.Core.Application
{
    /// <summary>
    /// Represents the entire amount of base properties a LaunchPad Data Transfer Object would possess.
    /// Of course subclassing DTOs will contain additional properties.
    /// </summary>
    /// <typeparam name="TIdType">The type of the Id</typeparam>
    public abstract partial class EntityDetailDtoBase<TIdType> : EntityDtoBase<TIdType>,
        IHasCreationTime, ICreationAudited, IHasModificationTime, IModificationAudited,
        IPassivable,
        IComparable<EntityDetailDtoBase<TIdType>>, IEquatable<EntityDetailDtoBase<TIdType>>
    {
        public static readonly long? DEFAULT_CREATOR_USER_ID = 1;

        /// <summary>
        /// A full description of this item.
        /// </summary>
        [DataObjectField(false)]
        [XmlAttribute]
        public virtual String DescriptionFull { get; set; }

        /// <summary>
        /// If this object is not a translation this field will be null. 
        /// If this object is a translation, this id references the parent object.
        /// </summary>
        [DataObjectField(true)]
        [XmlAttribute]
        public virtual TIdType TranslatedFromId { get; set; }


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

        [DataObjectField(false)]
        [XmlAttribute]
        public virtual bool IsActive { get; set; }


        #region "Constructors"

        /// <summary>
        /// Default constructor
        /// </summary>
        protected EntityDetailDtoBase() : base()
        {
            DescriptionFull = String.Empty;
            CreatorUserId = DEFAULT_CREATOR_USER_ID;
            IsActive = true;

        }

        /// <summary>
        /// Default constructor where the tenant id is known
        /// </summary>
        public EntityDetailDtoBase(int tenantId, TIdType id) : base(tenantId, id)
        {
            DescriptionFull = String.Empty;
            CreatorUserId = DEFAULT_CREATOR_USER_ID;
            IsActive = true;
        }

        public EntityDetailDtoBase(int tenantId, TIdType id, string culture) : base(tenantId, id,culture)
        {
            DescriptionFull = String.Empty;
            CreatorUserId = DEFAULT_CREATOR_USER_ID; 
            IsActive = true;
        }

        /// <summary>
        /// Serialization constructor used for deserialization
        /// </summary>
        /// <param name="info">The serialization info</param>
        /// <param name="context">The context of the stream</param>
        protected EntityDetailDtoBase(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            DescriptionFull = info.GetString("DescriptionFull");
            TranslatedFromId = (TIdType)info.GetValue("TranslatedFromId", typeof(TIdType));
            IsActive = info.GetBoolean("IsActive");
            CreationTime = info.GetDateTime("CreationTime");
            CreatorUserId = info.GetInt64("CreatorUserId");
            LastModifierUserId = info.GetInt64("LastModifierUserId");
            LastModificationTime = info.GetDateTime("LastModificationTime");
        }


        #endregion


        /// <summary>
        /// The method required for implementing ISerializable
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue("TranslatedFromId", TranslatedFromId);
            info.AddValue("IsActive", IsActive);
            info.AddValue("CreationTime", CreationTime);
            info.AddValue("CreatorUserId", CreatorUserId);
            info.AddValue("LastModifierUserId", LastModifierUserId);
            info.AddValue("LastModificationTime", LastModificationTime);
        }

        /// <summary>  
        /// Displays information about the class in readable format.  
        /// </summary>  
        /// <returns>A string representation of the object.</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("[GetEntityDetailDtoBase : ");
            sb.Append(ToStringBaseProperties());
            sb.Append("]");
            return sb.ToString();
        }

        /// <summary>
        /// This method makes it easy for any child class to generate a ToString() representation of
        /// the common base properties
        /// </summary>
        /// <returns>A string description of the entity</returns>
        protected override String ToStringBaseProperties()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(base.ToStringBaseProperties());
            // LaunchPAD RAD properties
            //
            sb.AppendFormat("DescriptionFull={0};", DescriptionFull);
            sb.AppendFormat("TranslatedFromId={0};", TranslatedFromId);
            // ABP properties
            //           
            sb.AppendFormat("CreationTime={0};", CreationTime);
            sb.AppendFormat("CreatorUserId={0};", CreatorUserId);
            sb.AppendFormat("LastModifierUserId={0};", LastModifierUserId);
            sb.AppendFormat("LastModificationTime={0};", LastModificationTime);
            sb.AppendFormat("IsActive={0};", IsActive);
            return sb.ToString();
        }

        /// <summary>
        /// Shallow clones the entity
        /// </summary>
        /// <typeparam name="TEntity">The source entity to clone</typeparam>
        /// <returns>A shallow clone of the entity and its serializable properties</returns>
        protected new TEntity Clone<TEntity>() where TEntity : EntityDetailDtoBase<TIdType>, new()
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
        public virtual int CompareTo(EntityDetailDtoBase<TIdType> other)
        {
            // put comparison of properties in here 
            // for base object we'll just sort by name and description short
            return Name.CompareTo(other.Name);
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
        public virtual bool Equals(EntityDetailDtoBase<TIdType> obj)
        {
            if (obj != null)
            {
                return Id.Equals(obj.Id) && Culture.Equals(obj.Culture) && TenantId.Equals(obj.TenantId)
                    && TranslatedFromId.Equals(obj.TranslatedFromId);

            }
            return false;
        }

        /// <summary>
        /// Override the == operator to test for equality
        /// </summary>
        /// <param name="x">The first value</param>
        /// <param name="y">The second value</param>
        /// <returns>True if both objects are fully equal based on the Equals logic</returns>
        public static bool operator ==(EntityDetailDtoBase<TIdType> x, EntityDetailDtoBase<TIdType> y)
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
        public static bool operator !=(EntityDetailDtoBase<TIdType> x, EntityDetailDtoBase<TIdType> y)
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
            return Id.GetHashCode() + Culture.GetHashCode() + TenantId.GetHashCode() + TranslatedFromId.GetHashCode();
        }
    }
}
