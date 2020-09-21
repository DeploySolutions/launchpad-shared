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

namespace DeploySoftware.LaunchPad.Shared.Domain
{
    using Abp.Domain.Entities;
    using System;
    using System.Runtime.Serialization;
    using System.Security.Permissions;
    using System.Text;
    using System.ComponentModel;
    using System.Xml.Serialization;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// Base class for Aggregate Root entities that must be specifically related to tenants. 
    /// Inherits from <see cref="AggregateRootBase{TIdType}{TIdType}">AggregateRootBase{TIdType}</see> and provides
    /// base functionality for many of its methods. 
    /// Implements AspNetBoilerplate's <see cref="IMustHaveTenant">IMustHaveTenant interface</see>, overriding the base interface where tenant may or may not exist.
    /// </summary>
    public abstract partial class TenantSpecificAggregateRootBase<TIdType> :
        AggregateRootBase<TIdType>, IMustHaveTenant

    {

        /// <summary>
        /// The id of the tenant that domain entity this belongs to
        /// </summary>
        [DataObjectField(false)]
        [XmlAttribute]
        [Required]
        [ForeignKey(nameof(TenantId))]
        public new int TenantId { get; set; }

        /// <summary>  
        /// Initializes a new instance of the <see cref="AggregateRootBase">AggregateRootBase</see> class
        /// </summary>
        protected TenantSpecificAggregateRootBase(int tenantId) : base(tenantId)
        {

        }

        /// <summary>
        /// Creates a new instance of the <see cref="TenantSpecificAggregateRootBase">TenantSpecificAggregateRootBase</see> class given a key, and some metadata. 
        /// </summary>
        /// <param name="cultureName">The culture for this entity</param>
        protected TenantSpecificAggregateRootBase(int tenantId, TIdType id, string cultureName) : base(tenantId, id, cultureName)
        {

        }

        /// <summary>
        /// Creates a new instance of the <see cref="TenantSpecificAggregateRootBase">TenantSpecificAggregateRootBase</see> class given a key, and some metadata. 
        /// </summary>
        /// <param name="cultureName">The culture for this entity</param>
        /// <param name="metadata">The desired metadata for this entity</param>
        protected TenantSpecificAggregateRootBase(int tenantId, DomainEntityKey<TIdType> key, MetadataInformation metadata) : base(tenantId, key,metadata)
        {

        }

        /// <summary>
        /// Serialization constructor used for deserialization
        /// </summary>
        /// <param name="info">The serialization info</param>
        /// <param name="context">The context of the stream</param>
        protected TenantSpecificAggregateRootBase(SerializationInfo info, StreamingContext context) : base(info, context)
        {

        }

        /// <summary>
        /// The method required for implementing ISerializable
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue("DomainEvents", DomainEvents);
        }

        /// <summary>
        /// Event called once deserialization constructor finishes.
        /// Useful for reattaching connections and other finite resources that 
        /// can't be serialized and deserialized.
        /// </summary>
        /// <param name="sender">The object that has been deserialized</param>
        public override void OnDeserialization(object sender)
        {
            // reconnect connection strings and other resources that won't be serialized
        }

        /// <summary>
        /// Comparison method between two objects of the same type, used for sorting.
        /// Because the CompareTo method is strongly typed by generic constraints,
        /// it is not necessary to test for the correct object type.
        /// </summary>
        /// <param name="other">The other object of this type we are comparing to</param>
        /// <returns></returns>
        public virtual int CompareTo(TenantSpecificAggregateRootBase<TIdType> other)
        {
            // put comparison of properties in here 
            // for base object we'll just sort by title
            return Metadata.DisplayName.CompareTo(other.Metadata.DisplayName);
        }

        /// <summary>  
        /// Displays information about the <c>Field</c> in readable format.  
        /// </summary>  
        /// <returns>A string representation of the object.</returns>
        public override String ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat(base.ToStringBaseProperties());
            sb.AppendFormat("DomainEvents={0};", DomainEvents);
            return sb.ToString();
        }

        /// <summary>
        /// Override the legacy Equals. Must cast obj in this case.
        /// </summary>
        /// <param name="obj">A type to check equivalency of (hopefully) an Entity</param>
        /// <returns>True if the entities are the same according to business key value</returns>
        public override bool Equals(object obj)
        {
            if (obj != null && obj is TenantSpecificAggregateRootBase<TIdType>)
            {
                return Equals(obj as TenantSpecificAggregateRootBase<TIdType>);
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
        public virtual bool Equals(TenantSpecificAggregateRootBase<TIdType> obj)
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
                    return Id.Equals(obj.Id) && Key.Culture.Equals(obj.Key.Culture) && Metadata.Equals(obj.Metadata);
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
        public static bool operator ==(TenantSpecificAggregateRootBase<TIdType> x, TenantSpecificAggregateRootBase<TIdType> y)
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
        public static bool operator !=(TenantSpecificAggregateRootBase<TIdType> x, TenantSpecificAggregateRootBase<TIdType> y)
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
            return Key.Culture.GetHashCode()+Id.GetHashCode();
        }

    }
}