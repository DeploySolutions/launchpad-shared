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
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Text;
using System.Xml.Serialization;


namespace DeploySoftware.LaunchPad.Core.Domain
{
    /// <summary>
    /// Base class for application-specific information
    /// </summary>
    /// <typeparam name="TPrimaryKey">The type of the key id field</typeparam>
    [Serializable()]
    public partial class ApplicationInformation<TIdType> : DomainEntityBase<TIdType>, IApplicationInformation<TIdType>, IMayHaveTenant
    {
        public const string DEFAULT_CULTURE = "en";
        public const string DEFAULT_HEX_COlOUR = "1dbff0";

        [DataObjectField(false)]
        [XmlAttribute]
        [ForeignKey(nameof(LaunchPadApplicationId))]
        public TIdType LaunchPadApplicationId { get; set; }

        /// <summary>
        /// The id of the tenant that domain entity this belongs to (if any)
        /// </summary>
        [DataObjectField(false)]
        [XmlAttribute]
        [Required]
        [ForeignKey(nameof(TenantId))]
        public int? TenantId { get; set; }

        /// <summary>
        /// The default culture of this application
        /// </summary>
        [DataObjectField(false)]
        [XmlAttribute]
        public String ApplicationKey
        {
            get; set;
        }

        /// <summary>
        /// The default culture of this application
        /// </summary>
        [DataObjectField(false)]
        [XmlAttribute]
        public String CultureDefault
        {
            get; set;
        }

        /// <summary>
        /// The supported cultures of this application
        /// </summary>
        [DataObjectField(false)]
        [XmlAttribute]
        public String CultureSupported
        {
            get; set;
        }

        /// <summary>
        /// The main theme
        /// </summary>
        [DataObjectField(false)]
        [XmlAttribute]
        public string Theme { get; set; }

        /// <summary>
        /// The Uri for the logo to display in this application
        /// </summary>
        [DataObjectField(false)]
        [XmlAttribute]
        public Uri LogoUri
        {
            get; set;
        }

        /// <summary>
        /// The primary colour (in HEX) for displays in this application
        /// </summary>
        [DataObjectField(false)]
        [XmlAttribute]
        public String PrimaryColourHex
        {
            get; set;
        }

        /// <summary>
        /// The default display time zone of the application
        /// </summary>
        [DataObjectField(false)]
        [XmlAttribute]
        public String DefaultTimeZone
        {
            get; set;
        }

        #region "Constructors"
        public ApplicationInformation() : base()
        {
            PrimaryColourHex = DEFAULT_HEX_COlOUR;
            CultureDefault = DEFAULT_CULTURE;
            CultureSupported = DEFAULT_CULTURE;
        }

        public ApplicationInformation(int? tenantId) : base()
        {
            TenantId = tenantId;
            PrimaryColourHex = DEFAULT_HEX_COlOUR;
            CultureDefault = DEFAULT_CULTURE;
            CultureSupported = DEFAULT_CULTURE;
        }

        public ApplicationInformation(int? tenantId, TIdType id, string cultureName) : base(id, cultureName)
        {
            TenantId = tenantId;
            PrimaryColourHex = DEFAULT_HEX_COlOUR;
            CultureDefault = DEFAULT_CULTURE;
            CultureSupported = DEFAULT_CULTURE;
        }

        public ApplicationInformation(int? tenantId, TIdType id, string cultureName, String cultureDefault) : base(id, cultureName)
        {
            TenantId = tenantId;
            Id = id;
            PrimaryColourHex = ApplicationInformation<TIdType>.DEFAULT_HEX_COlOUR;
            CultureDefault = cultureDefault;
            CultureSupported = cultureDefault;
        }

        public ApplicationInformation(int? tenantId, TIdType id, string cultureName, String cultureDefault, String cultureSupported) : base(id, cultureName)
        {
            TenantId = tenantId;
            Id = id;
            PrimaryColourHex = ApplicationInformation<TIdType>.DEFAULT_HEX_COlOUR;
            CultureDefault = cultureDefault;
            CultureSupported = cultureSupported;
        }

