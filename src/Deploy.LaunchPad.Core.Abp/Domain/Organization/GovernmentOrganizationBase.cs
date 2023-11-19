// ***********************************************************************
// Assembly         : Deploy.LaunchPad.Core.Abp
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 04-21-2023
// ***********************************************************************
// <copyright file="GovernmentOrganizationBase.cs" company="Deploy Software Solutions, inc.">
//     2018-2023 Deploy Software Solutions, inc.
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

using Schema.NET;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using System.Xml.Serialization;

namespace Deploy.LaunchPad.Core.Abp.Domain
{

    /// <summary>
    /// Base class for Entities. Implements <see cref="IDomainEntity">IDomainEntity</see> and provides
    /// base functionality for many of its methods. Inherits from ASP.NET Boilerplate's IEntity interface.
    /// </summary>
    /// <typeparam name="TPrimaryKey">The type of the t primary key.</typeparam>
    public abstract partial class GovernmentOrganizationBase<TPrimaryKey> : OrganizationBase<TPrimaryKey>, IOrganization<TPrimaryKey>
    {

        /// <summary>
        /// The schema
        /// </summary>
        private GovernmentOrganization _schema;
        /// <summary>
        /// Gets or sets the schema.
        /// </summary>
        /// <value>The schema.</value>
        [DataObjectField(false)]
        [XmlAttribute]
        public new GovernmentOrganization Schema { get => _schema; set => _schema = value; }


        #region Implementation of ASP.NET Boilerplate's IEntity interface



        #endregion


        /// <summary>
        /// Initializes a new instance of the <see cref="GovernmentOrganizationBase">GovernmentOrganizationBase</see> class
        /// </summary>
        protected GovernmentOrganizationBase() : base()
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GovernmentOrganizationBase">GovernmentOrganizationBase</see> class
        /// </summary>
        /// <param name="tenantId">The tenant identifier.</param>
        protected GovernmentOrganizationBase(int? tenantId) : base(tenantId)
        {

        }

        /// <summary>
        /// Creates a new instance of the <see cref="GovernmentOrganizationBase">GovernmentOrganizationBase</see> class given an id, and some metadata.
        /// </summary>
        /// <param name="tenantId">The tenant identifier.</param>
        /// <param name="id">The identifier.</param>
        protected GovernmentOrganizationBase(int? tenantId, TPrimaryKey id) : base(tenantId)
        {
            Id = id;
        }

        /// <summary>
        /// Serialization constructor used for deserialization
        /// </summary>
        /// <param name="info">The serialization info</param>
        /// <param name="context">The context of the stream</param>
        protected GovernmentOrganizationBase(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            Schema = (GovernmentOrganization)info.GetValue("Organization", typeof(GovernmentOrganization));
            Offices = (IList<String>)info.GetValue("Offices", typeof(List<String>));
        }

        /// <summary>
        /// The method required for implementing ISerializable
        /// </summary>
        /// <param name="info">The information.</param>
        /// <param name="context">The context.</param>
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue("Schema", Schema);
            info.AddValue("Offices", Offices);
        }


        /// <summary>
        /// Shallow clones the entity
        /// </summary>
        /// <typeparam name="TEntity">The source entity to clone</typeparam>
        /// <returns>A shallow clone of the entity and its serializable properties</returns>
        protected new virtual TEntity Clone<TEntity>() where TEntity : GovernmentOrganizationBase<TPrimaryKey>, new()
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
        /// <returns>System.Int32.</returns>
        public virtual int CompareTo(GovernmentOrganizationBase<TPrimaryKey> other)
        {
            if (other == null) return 1;
            return FullName.CompareTo(other.FullName);
        }

        /// <summary>
        /// Displays information about the <c>Field</c> in readable format.
        /// </summary>
        /// <returns>A string representation of the object.</returns>
        public override String ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("[GovernmentOrganizationBase: ");
            //  sb.Append(base.ToStringBaseProperties());
            sb.Append(']');
            return sb.ToString();
        }

        /// <summary>
        /// Override the legacy Equals. Must cast obj in this case.
        /// </summary>
        /// <param name="obj">A type to check equivalency of (hopefully) an Entity</param>
        /// <returns>True if the entities are the same according to business key value</returns>
        public override bool Equals(object obj)
        {
            if (obj != null && obj is GovernmentOrganizationBase<TPrimaryKey>)
            {
                return Equals(obj as GovernmentOrganizationBase<TPrimaryKey>);
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
        public virtual bool Equals(GovernmentOrganizationBase<TPrimaryKey> obj)
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
                    return Id.Equals(obj.Id)
                        && Culture.Equals(obj.Culture)
                        && Schema.Equals(obj.Schema);
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
        public static bool operator ==(GovernmentOrganizationBase<TPrimaryKey> x, GovernmentOrganizationBase<TPrimaryKey> y)
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
        public static bool operator !=(GovernmentOrganizationBase<TPrimaryKey> x, GovernmentOrganizationBase<TPrimaryKey> y)
        {
            return !(x == y);
        }


        /// <summary>
        /// Computes and retrieves a hash code for an object.
        /// </summary>
        /// <returns>A hash code for an object.</returns>
        /// <remarks>This method implements the <see cref="Object">Object</see> method.</remarks>
        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

    }
}
