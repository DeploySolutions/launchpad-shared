//LaunchPad Shared
// Copyright (c) 2018-2023 Deploy Software Solutions, inc. 

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


namespace Deploy.LaunchPad.Core.Abp.Domain.SoftwareApplications
{
    using System;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Xml.Serialization;
    using Deploy.LaunchPad.Core.Abp.Domain.Model;

    /// <summary>
    /// Represents a tenant in an application.
    /// </summary>
    /// <typeparam name="TIdType"></typeparam>
    public interface ITenantDetails<TIdType> : ILaunchPadDomainEntity<TIdType>
    {
        [DataObjectField(false)]
        [XmlAttribute]
        [ForeignKey(nameof(LaunchPadApplicationId))]
        TIdType LaunchPadApplicationId { get; set; }

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
        String CultureSupported
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
        /// The main theme
        /// </summary>
        [DataObjectField(false)]
        [XmlAttribute]
        string Theme { get; set; }

        /// <summary>
        /// The Uri for the logo to display in this tenant
        /// </summary>
        [DataObjectField(false)]
        [XmlAttribute]
        Uri LogoUri
        {
            get; set;
        }

        /// <summary>
        /// The primary colour (in HEX) for displays in this tenant
        /// </summary>
        [DataObjectField(false)]
        [XmlAttribute]
        String PrimaryColourHex
        {
            get; set;
        }

    }
}
