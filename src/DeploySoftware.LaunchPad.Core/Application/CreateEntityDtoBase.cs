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
    /// Represents the base properties a LaunchPad Data Transfer Object would possess in order to create an entity
    /// It does not include properties that are likely to be set on creating by ABP, such as Creator information, or 
    /// ABP properties that are not likely to be set, such as Deletion or Last Modified information.
    /// Of course subclassing DTOs may contain additional properties.
    /// </summary>
    /// <typeparam name="TIdType">The type of the Id</typeparam>
    public abstract class CreateEntityDtoBase<TIdType> : MinimalEntityDtoBase<TIdType>,
        IHasCreationTime, ICreationAudited, IPassivable,
        IEquatable<CreateEntityDtoBase<TIdType>>
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

        [DataObjectField(false)]
        [XmlAttribute]
        public virtual bool IsActive { get; set; }


        #region "Constructors"

        /// <summary>
        /// Default constructor
        /// </summary>
        protected CreateEntityDtoBase() : base()
        {
            CreatorUserId = DEFAULT_CREATOR_USER_ID; // TODO - default user account?
            IsActive = true;
        }

        /// <summary>
        /// Default constructor where the id is known
        /// <param name="id">The id of the  entity being created</param>
        /// </summary>
        public CreateEntityDtoBase(TIdType id) : base(id)
        {
            CreatorUserId = DEFAULT_CREATOR_USER_ID; // TODO - default user account?
            IsActive = true;
        }

        public CreateEntityDtoBase( TIdType id, string culture) : base( id,culture)
        {
            CreatorUserId = DEFAULT_CREATOR_USER_ID; // TODO - default user account?
            IsActive = true;
        }

        /// <summary>
        /// Serialization constructor used for deserialization
        /// </summary>
        /// <param name="info">The serialization info</param>
        /// <param name="context">The context of the stream</param>
        protected CreateEntityDtoBase(SerializationInfo info, StreamingContext context) : base(info, context)
        {

            DescriptionFull = info.GetString("DescriptionFull");
            TranslatedFromId = (TIdType)info.GetValue("TranslatedFromId", typeof(TIdType));
            IsActive = info.GetBoolean("IsActive");
            CreationTime = info.GetDateTime("CreationTime");
            CreatorUserId = info.GetInt64("CreatorUserId");
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
            info.AddValue("DescriptionFull", DescriptionFull);
            info.AddValue("DescriptionShort", DescriptionShort);
            info.AddValue("TranslatedFromId", TranslatedFromId);
            info.AddValue("CreationTime", CreationTime);
            info.AddValue("CreatorUserId", CreatorUserId);
            info.AddValue("IsActive", IsActive);
        }

        /// <summary>  
        /// Displays information about the class in readable format.  
        /// </summary>  
        /// <returns>A string representation of the object.</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("[CreateEntityDtoBase : ");
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
            // LaunchPAD RAD properties
            sb.AppendFormat("Id={0};", Id);
            sb.AppendFormat("Name={0};", Name);
            sb.AppendFormat("DescriptionShort={0};", DescriptionShort);
            sb.AppendFormat("DescriptionFull={0};", DescriptionFull);
            sb.AppendFormat("TranslatedFromId={0};", TranslatedFromId);
            //sb.AppendFormat(" Tags={0};", Tags.ToString());
            // ABP properties
            sb.AppendFormat("IsActive={0};", IsActive); 
            sb.AppendFormat("CreationTime={0};", CreationTime);
            sb.AppendFormat("CreatorUserId={0};", CreatorUserId);            
            return sb.ToString();
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
        public virtual bool Equals(CreateEntityDtoBase<TIdType> obj)
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