        /// <summary>
        /// Serialization constructor used for deserialization
        /// </summary>
        /// <param name="info">The serialization info</param>
        /// <param name="context">The context of the stream</param>
        protected ApplicationInformation(SerializationInfo info, StreamingContext context) : base(info,context)
        {
            TenantId = info.GetInt32("TenantId"); 
            ApplicationKey = info.GetString("ApplicationKey"); 
            CultureDefault = info.GetString("CultureDefault");
            CultureSupported = info.GetString("CultureSupported");
            PrimaryColourHex = info.GetString("DisplayPrimaryColourHex");
            LogoUri = (Uri)info.GetValue("DisplayLogoUri", typeof(Uri));
            Theme = info.GetString("DisplayThemeName");
            DefaultTimeZone = info.GetString("DisplayDefaultTimeZone");
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
            info.AddValue("ApplicationKey", ApplicationKey);
            info.AddValue("CultureDefault", CultureDefault);
            info.AddValue("CultureSupported", CultureSupported);
            info.AddValue("PrimaryColourHex", PrimaryColourHex);
            info.AddValue("LogoUri", LogoUri);
            info.AddValue("Theme", Theme);
            info.AddValue("DefaultTimeZone", DefaultTimeZone);
        }

        /// <summary>  
        /// Displays information about the class in readable format.  
        /// </summary>  
        /// <returns>A string representation of the object.</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("[ApplicationInformation : ");
            sb.AppendFormat(ToStringBaseProperties());
            sb.AppendFormat(" TenantId={0};", TenantId);
            sb.AppendFormat(" ApplicationKey={0};", ApplicationKey);
            sb.AppendFormat(" CultureDefault={0};", CultureDefault);
            sb.AppendFormat(" CultureSupported={0};", CultureSupported);
            sb.AppendFormat(" PrimaryColourHex={0};", PrimaryColourHex);
            sb.AppendFormat(" LogoUri={0};", LogoUri);
            sb.AppendFormat(" Theme={0};", Theme);
            sb.AppendFormat(" DefaultTimeZone={0};", DefaultTimeZone);
            sb.Append("]");
            return sb.ToString();
        }

        /// <summary>
        /// Shallow clones the entity
        /// </summary>
        /// <typeparam name="TEntity">The source entity to clone</typeparam>
        /// <returns>A shallow clone of the entity and its serializable properties</returns>
        protected new virtual TEntity Clone<TEntity>() where TEntity : IApplicationInformation<TIdType>, new()
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
        public virtual int CompareTo(ApplicationInformation<TIdType> other)
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
            if (obj != null && obj is ApplicationInformation<TIdType>)
            {
                return Equals((ApplicationInformation<TIdType>)obj);
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
        public virtual bool Equals(ApplicationInformation<TIdType> obj)
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
                    if (TenantId != null)
                    {
                        return Id.Equals(obj.Id)
                        && Culture.Equals(obj.Culture)
                        && ApplicationKey.Equals(obj.ApplicationKey)
                        && Name.Equals(obj.Name);
                    }
                    else
                    {
                        return Id.Equals(obj.Id)
                        && Culture.Equals(obj.Culture)
                        && ApplicationKey.Equals(obj.ApplicationKey)
                        && Name.Equals(obj.Name)
                        && TenantId.Equals(obj.TenantId);
                    }
                    
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
        public static bool operator ==(ApplicationInformation<TIdType> x, ApplicationInformation<TIdType> y)
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
        public static bool operator !=(ApplicationInformation<TIdType> x, ApplicationInformation<TIdType> y)
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
            return Id.GetHashCode() + Culture.GetHashCode() + ApplicationKey.GetHashCode();
        }

    }
}
