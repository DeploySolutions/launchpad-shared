// ***********************************************************************
// Assembly         : Deploy.LaunchPad.Core
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 01-08-2023
// ***********************************************************************
// <copyright file="MetadataInformation.cs" company="Deploy Software Solutions, inc.">
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

namespace Deploy.LaunchPad.Core.Domain
{
    using System;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Runtime.Serialization;
    using System.Text;
    using System.Xml.Serialization;

    /// <summary>
    /// This class holds common metadata information for entities used by the framework,
    /// such as the author or the date last modified. It is a core component of any Entity class.
    /// </summary>
    [Serializable()]
    [ComplexType]
    public partial class MetadataInformation : IMetadataInformation
    {
        /// <summary>
        /// The culture of this entity
        /// </summary>
        /// <value>The culture.</value>
        [DataObjectField(true)]
        [XmlAttribute]
        public String Culture { get; set; }

        /// <summary>
        /// The id of the tenant that domain entity this belongs to (null if not known/applicable)
        /// </summary>
        /// <value>The tenant identifier.</value>
        [DataObjectField(false)]
        [XmlAttribute]
        public int? TenantId { get; set; }

        /// <summary>
        /// The display name that can be displayed as a label externally to users when referring to this object
        /// (rather than using a GUID, which is unfriendly but unique)
        /// </summary>
        /// <value>The name.</value>
        [DataObjectField(false)]
        [XmlAttribute]
        public virtual String Name { get; set; }

        /// <summary>
        /// A full description of this item.
        /// </summary>
        /// <value>The description full.</value>
        [DataObjectField(false)]
        [XmlAttribute]
        public virtual String DescriptionFull { get; set; }

        /// <summary>
        /// A short description of this item.
        /// </summary>
        /// <value>The description short.</value>
        [DataObjectField(false)]
        [XmlAttribute]
        public virtual String DescriptionShort { get; set; }

        /// <summary>
        /// The date and time that this object was created.
        /// </summary>
        /// <value>The creation time.</value>
        [DataObjectField(false)]
        [XmlAttribute]
        public virtual DateTime CreationTime { get; set; }

        /// <summary>
        /// The id of the User Agent which created this entity
        /// </summary>
        /// <value>The creator user identifier.</value>
        [DataObjectField(false)]
        [XmlAttribute]
        public virtual long? CreatorUserId { get; set; }

        /// <summary>
        /// The date and time that the location and/or properties of this object were last modified.
        /// </summary>
        /// <value>The last modification time.</value>
        [DataObjectField(false)]
        [XmlAttribute]
        public virtual DateTime? LastModificationTime { get; set; }

        /// <summary>
        /// The id of the User Agent which last modified this object.
        /// </summary>
        /// <value>The last modifier user identifier.</value>
        [DataObjectField(false)]
        [XmlAttribute]
        public virtual Int64? LastModifierUserId { get; set; }


        /// <summary>
        /// The date and time that this object was deleted.
        /// </summary>
        /// <value>The deletion time.</value>
        [DataObjectField(false)]
        [XmlAttribute]
        public virtual DateTime? DeletionTime { get; set; }

        /// <summary>
        /// The id of the user which deleted this entity
        /// </summary>
        /// <value>The deleter user identifier.</value>
        [DataObjectField(false)]
        [XmlAttribute]
        public virtual long? DeleterUserId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is active.
        /// </summary>
        /// <value><c>true</c> if this instance is active; otherwise, <c>false</c>.</value>
        [DataObjectField(false)]
        [XmlAttribute]
        public bool IsActive { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is deleted.
        /// </summary>
        /// <value><c>true</c> if this instance is deleted; otherwise, <c>false</c>.</value>
        [DataObjectField(false)]
        [XmlAttribute]
        public bool IsDeleted { get; set; }

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="MetadataInformation">MetadataInformation</see> class.
        /// </summary>
        public MetadataInformation()
        {
            DescriptionFull = String.Empty;
            DescriptionShort = String.Empty;
            Name = String.Empty;
            CreationTime = DateTime.UtcNow;
            CreatorUserId = 1; // TODO - default user account?
            IsDeleted = false;
            IsActive = true;
        }

        /// <summary>
        /// Serialization constructor used for deserialization
        /// </summary>
        /// <param name="info">The serialization info</param>
        /// <param name="context">The context of the stream</param>
        public MetadataInformation(SerializationInfo info, StreamingContext context)
        {
            TenantId = info.GetInt32("TenantId");
            Name = info.GetString("DisplayName");
            DescriptionFull = info.GetString("DescriptionFull");
            DescriptionShort = info.GetString("DescriptionShort");
            CreationTime = info.GetDateTime("CreationTime");
            CreatorUserId = info.GetInt64("CreatorUserId");
            LastModifierUserId = info.GetInt64("LastModifierUserId");
            LastModificationTime = info.GetDateTime("LastModificationTime");
            DeletionTime = info.GetDateTime("DeletionTime");
            DeleterUserId = info.GetInt64("DeleterUserId");
            IsDeleted = info.GetBoolean("IsDeleted");
            IsActive = info.GetBoolean("IsActive");
        }

        #endregion

        /// <summary>
        /// The method required for implementing ISerializable
        /// </summary>
        /// <param name="info">The serialization info</param>
        /// <param name="context">The context of the stream</param>
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("TenantId", TenantId);
            info.AddValue("DisplayName", Name);
            info.AddValue("DescriptionFull", DescriptionFull);
            info.AddValue("DescriptionShort", DescriptionShort);
            info.AddValue("CreationTime", CreationTime);
            info.AddValue("CreatorUserId", CreatorUserId);
            info.AddValue("LastModifierUserId", LastModifierUserId);
            info.AddValue("LastModificationTime", LastModificationTime);
            info.AddValue("DeleterUserId", DeleterUserId);
            info.AddValue("DeletionTime", DeletionTime);
            info.AddValue("IsDeleted", IsDeleted);
            info.AddValue("IsActive", IsActive);
        }

