﻿// ***********************************************************************
// Assembly         : Deploy.LaunchPad.Core.Abp
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 10-27-2023
// ***********************************************************************
// <copyright file="TenantSpecificAggregateRootBase.cs" company="Deploy Software Solutions, inc.">
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

using Abp.Domain.Entities;
using Deploy.LaunchPad.Core.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Text;
using System.Xml.Serialization;

namespace Deploy.LaunchPad.Core.Abp.Domain.Model
{

    /// <summary>
    /// Base class for Aggregate Root entities that must be specifically related to tenants.
    /// Inherits from <see cref="LaunchPadAggregateRootBase{TIdType}{TIdType}">AggregateRootBase{TIdType}</see> and provides
    /// base functionality for many of its methods.
    /// Implements AspNetBoilerplate's <see cref="IMustHaveTenant">IMustHaveTenant interface</see>, overriding the base interface where tenant may or may not exist.
    /// </summary>
    /// <typeparam name="TIdType">The type of the t identifier type.</typeparam>
    [Serializable]
    public abstract partial class TenantSpecificAggregateRootBase<TIdType> :
        LaunchPadAggregateRootBase<TIdType>, IMustHaveTenant

    {

        protected int _tenantId;
        /// <summary>
        /// The id of the tenant that domain entity this belongs to
        /// </summary>
        /// <value>The tenant identifier.</value>
        [DataObjectField(false)]
        [XmlAttribute]
        [Required]
        [ForeignKey(nameof(TenantId))]
        public virtual int TenantId
        {
            get { return _tenantId; }
            set { _tenantId = value; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AggregateRootBase">AggregateRootBase</see> class
        /// </summary>
        protected TenantSpecificAggregateRootBase() : base()
        {
            TenantId = 1;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AggregateRootBase">AggregateRootBase</see> class
        /// </summary>
        /// <param name="tenantId">The tenant identifier.</param>
        protected TenantSpecificAggregateRootBase(int tenantId) : base()
        {
            TenantId = tenantId;
        }

        /// <summary>
        /// Creates a new instance of the <see cref="TenantSpecificAggregateRootBase">TenantSpecificAggregateRootBase</see> class given a key, and some metadata.
        /// </summary>
        /// <param name="tenantId">The tenant identifier.</param>
        /// <param name="id">The identifier.</param>
        /// <param name="cultureName">The culture for this entity</param>
        protected TenantSpecificAggregateRootBase(int tenantId, TIdType id, string cultureName) : base(id, cultureName)
        {
            TenantId = tenantId;
        }

        /// <summary>
        /// Creates a new instance of the <see cref="TenantSpecificAggregateRootBase">TenantSpecificAggregateRootBase</see> class given a key, and some metadata.
        /// </summary>
        /// <param name="tenantId">The tenant identifier.</param>
        /// <param name="id">The identifier.</param>
        /// <param name="metadata">The desired metadata for this entity</param>
        protected TenantSpecificAggregateRootBase(int tenantId, TIdType id, MetadataInformation metadata) : base(id, metadata.Culture)
        {

            TenantId = tenantId;
        }

        /// <summary>
        /// Serialization constructor used for deserialization
        /// </summary>
        /// <param name="info">The serialization info</param>
        /// <param name="context">The context of the stream</param>
        protected TenantSpecificAggregateRootBase(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            TenantId = info.GetInt32("TenantId");
        }

        /// <summary>
        /// The method required for implementing ISerializable
        /// </summary>
        /// <param name="info">The information.</param>
        /// <param name="context">The context.</param>
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue("TenantId", TenantId);
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
        /// <returns>System.Int32.</returns>
        public virtual int CompareTo(TenantSpecificAggregateRootBase<TIdType> other)
        {
            // put comparison of properties in here 
            // for base object we'll just sort by title
            return Name.CompareTo(other.Name);
        }

        /// <summary>
        /// Displays information about the <c>Field</c> in readable format.
        /// </summary>
        /// <returns>A string representation of the object.</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            //sb.AppendFormat(base.ToStringBaseProperties());
            sb.AppendFormat("DomainEvents={0};", DomainEvents);
            sb.AppendFormat("TenantId={0};", TenantId);
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
        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
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
                    return Id.Equals(obj.Id) && Culture.Equals(obj.Culture) && TenantId.Equals(obj.TenantId);
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
            if (ReferenceEquals(x, null))
            {
                if (ReferenceEquals(y, null))
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
        /// <returns>A hash code for an object.</returns>
        /// <remarks>This method implements the <see cref="object">Object</see> method.</remarks>
        public override int GetHashCode()
        {
            return Culture.GetHashCode() + Id.GetHashCode() + TenantId.GetHashCode();
        }

    }
}
