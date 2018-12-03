//LaunchPad Shared
// Copyright (c) 2016 Deploy Software Solutions, inc. 

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
    using Schema.NET;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Reflection;
    using System.Runtime.Serialization;
    using System.Security.Permissions;
    using System.Text;
    using System.Xml.Serialization;
    using DeploySoftware.LaunchPad.Shared.Domain.Metadata;

    /// <summary>
    /// Base class for Entities. Implements <see cref="IDomainEntity">IDomainEntity</see> and provides
    /// base functionality for many of its methods. Inherits from ASP.NET Boilerplate's IEntity interface.
    /// </summary>
    public abstract partial class GovernmentOrganizationBase<TPrimaryKey> : OrganizationBase<TPrimaryKey>, IOrganization<TPrimaryKey>
    {

        private GovernmentOrganization _schema;
        [DataObjectField(false)]
        [XmlAttribute]
        public new GovernmentOrganization Schema { get => _schema; set => _schema = value; }
        
        
        #region Implementation of ASP.NET Boilerplate's IEntity interface



        #endregion

        /// <summary>  
        /// Initializes a new instance of the <see cref="EntityBase">Entity</see> class
        /// </summary>
        protected GovernmentOrganizationBase()
        {
            GlobalKey = new DomainEntityKey();
            Metadata = new MetadataInformation();
            Tags = new List<MetadataTag<TPrimaryKey>>();
        }

        /// <summary>
        /// Creates a new instance of the <see cref="EntityBase">Entity</see> class given a key, and some metadata. 
        /// </summary>
        /// <param name="key">The unique identifier for this entity</param>
        /// <param name="metadata">The desired metadata for this entity</param>
        protected GovernmentOrganizationBase(DomainEntityKey key, MetadataInformation metadata)
        {
            GlobalKey = new DomainEntityKey();
            Metadata = metadata;
            Tags = new List<MetadataTag<TPrimaryKey>>();
        }

        /// <summary>
        /// Serialization constructor used for deserialization
        /// </summary>
        /// <param name="info">The serialization info</param>
        /// <param name="context">The context of the stream</param>
        protected GovernmentOrganizationBase(SerializationInfo info, StreamingContext context) 
        {
            GlobalKey = (DomainEntityKey)info.GetValue("GlobalKey", typeof(DomainEntityKey));
            Metadata = (MetadataInformation)info.GetValue("Metadata", typeof(MetadataInformation));
            Tags = (IEnumerable<MetadataTag<TPrimaryKey>>)info.GetValue("Metadata", typeof(IEnumerable<MetadataTag<TPrimaryKey>>));
            Schema = (GovernmentOrganization)info.GetValue("Organization", typeof(GovernmentOrganization));
            Offices = (IList<String>)info.GetValue("Offices", typeof(IList<String>));
        }

        /// <summary>
        /// The method required for implementing ISerializable
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
        public new virtual void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("GlobalKey", GlobalKey);
            info.AddValue("Metadata", Metadata);
            info.AddValue("Tags", Tags);
            info.AddValue("Schema", Schema);
            info.AddValue("Offices", Offices);
        }
        

        /// <summary>
        /// Shallow clones the entity
        /// </summary>
        /// <typeparam name="TEntity">The source entity to clone</typeparam>
        /// <returns>A shallow clone of the entity and its serializable properties</returns>
        protected new virtual TEntity Clone<TEntity>() where TEntity : IGovernmentOrganization<TPrimaryKey>, new()
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
            sb.Append(ToStringBaseProperties());
            sb.Append(Tags.ToString());
            sb.Append("]");
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
        /// <returns></returns>
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
                    return GlobalKey.Equals(obj.GlobalKey)
                        && Metadata.Equals(obj.Metadata)
                        && Tags.Equals(obj.Tags)
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
        /// <remarks>  
        /// This method implements the <see cref="Object">Object</see> method.  
        /// </remarks>  
        /// <returns>A hash code for an object.</returns>
        public override int GetHashCode()
        {
            return GlobalKey.GetHashCode();
        }

    }
}