        /// <summary>
        /// finite resources that
        /// can't be serialized and deserialized.
        /// </summary>
        /// <param name="sender">The object that has been deserialized</param>
        /// Event called once deserialization constructor finishes.
        /// Useful for reattaching connections and other
        public virtual void OnDeserialization(object sender)
        {
            // reconnect connection strings and other resources that won't be serialized
        }

        /// <summary>
        /// Displays information about the <c>Field</c> in readable format.
        /// </summary>
        /// <returns>A string representation of the object.</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("[MetadataInformation: ");
            sb.AppendFormat("TenantId={0};", TenantId);
            sb.AppendFormat("CreatorId={0};", CreatorUserId);
            sb.AppendFormat(" DisplayName={0};", Name);
            if (!String.IsNullOrEmpty(DescriptionFull))
            {
                sb.AppendFormat(" DescriptionFull={0};", DescriptionFull);
            }
            if (!String.IsNullOrEmpty(DescriptionShort))
            {
                sb.AppendFormat(" DescriptionShort={0};", DescriptionShort);
            }
            sb.AppendFormat(" LastModifiedById={0};", LastModifierUserId);

            sb.AppendFormat(" DateCreated={0};", CreationTime);
            sb.AppendFormat(" DateLastModified={0};", LastModificationTime);
            sb.AppendFormat(" IsActive={0};", IsActive);
            sb.AppendFormat(" IsDeleted={0};", IsDeleted);
            sb.Append(']');
            return sb.ToString();
        }

        /// <summary>
        /// Override the legacy Equals. Must cast objectToSerialize in this case.
        /// </summary>
        /// <param name="obj">A type to check equivalency of (hopefully) the right type</param>
        /// <returns>True if the objects are the same</returns>
        public override bool Equals(object obj)
        {
            if (obj != null && obj is MetadataInformation)
            {
                return Equals(obj as MetadataInformation);
            }
            return false;
        }

        /// <summary>
        /// Equality method between two objects of the same type.
        /// Because the Equals method is strongly typed by generic constraints,
        /// it is not necessary to test for the correct object type.
        /// </summary>
        /// <param name="obj">The other object of this type we are testing equality with</param>
        /// <returns>True if the objects are equal in value</returns>
        public virtual bool Equals(MetadataInformation obj)
        {
            // If parameter is null, return false.
            if (obj is null)
            {
                return false;
            }

            // Optimization for a common success case.
            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            // If run-time types are not exactly the same, return false.
            if (this.GetType() != obj.GetType())
            {
                return false;
            }

            // Return true if the fields match.
            // Note that the base class is not invoked because it is
            // System.Object, which defines Equals as reference equality.
            return
                 (
                     TenantId.Equals(obj.TenantId) &&
                     CreatorUserId.Equals(obj.CreatorUserId) &&
                     Name.Equals(obj.Name) &&
                     DescriptionFull.Equals(obj.DescriptionFull) &&
                     DescriptionShort.Equals(obj.DescriptionShort) &&
                     LastModifierUserId.Equals(obj.LastModifierUserId) &&
                     CreationTime.Equals(obj.CreationTime) &&
                     LastModificationTime.Equals(obj.LastModificationTime) &&
                     IsDeleted.Equals(obj.IsDeleted) &&
                     IsActive.Equals(obj.IsActive)
                 )
             ;
        }

        /// <summary>
        /// Override the == operator to test for equality
        /// </summary>
        /// <param name="x">The first value</param>
        /// <param name="y">The second value</param>
        /// <returns>True if both objects are fully equal based on the Equals logic</returns>
        public static bool operator ==(MetadataInformation x, MetadataInformation y)
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
        /// Implements the != operator.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator !=(MetadataInformation x, MetadataInformation y)
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
            return
                TenantId.GetHashCode()
                + CreatorUserId.GetHashCode()
                + CreationTime.GetHashCode()
                + Name.GetHashCode()
                + DescriptionFull.GetHashCode()
                + DescriptionShort.GetHashCode()
                + LastModifierUserId.GetHashCode()
                + LastModificationTime.GetHashCode()
                + DeletionTime.GetHashCode()
                + DeleterUserId.GetHashCode()
                ;
        }
    }
}
