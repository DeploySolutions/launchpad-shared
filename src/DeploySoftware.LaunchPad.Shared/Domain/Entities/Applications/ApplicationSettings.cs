﻿//LaunchPad Shared
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


namespace DeploySoftware.LaunchPad.Shared.Domain
{
    [Serializable()]
    public class ApplicationSettings<TPrimaryKey> : DomainEntityBase<TPrimaryKey>, IApplicationSettings<TPrimaryKey>
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
        public IEnumerable<String> CultureSupported
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

        #region "Constructors"

        public ApplicationSettings() : base()
        {
            DisplayPrimaryColourHex = DEFAULT_HEX_COlOUR;
            CultureDefault = DEFAULT_CULTURE;
            CultureSupported = new List<string>
            {
                DEFAULT_CULTURE
            };
        }

        public ApplicationSettings(string cultureName) : base(cultureName)
        {
            DisplayPrimaryColourHex = DEFAULT_HEX_COlOUR;
            CultureDefault = DEFAULT_CULTURE;
            CultureSupported = new List<string>
            {
                DEFAULT_CULTURE
            };
        }

        public ApplicationSettings(string cultureName, String cultureDefault) : base(cultureName)
        {
            DisplayPrimaryColourHex = ApplicationSettings<TPrimaryKey>.DEFAULT_HEX_COlOUR;
            CultureDefault = cultureDefault;
            CultureSupported = new List<string>
            {
                cultureDefault
            };
        }

        public ApplicationSettings(string cultureName, String cultureDefault, IEnumerable<string> cultureSupported) : base(cultureName)
        {
            DisplayPrimaryColourHex = ApplicationSettings<TPrimaryKey>.DEFAULT_HEX_COlOUR;
            CultureDefault = cultureDefault;
            CultureSupported = cultureSupported;
        }

        /// <summary>
        /// Serialization constructor used for deserialization
        /// </summary>
        /// <param name="info">The serialization info</param>
        /// <param name="context">The context of the stream</param>
        protected ApplicationSettings(SerializationInfo info, StreamingContext context)
        {
            Id = (TPrimaryKey)info.GetValue("Id", typeof(TPrimaryKey));
            Culture = info.GetString("CultureName");
            Metadata = (MetadataInformation)info.GetValue("Metadata", typeof(MetadataInformation));
            CultureDefault = info.GetString("CultureDefault");
            CultureSupported = (IEnumerable<string>)info.GetValue("CultureSupported", typeof(IEnumerable<string>));
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
            sb.Append("[ApplicationSettings : ");
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
