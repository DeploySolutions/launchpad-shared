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


namespace DeploySoftware.LaunchPad.Core.Abp.Domain.SoftwareApplications
{
    using Newtonsoft.Json.Linq;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Xml.Serialization;

    /// <summary>
    /// Represents the specific settings of an application.
    /// </summary>
    /// <typeparam name="TPrimaryKey"></typeparam>
    public interface IApplicationDetails<TIdType> : IDomainEntity<TIdType>
    {
        [DataObjectField(false)]
        [XmlAttribute]
        [ForeignKey(nameof(LaunchPadApplicationId))]
        TIdType LaunchPadApplicationId { get; set; }

        /// <summary>
        /// The default culture of this application
        /// </summary>
        [DataObjectField(false)]
        [XmlAttribute]
        String ApplicationKey
        {
            get; set;
        }

        /// <summary>
        /// The default culture of this application
        /// </summary>
        [DataObjectField(false)]
        [XmlAttribute]
        String CultureDefault
        {
            get; set;
        }

        /// <summary>
        /// The comma-delimited list of cultures supported by this application
        /// </summary>
        [DataObjectField(false)]
        [XmlAttribute]
        String CultureSupported
        {
            get; set;
        }

        /// <summary>
        /// The main theme
        /// </summary>
        [DataObjectField(false)]
        [XmlAttribute]
        string Theme { get; set; }

        /// <summary>
        /// The Uri for the logo to display in this application
        /// </summary>
        [DataObjectField(false)]
        [XmlAttribute]
        Uri LogoUri
        {
            get; set;
        }

        /// <summary>
        /// The primary colour (in HEX) for displays in this application.
        /// (Colour is spelled correctly in Canadian, eh.)
        /// </summary>
        [DataObjectField(false)]
        [XmlAttribute]
        String PrimaryColourHex
        {
            get; set;
        }

        /// <summary>
        /// The default display time zone of the application
        /// </summary>
        [DataObjectField(false)]
        [XmlAttribute]
        String DefaultTimeZone
        {
            get; set;
        }

    }
}
