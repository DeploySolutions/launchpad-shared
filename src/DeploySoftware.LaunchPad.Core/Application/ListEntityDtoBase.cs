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

using Abp.Application.Services.Dto;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Text;
using System.Xml.Serialization;


namespace DeploySoftware.LaunchPad.Core.Application
{
    /// <summary>
    /// Represents the base properties a LaunchPad Data Transfer Object would show in a list.
    /// Of course subclassing DTOs will contain additional properties.
    /// </summary>
    /// <typeparam name="TIdType">The type of the Id</typeparam>
    public abstract partial class ListEntityDtoBase<TIdType> : MinimalEntityDtoBase<TIdType>,
        IHasCreationTime, ICreationAudited, IHasModificationTime, IModificationAudited, IPassivable,
        IComparable<ListEntityDtoBase<TIdType>>, IEquatable<ListEntityDtoBase<TIdType>>
    {
   
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
        protected ListEntityDtoBase() : base()
        {
            IsActive = true;
        }

        /// <summary>
        /// Default constructor where the tenant id is known
        /// </summary>
        public ListEntityDtoBase(TIdType id) : base(id)
        {
            IsActive = true;
        }

        public ListEntityDtoBase( TIdType id, string culture) : base( id,culture)
        {
            IsActive = true;
        }

        /// <summary>
        /// Serialization constructor used for deserialization
        /// </summary>
        /// <param name="info">The serialization info</param>
        /// <param name="context">The context of the stream</param>
        protected ListEntityDtoBase(SerializationInfo info, StreamingContext context) : base(info, context)
        {

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
            info.AddValue("Id", Id);
            info.AddValue("Culture", Culture);
            info.AddValue("DisplayName", Name);
            info.AddValue("DescriptionShort", DescriptionShort);
            info.AddValue("TranslatedFromId", TranslatedFromId);
            info.AddValue("CreationTime", CreationTime);
            info.AddValue("CreatorUserId", CreatorUserId);
            info.AddValue("LastModifierUserId", LastModifierUserId);
            info.AddValue("LastModificationTime", LastModificationTime);
            info.AddValue("IsActive", IsActive);
        }

        /// <summary>  
        /// Displays information about the class in readable format.  
        /// </summary>  
        /// <returns>A string representation of the object.</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("[ListEntityDtoBase : ");
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
            // ABP properties
            return sb.ToString();
        }

        /// <summary>
        /// Comparison method between two objects of the same type, used for sorting.
        /// Because the CompareTo method is strongly typed by generic constraints,
        /// it is not necessary to test for the correct object type.
        /// </summary>
        /// <param name="other">The other object of this type we are comparing to</param>
        /// <returns></returns>
        public virtual int CompareTo(ListEntityDtoBase<TIdType> other)
        {
            // put comparison of properties in here 
            // for base object we'll just sort by title
            return Name.CompareTo(other.Name);
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
        public virtual bool Equals(ListEntityDtoBase<TIdType> obj)
        {
            if (obj != null)
            {
                // For safe equality we need to match on business key equality.
                // Base domain entities are functionally equal if their key and metadata are equal.
                // Subclasses should extend to include their own enhanced equality checks, as required.
                return Id.Equals(obj.Id) && Culture.Equals(obj.Culture) && IsActive.Equals(obj.IsActive);
            }
            return false;
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
            return Id.GetHashCode() + Culture.GetHashCode() + Name.GetHashCode() + DescriptionShort.GetHashCode();
        }
    }
}