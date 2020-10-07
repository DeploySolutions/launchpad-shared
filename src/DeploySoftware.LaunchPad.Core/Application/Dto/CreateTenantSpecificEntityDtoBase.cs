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
    public abstract partial class CreateTenantSpecificEntityDtoBase<TIdType> : 
        CreateEntityDtoBase<TIdType>, IMustHaveTenant
    {

        /// <summary>
        /// The id of the tenant that domain entity this belongs to
        /// </summary>
        [DataObjectField(false)]
        [XmlAttribute]
        [Required]
        [ForeignKey(nameof(TenantId))]
        public new int TenantId { get; set; }

        #region "Constructors"

        /// <summary>
        /// Default constructor
        /// </summary>
        protected CreateTenantSpecificEntityDtoBase() : base()
        {
            
        }

        /// <summary>
        /// Default constructor where the id is known
        /// <param name="id">The id of the  entity being created</param>
        /// </summary>
        public CreateTenantSpecificEntityDtoBase(int tenantId, TIdType id) : base(tenantId, id)
        {
            TenantId = tenantId;
        }

        public CreateTenantSpecificEntityDtoBase(int tenantId, TIdType id, string culture) : base(tenantId, id,culture)
        {
            TenantId = tenantId;
            Id = id;
            Culture = culture;
        }

        /// <summary>
        /// Serialization constructor used for deserialization
        /// </summary>
        /// <param name="info">The serialization info</param>
        /// <param name="context">The context of the stream</param>
        protected CreateTenantSpecificEntityDtoBase(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            TenantId = info.GetInt32("TenantId");
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
            info.AddValue("TenantId", TenantId);
        }

        /// <summary>  
        /// Displays information about the class in readable format.  
        /// </summary>  
        /// <returns>A string representation of the object.</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("[CreateTenantSpecificEntityDtoBase : ");
            sb.Append(ToStringBaseProperties());
            sb.AppendFormat("TenantId={0};", TenantId);
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
        public virtual int CompareTo(CreateTenantSpecificEntityDtoBase<TIdType> other)
        {
            // put comparison of properties in here 
            // for base object we'll just sort by name and description short
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
        public virtual bool Equals(CreateTenantSpecificEntityDtoBase<TIdType> obj)
        {
            if (obj != null)
            {
                // For safe equality we need to match on business key equality.
                // Base domain entities are functionally equal if their key and metadata are equal.
                // Subclasses should extend to include their own enhanced equality checks, as required.
                return Id.Equals(obj.Id) && Culture.Equals(obj.Culture) && TenantId.Equals(obj.TenantId);
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
            return Id.GetHashCode() + Culture.GetHashCode() + Name.GetHashCode() + DescriptionShort.GetHashCode() + TenantId.GetHashCode();
        }
    }
}
