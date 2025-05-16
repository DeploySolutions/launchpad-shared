// ***********************************************************************
// Assembly         : Deploy.LaunchPad.Core.Abp
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 07-26-2023
// ***********************************************************************
// <copyright file="PersonDomainEntityBase.cs" company="Deploy Software Solutions, inc.">
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
using Deploy.LaunchPad.Core.Abp.Domain.Model;
using Deploy.LaunchPad.Core.Person;
using Deploy.LaunchPad.Core.Schemas.SchemaDotOrg;
using Schema.NET;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Deploy.LaunchPad.Core.Abp.Domain
{


    /// <summary>
    /// Base class for Persons.
    /// Implements <see cref="IPersonDomainEntity&lt;TPrimaryKey&gt;">IPersonDomainEntity&lt;TPrimaryKey&gt;</see> and provides
    /// base functionality for many of its methods.
    /// </summary>
    /// <typeparam name="TIdType">The type of the t identifier type.</typeparam>
    public abstract partial class PersonDomainEntityBase<TIdType> : LaunchPadDomainEntityBase<TIdType>, IPersonDomainEntity<TIdType>, IMayHaveTenant
    {

        protected virtual Schema.NET.Person? _schemaDotOrg { get; set; }

        /// <summary>
        /// Gets or sets the schema.
        /// </summary>
        /// <value>The schema.</value>
        public virtual string SchemaDotOrgJson
        {
            get
            {
                if (_schemaDotOrg != null)
                {
                    return _schemaDotOrg.ToString();
                }
                return "{}";
            }
            set
            {
                if (value != null)
                {
                    _schemaDotOrg = // read from Json using Newtonsoft
                        Newtonsoft.Json.JsonConvert.DeserializeObject<Schema.NET.Person>(value);
                }
            }
        }

        /// <summary>
        /// TenantId of this entity.
        /// </summary>
        /// <value>The tenant identifier.</value>
        public virtual int? TenantId { get; set; }
        public IList<ILaunchPadPerson> Parents { get; set; }
        public IList<ILaunchPadPerson> Children { get; set; }
        public IList<ILaunchPadPerson> Siblings { get; set; }
        public IList<ILaunchPadPerson> Colleagues { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="OrganizationDomainEntityBase&lt;TPrimaryKey&gt;">OrganizationBase&lt;TPrimaryKey&gt;</see> class
        /// </summary>
        protected PersonDomainEntityBase() : base()
        {
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="PersonDomainEntityBase&lt;TPrimaryKey&gt;">OrganizationBase&lt;TPrimaryKey&gt;</see> class
        /// </summary>
        /// <param name="tenantId">The tenant identifier.</param>
        protected PersonDomainEntityBase(int? tenantId) : base()
        {
            TenantId = tenantId;
        }

        /// <summary>
        /// Creates a new instance of the <see cref="PersonDomainEntityBase&lt;TPrimaryKey&gt;">OrganizationBase&lt;TPrimaryKey&gt;</see>
        /// class given a key, and some metadata.
        /// </summary>
        /// <param name="tenantId">The tenant identifier.</param>
        /// <param name="id">The identifier.</param>
        protected PersonDomainEntityBase(int? tenantId, TIdType id) : base()
        {
            TenantId = tenantId;
        }

        /// <summary>
        /// Serialization constructor used for deserialization
        /// </summary>
        /// <param name="info">The serialization info</param>
        /// <param name="context">The context of the stream</param>
        protected PersonDomainEntityBase(SerializationInfo info, StreamingContext context) : base(info, context)
        {

            SchemaDotOrgJson = info.GetString("SchemaDotOrgJson");
            _schemaDotOrg = (Schema.NET.Person)info.GetValue("_schemaDotOrg", typeof(Schema.NET.Person));
            
        }

        /// <summary>
        /// The method required for implementing ISerializable
        /// </summary>
        /// <param name="info">The information.</param>
        /// <param name="context">The context.</param>
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue("_schemaDotOrg", _schemaDotOrg);
            info.AddValue("SchemaDotOrgJson", SchemaDotOrgJson);
        }


        /// <summary>
        /// Shallow clones the entity
        /// </summary>
        /// <typeparam name="TEntity">The source entity to clone</typeparam>
        /// <returns>A shallow clone of the entity and its serializable properties</returns>
        protected new virtual TEntity Clone<TEntity>() where TEntity : IPersonDomainEntity<TIdType>, new()
        {
            TEntity clone = new TEntity();
            foreach (PropertyInfo info in GetType().GetProperties())
            {
                // ensure the property type is serializable
                if (info.GetType().IsSerializable)
                {
                    PropertyInfo cloneInfo = GetType().GetProperty(info.Name);
                    if (cloneInfo != null) cloneInfo.SetValue(clone, info.GetValue(this, null), null);
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
        public virtual int CompareTo(PersonDomainEntityBase<TIdType> other)
        {
            return other == null ? 1 : String.Compare(Name.Full, other.Name.Full, StringComparison.InvariantCulture);
        }

        ///// <summary>
        ///// This method makes it easy for any child class to generate a ToString() representation of
        ///// the common base properties
        ///// </summary>
        ///// <returns>A string description of the entity</returns>
        //protected override String ToStringBaseProperties()
        //{
        //    StringBuilder sb = new StringBuilder();
        //    sb.Append(ToStringBaseProperties());
        //    sb.AppendFormat("Schema={0};", Schema);
        //    sb.AppendFormat("FullName={0};", FullName);
        //    sb.AppendFormat("Abbreviation={0};", Abbreviation);
        //    sb.AppendFormat("HeadquartersAddress={0};", HeadquartersAddress);
        //    sb.AppendFormat("Website={0};", Website);
        //    return sb.ToString();
        //}

        /// <summary>
        /// Override the legacy Equals. Must cast obj in this case.
        /// </summary>
        /// <param name="obj">A type to check equivalency of (hopefully) an Entity</param>
        /// <returns>True if the entities are the same according to business key value</returns>
        public override bool Equals(object obj)
        {
            if (obj != null && obj is PersonDomainEntityBase<TIdType>)
            {
                return Equals((PersonDomainEntityBase<TIdType>)obj);
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
        public virtual bool Equals(PersonDomainEntityBase<TIdType> obj)
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
                    // Base domain entities are functionally equal if their key and metadata and tags are equal.
                    // Subclasses should extend to include their own enhanced equality checks, as required.
                    return Id.Equals(obj.Id)
                        && Culture.Equals(obj.Culture)
                        && SchemaDotOrgJson.Equals(obj.SchemaDotOrgJson);
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
        public static bool operator ==(PersonDomainEntityBase<TIdType> x, PersonDomainEntityBase<TIdType> y)
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
        public static bool operator !=(PersonDomainEntityBase<TIdType> x, PersonDomainEntityBase<TIdType> y)
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
            return Id.GetHashCode();
        }

    }
}
