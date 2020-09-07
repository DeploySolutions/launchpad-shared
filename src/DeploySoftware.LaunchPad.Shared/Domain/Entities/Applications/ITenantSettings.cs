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
    using Newtonsoft.Json.Linq;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Xml.Serialization;

    /// <summary>
    /// Represents a tenant in an application.
    /// </summary>
    /// <typeparam name="TPrimaryKey"></typeparam>
    public interface ITenantSettings<TPrimaryKey> : IDomainEntity<TPrimaryKey>
    {

        /// <summary>
        /// The default culture of this tenant
        /// </summary>
        [DataObjectField(false)]
        [XmlAttribute]
        String CultureDefault
        {
            get; set;
        }

        /// <summary>
        /// The supported cultures of this tenant
        /// </summary>
        [DataObjectField(false)]
        [XmlAttribute]
        IEnumerable<String> CultureSupported
        {
            get; set;
        }

        /// <summary>
        /// The account or primary owner of this tenant
        /// </summary>
        [DataObjectField(false)]
        [XmlAttribute]
        long? PrimaryOwnerId { get; set; }

        /// <summary>
        /// The Uri for the logo to display in this tenant
        /// </summary>
        [DataObjectField(false)]
        [XmlAttribute]
        Uri DisplayLogoUri
        {
            get; set;
        }

        /// <summary>
        /// The primary colour (in HEX) for displays in this tenant
        /// </summary>
        [DataObjectField(false)]
        [XmlAttribute]
        String DisplayPrimaryColourHex
        {
            get; set;
        }

    }
}
