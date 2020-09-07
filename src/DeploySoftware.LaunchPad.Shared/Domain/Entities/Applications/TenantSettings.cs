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


namespace DeploySoftware.LaunchPad.Shared.Domain
{
    [Serializable()]
    public class TenantSettings<TPrimaryKey> : DomainEntityBase<TPrimaryKey>, ITenantSettings<TPrimaryKey>
    {
        
        [DataObjectField(false)]
        [XmlAttribute]
        public string CultureDefault { get; set; }

        [DataObjectField(false)]
        [XmlAttribute]
        public IEnumerable<string> CultureSupported { get; set; }

        [DataObjectField(false)]
        [XmlAttribute]
        public long? PrimaryOwnerId { get; set; }

        [DataObjectField(false)]
        [XmlAttribute]
        public Uri DisplayLogoUri { get; set; }

        [DataObjectField(false)]
        [XmlAttribute]
        public string DisplayPrimaryColourHex { get; set; }

        #region "Constructors"

        public TenantSettings() : base()
        {
            DisplayPrimaryColourHex = ApplicationSettings<TPrimaryKey>.DEFAULT_HEX_COlOUR;
            CultureDefault = ApplicationSettings<TPrimaryKey>.DEFAULT_CULTURE;
            CultureSupported = new List<string>
            {
                ApplicationSettings<TPrimaryKey>.DEFAULT_CULTURE
            };

        }

        public TenantSettings(string cultureName, String cultureDefault) : base(cultureName)
        {
            DisplayPrimaryColourHex = ApplicationSettings<TPrimaryKey>.DEFAULT_HEX_COlOUR;
            CultureDefault = cultureDefault;
            CultureSupported = new List<string>
            {
                cultureDefault
            };
        }

        public TenantSettings(string cultureName, String cultureDefault, IEnumerable<string> cultureSupported) : base(cultureName)
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
        protected TenantSettings(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            CultureDefault = info.GetString("CultureDefault");
            CultureSupported = (IEnumerable<string>)info.GetValue("CultureSupported", typeof(IEnumerable<string>));
            DisplayPrimaryColourHex = info.GetString("DisplayPrimaryColourHex");
            PrimaryOwnerId = info.GetInt64("PrimaryOwnerId");
            DisplayLogoUri = (Uri)info.GetValue("DisplayLogoUri", typeof(Uri));

            
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
            sb.AppendFormat(" DisplayPrimaryColourHex={0};", DisplayPrimaryColourHex);
            sb.AppendFormat(" DisplayLogoUri={0};", DisplayLogoUri);
            sb.AppendFormat(" PrimaryOwnerId={0};", PrimaryOwnerId);
            sb.Append("]");
            return sb.ToString();
        }
    }
}
