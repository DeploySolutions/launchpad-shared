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

namespace DeploySoftware.LaunchPad.Shared.Domain
{
    using Abp.Domain.Entities;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Reflection;
    using System.Runtime.Serialization;
    using System.Security.Permissions;
    using System.Text;
    using System.Xml.Serialization;
    using DeploySoftware.LaunchPad.Shared.Domain.Metadata;
    using Abp.Domain.Entities.Auditing;
    using System.Globalization;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Base class for Aggregate Root Entities (in Domain Driven Design). Implements <see cref="IDomainEntity">IDomainEntity</see> and provides
    /// base functionality for many of its methods. Inherits from ASP.NET Boilerplate's AggregateRoot class.
    /// Implements AspNetBoilerplate's auditing interfaces.
    /// </summary>
    public abstract partial class AggregateRootBase<TPrimaryKey> : 
        AggregateRoot<TPrimaryKey>, IDomainEntity<TPrimaryKey>, 
        IComparable<DomainEntityBase<TPrimaryKey>>, IEquatable<DomainEntityBase<TPrimaryKey>>,
        IHasCreationTime, ICreationAudited, IHasModificationTime, IModificationAudited, ISoftDelete, IDeletionAudited

    {
        /// <summary>
        /// The Culture code of this object
        /// </summary>
        [DataObjectField(true)]
        [XmlAttribute]
        [Key]
        public virtual String CultureName { get; set; }

        /// <summary>
        /// A convenience readonly property to get a <see cref="CultureInfo">CultureInfo</see> instance from the current 
        /// culture code
        /// </summary>
        public virtual CultureInfo Culture { get { return new CultureInfo(CultureName); } }

        /// <summary>
        /// Each entity can have an open-ended set of metadata applied to it, that helps to describe it.
        /// </summary>
        [DataObjectField(false)]
        [XmlAttribute]
        public MetadataInformation Metadata { get; set; }

        #region Implementation of ASP.NET Boilerplate's deletion and auditing interfaces

        private DateTime creationTime;
        public DateTime CreationTime
        {
            get { return Metadata.DateCreated; }
            set
            { 
                creationTime = value;
                Metadata.DateCreated = value;
            }
        }

        private Int64? creatorUserId;
        public long? CreatorUserId
        {
            get { return Metadata.CreatorId; }
            set
            {
                creatorUserId = value;
                Metadata.CreatorId = value;
            }
        }

        private DateTime? lastModificationTime;
        public DateTime? LastModificationTime
        {
            get { return Metadata.DateLastModified; }
            set
            {
                lastModificationTime = value;
                Metadata.DateLastModified = value;
            }
        }

        private Int64? lastModifierUserId;
        public long? LastModifierUserId
        {
            get { return Metadata.LastModifiedById; }
            set
            {
                lastModifierUserId = value;
                Metadata.LastModifiedById = value;
            }
        }

        public bool IsDeleted { get;set; }
        public long? DeleterUserId { get; set; }
        public DateTime? DeletionTime { get;set; }

        #endregion

        /// <summary>  
        /// Initializes a new instance of the <see cref="AggregateRootBase">AggregateRootBase</see> class
        /// </summary>
        protected AggregateRootBase() : base()
        {
            CultureName = "en";
            Metadata = new MetadataInformation();
        }

        /// <summary>
        /// Creates a new instance of the <see cref="AggregateRootBase">AggregateRootBase</see> class given a key, and some metadata. 
        /// </summary>
        /// <param name="cultureName">The culture for this entity</param>
        protected AggregateRootBase(string cultureName) : base()
        {
            CultureName = cultureName;
        }

        /// <summary>
        /// Creates a new instance of the <see cref="AggregateRootBase">AggregateRootBase</see> class given a key, and some metadata. 
        /// </summary>
        /// <param name="cultureName">The culture for this entity</param>
        /// <param name="metadata">The desired metadata for this entity</param>
        protected AggregateRootBase(string cultureName, MetadataInformation metadata) : base()
        {
            CultureName = cultureName;
            Metadata = metadata;
        }

        /// <summary>
        /// Serialization constructor used for deserialization
        /// </summary>
        /// <param name="info">The serialization info</param>
        /// <param name="context">The context of the stream</param>
        protected AggregateRootBase(SerializationInfo info, StreamingContext context)
        {
            Id = (TPrimaryKey)info.GetValue("Id", typeof(TPrimaryKey));
            CultureName = info.GetString("CultureName");
            Metadata = (MetadataInformation)info.GetValue("Metadata", typeof(MetadataInformation));
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
            info.AddValue("CultureName", CultureName);
            info.AddValue("Metadata", Metadata);
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
        protected virtual TEntity Clone<TEntity>() where TEntity : IDomainEntity<TPrimaryKey>, new()
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
        public virtual int CompareTo(DomainEntityBase<TPrimaryKey> other)
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
            sb.Append("[DomainEntity: ");
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
            sb.AppendFormat("Id={0};", Id);
            sb.AppendFormat("CultureName={0};", CultureName);
            sb.AppendFormat("Metadata={0};", Metadata);
            return sb.ToString();
        }

        /// <summary>
        /// Override the legacy Equals. Must cast obj in this case.
        /// </summary>
        /// <param name="obj">A type to check equivalency of (hopefully) an Entity</param>
        /// <returns>True if the entities are the same according to business key value</returns>
        public override bool Equals(object obj)
        {
            if (obj != null && obj is DomainEntityBase<TPrimaryKey>)
            {
                return Equals(obj as DomainEntityBase<TPrimaryKey>);
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
        public virtual bool Equals(DomainEntityBase<TPrimaryKey> obj)
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
                    return Id.Equals(obj.Id) && CultureName.Equals(obj.CultureName) && Metadata.Equals(obj.Metadata);
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
        public static bool operator ==(AggregateRootBase<TPrimaryKey> x, AggregateRootBase<TPrimaryKey> y)
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
        public static bool operator !=(AggregateRootBase<TPrimaryKey> x, AggregateRootBase<TPrimaryKey> y)
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
            return CultureName.GetHashCode()+Id.GetHashCode();
        }

    }
}
