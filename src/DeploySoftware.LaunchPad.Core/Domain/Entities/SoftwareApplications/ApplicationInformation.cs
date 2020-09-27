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
    public class ApplicationInformation<TIdType> : DomainEntityBase<TIdType>, IApplicationInformation<TIdType>, IMayHaveTenant
    {
        public const string DEFAULT_CULTURE = "en";
        public const string DEFAULT_HEX_COlOUR = "1dbff0";

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
        public string DisplayThemeName { get; set; }

        /// <summary>
        /// The Uri for the logo to display in this application
        /// </summary>
        [DataObjectField(false)]
        [XmlAttribute]
        public Uri DisplayLogoUri
        {
            get; set;
        }

        /// <summary>
        /// The primary colour (in HEX) for displays in this application
        /// </summary>
        [DataObjectField(false)]
        [XmlAttribute]
        public String DisplayPrimaryColourHex
        {
            get; set;
        }

        /// <summary>
        /// The default display time zone of the application
        /// </summary>
        [DataObjectField(false)]
        [XmlAttribute]
        public String DisplayDefaultTimeZone
        {
            get; set;
        }
        public int? TenantId { get; set; }
        #region "Constructors"

        public ApplicationInformation(int? tenantId) : base()
        {
            TenantId = tenantId;
            DisplayPrimaryColourHex = DEFAULT_HEX_COlOUR;
            CultureDefault = DEFAULT_CULTURE;
            CultureSupported = DEFAULT_CULTURE;
        }

        public ApplicationInformation(int? tenantId, TIdType id, string cultureName) : base(id, cultureName)
        {
            TenantId = tenantId;
            DisplayPrimaryColourHex = DEFAULT_HEX_COlOUR;
            CultureDefault = DEFAULT_CULTURE;
            CultureSupported = DEFAULT_CULTURE;
        }

        public ApplicationInformation(int? tenantId, TIdType id, string cultureName, String cultureDefault) : base(id, cultureName)
        {
            TenantId = tenantId;
            Id = id;
            DisplayPrimaryColourHex = ApplicationInformation<TIdType>.DEFAULT_HEX_COlOUR;
            CultureDefault = cultureDefault;
            CultureSupported = cultureDefault;
        }

        public ApplicationInformation(int? tenantId, TIdType id, string cultureName, String cultureDefault, String cultureSupported) : base(id, cultureName)
        {
            TenantId = tenantId;
            Id = id;
            DisplayPrimaryColourHex = ApplicationInformation<TIdType>.DEFAULT_HEX_COlOUR;
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
            CultureDefault = info.GetString("CultureDefault");
            CultureSupported = info.GetString("CultureSupported");
            DisplayPrimaryColourHex = info.GetString("DisplayPrimaryColourHex");
            DisplayLogoUri = (Uri)info.GetValue("DisplayLogoUri", typeof(Uri));
            DisplayThemeName = info.GetString("DisplayThemeName");
            DisplayDefaultTimeZone = info.GetString("DisplayDefaultTimeZone");
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
            info.AddValue("DisplayPrimaryColourHex", DisplayPrimaryColourHex);
            info.AddValue("DisplayLogoUri", DisplayLogoUri);
            info.AddValue("DisplayThemeName", DisplayThemeName);
            info.AddValue("DisplayDefaultTimeZone", DisplayDefaultTimeZone);
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
            sb.AppendFormat(" CultureDefault={0};", CultureDefault);
            sb.AppendFormat(" CultureSupported={0};", CultureSupported);
            sb.AppendFormat(" DisplayPrimaryColourHex={0};", DisplayPrimaryColourHex);
            sb.AppendFormat(" DisplayLogoUri={0};", DisplayLogoUri);
            sb.AppendFormat(" DisplayThemeName={0};", DisplayThemeName);
            sb.AppendFormat(" DisplayDefaultTimeZone={0};", DisplayDefaultTimeZone);
            sb.Append("]");
            return sb.ToString();
        }
    }
}
