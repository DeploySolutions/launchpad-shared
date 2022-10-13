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
    
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Text;
using System.Xml.Serialization;


namespace DeploySoftware.LaunchPad.Core.Abp.Domain.SoftwareApplications
{
    /// <summary>
    /// Base class for tenant-specific information
    /// </summary>
    /// <typeparam name="TIdType">The type of the key id field</typeparam>
    [Serializable()]
    public partial class TenantDetails<TIdType> : TenantSpecificDomainEntityBase<TIdType>, ITenantDetails<TIdType>
    {

        [DataObjectField(false)]
        [XmlAttribute]
        [ForeignKey(nameof(LaunchPadApplicationId))]
        public virtual TIdType LaunchPadApplicationId { get; set; }

        [DataObjectField(false)]
        [XmlAttribute]
        public virtual string CultureDefault { get; set; }

        [DataObjectField(false)]
        [XmlAttribute]
        public virtual String CultureSupported { get; set; }

        [DataObjectField(false)]
        [XmlAttribute]
        public virtual long? PrimaryOwnerId { get; set; }

        [DataObjectField(false)]
        [XmlAttribute]
        public virtual Uri LogoUri { get; set; }

        [DataObjectField(false)]
        [XmlAttribute]
        public virtual string PrimaryColourHex { get; set; }


        /// <summary>
        /// The main theme (if Tenant is allowed to modify theme)
        /// </summary>
        [DataObjectField(false)]
        [XmlAttribute]
        public virtual string Theme { get; set; }

        #region "Constructors"
        public TenantDetails() : base()
        {
            PrimaryColourHex = ApplicationDetails<TIdType>.DEFAULT_HEX_COlOUR;
            CultureDefault = ApplicationDetails<TIdType>.DEFAULT_CULTURE;
            CultureSupported = ApplicationDetails<TIdType>.DEFAULT_CULTURE;
        }

        public TenantDetails(int tenantId) : base(tenantId)
        {
            PrimaryColourHex = ApplicationDetails<TIdType>.DEFAULT_HEX_COlOUR;
            CultureDefault = ApplicationDetails<TIdType>.DEFAULT_CULTURE;
            CultureSupported = ApplicationDetails<TIdType>.DEFAULT_CULTURE;

        }

        public TenantDetails(int tenantId, TIdType id, string cultureName, String cultureDefault) : base(tenantId, id, cultureName)
        {
            PrimaryColourHex = ApplicationDetails<TIdType>.DEFAULT_HEX_COlOUR;
            CultureDefault = cultureDefault;
            CultureSupported = cultureDefault;
        }

        public TenantDetails(int tenantId, TIdType id, string cultureName, String cultureDefault, String cultureSupported) : base(tenantId, id, cultureName)
        {
            PrimaryColourHex = ApplicationDetails<TIdType>.DEFAULT_HEX_COlOUR;
            CultureDefault = cultureDefault;
            CultureSupported = cultureSupported;
        }

        /// <summary>
        /// Serialization constructor used for deserialization
        /// </summary>
        /// <param name="info">The serialization info</param>
        /// <param name="context">The context of the stream</param>
        protected TenantDetails(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            CultureDefault = info.GetString("CultureDefault");
            CultureSupported = info.GetString("CultureSupported");
            PrimaryColourHex = info.GetString("DisplayPrimaryColourHex");
            PrimaryOwnerId = info.GetInt64("PrimaryOwnerId");
            LogoUri = (Uri)info.GetValue("DisplayLogoUri", typeof(Uri));
            Theme = info.GetString("DisplayThemeName");
        }

        #endregion

        /// <summary>
        /// The method required for implementing ISerializable
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue("CultureDefault", CultureDefault);
            info.AddValue("CultureSupported", CultureSupported);
            info.AddValue("DisplayPrimaryColourHex", PrimaryColourHex);
            info.AddValue("DisplayLogoUri", LogoUri);
            info.AddValue("Theme", Theme);
            info.AddValue("PrimaryOwnerId", PrimaryOwnerId);
        }

        /// <summary>  
        /// Displays information about the class in readable format.  
        /// </summary>  
        /// <returns>A string representation of the object.</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("[TenantSettings : ");
            sb.AppendFormat(ToStringBaseProperties());
            sb.AppendFormat(" CultureDefault={0};", CultureDefault);
            sb.AppendFormat(" CultureSupported={0};", CultureSupported);
            sb.AppendFormat(" DisplayPrimaryColourHex={0};", PrimaryColourHex);
            sb.AppendFormat(" DisplayLogoUri={0};", LogoUri);
            sb.AppendFormat(" Theme={0};", Theme);
            sb.AppendFormat(" PrimaryOwnerId={0};", PrimaryOwnerId);
            sb.Append(']');
            return sb.ToString();
        }

        /// <summary>
        /// Shallow clones the entity
        /// </summary>
        /// <typeparam name="TEntity">The source entity to clone</typeparam>
        /// <returns>A shallow clone of the entity and its serializable properties</returns>
        protected new virtual TEntity Clone<TEntity>() where TEntity : ITenantDetails<TIdType>, new()
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
        /// <returns></returns>
        public virtual int CompareTo(TenantDetails<TIdType> other)
        {
            return other == null ? 1 : String.Compare(Name, other.Name, StringComparison.InvariantCulture);
        }

        /// <summary>
        /// Override the legacy Equals. Must cast obj in this case.
        /// </summary>
        /// <param name="obj">A type to check equivalency of (hopefully) an Entity</param>
        /// <returns>True if the entities are the same according to business key value</returns>
        public override bool Equals(object obj)
        {
            if (obj != null && obj is TenantDetails<TIdType>)
            {
                return Equals((TenantDetails<TIdType>)obj);
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
        public virtual bool Equals(TenantDetails<TIdType> obj)
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
                        && TenantId.Equals(obj.TenantId)
                        && Name.Equals(obj.Name);
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
        public static bool operator ==(TenantDetails<TIdType> x, TenantDetails<TIdType> y)
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
        public static bool operator !=(TenantDetails<TIdType> x, TenantDetails<TIdType> y)
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
            return Id.GetHashCode() + Culture.GetHashCode() + TenantId.GetHashCode();
        }
    }
}
