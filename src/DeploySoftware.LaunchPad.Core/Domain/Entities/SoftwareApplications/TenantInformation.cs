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
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Text;
using System.Xml.Serialization;


namespace DeploySoftware.LaunchPad.Core.Domain
{
    /// <summary>
    /// Base class for tenant-specific information
    /// </summary>
    /// <typeparam name="TIdType">The type of the key id field</typeparam>
    [Serializable()]
    public partial class TenantInformation<TIdType> : TenantSpecificDomainEntityBase<TIdType>, ITenantInformation<TIdType>
    {
        
        [DataObjectField(false)]
        [XmlAttribute]
        public string CultureDefault { get; set; }

        [DataObjectField(false)]
        [XmlAttribute]
        public String CultureSupported { get; set; }

        [DataObjectField(false)]
        [XmlAttribute]
        public long? PrimaryOwnerId { get; set; }

        [DataObjectField(false)]
        [XmlAttribute]
        public Uri LogoUri { get; set; }

        [DataObjectField(false)]
        [XmlAttribute]
        public string PrimaryColourHex { get; set; }


        /// <summary>
        /// The main theme (if Tenant is allowed to modify theme)
        /// </summary>
        [DataObjectField(false)]
        [XmlAttribute]
        public string Theme { get; set; }

        #region "Constructors"
        public TenantInformation() : base()
        {
            PrimaryColourHex = ApplicationInformation<TIdType>.DEFAULT_HEX_COlOUR;
            CultureDefault = ApplicationInformation<TIdType>.DEFAULT_CULTURE;
            CultureSupported = ApplicationInformation<TIdType>.DEFAULT_CULTURE;
        }

        public TenantInformation(int tenantId) : base(tenantId)
        {
            PrimaryColourHex = ApplicationInformation<TIdType>.DEFAULT_HEX_COlOUR;
            CultureDefault = ApplicationInformation<TIdType>.DEFAULT_CULTURE;
            CultureSupported = ApplicationInformation<TIdType>.DEFAULT_CULTURE;

        }

        public TenantInformation(int tenantId, TIdType id, string cultureName, String cultureDefault) : base(tenantId, id, cultureName)
        {
            PrimaryColourHex = ApplicationInformation<TIdType>.DEFAULT_HEX_COlOUR;
            CultureDefault = cultureDefault;
            CultureSupported = cultureDefault;
        }

        public TenantInformation(int tenantId, TIdType id, string cultureName, String cultureDefault, String cultureSupported) : base(tenantId, id, cultureName)
        {
            PrimaryColourHex = ApplicationInformation<TIdType>.DEFAULT_HEX_COlOUR;
            CultureDefault = cultureDefault;
            CultureSupported = cultureSupported;
        }

        /// <summary>
        /// Serialization constructor used for deserialization
        /// </summary>
        /// <param name="info">The serialization info</param>
        /// <param name="context">The context of the stream</param>
        protected TenantInformation(SerializationInfo info, StreamingContext context) : base(info, context)
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
        [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
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
            sb.Append("]");
            return sb.ToString();
        }
    }
}
