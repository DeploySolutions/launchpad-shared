// ***********************************************************************
// Assembly         : Deploy.LaunchPad.Core.Abp
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 07-26-2023
// ***********************************************************************
// <copyright file="IApplicationDetails.cs" company="Deploy Software Solutions, inc.">
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


namespace Deploy.LaunchPad.Core.Abp.SoftwareApplications
{
    using System;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Xml.Serialization;
    using Deploy.LaunchPad.Core.Abp.Model;

    /// <summary>
    /// Represents the specific settings of an application.
    /// </summary>
    /// <typeparam name="TIdType">The type of the t identifier type.</typeparam>
    public partial interface IApplicationDetails<TIdType> : ILaunchPadDomainEntity<TIdType>
    {
        /// <summary>
        /// Gets or sets the launch pad application identifier.
        /// </summary>
        /// <value>The launch pad application identifier.</value>
        [DataObjectField(false)]
        [XmlAttribute]
        [ForeignKey(nameof(LaunchPadApplicationId))]
        TIdType LaunchPadApplicationId { get; set; }

        /// <summary>
        /// The default culture of this application
        /// </summary>
        /// <value>The application key.</value>
        [DataObjectField(false)]
        [XmlAttribute]
        String ApplicationKey
        {
            get; set;
        }

        /// <summary>
        /// The default culture of this application
        /// </summary>
        /// <value>The culture default.</value>
        [DataObjectField(false)]
        [XmlAttribute]
        String CultureDefault
        {
            get; set;
        }

        /// <summary>
        /// The comma-delimited list of cultures supported by this application
        /// </summary>
        /// <value>The culture supported.</value>
        [DataObjectField(false)]
        [XmlAttribute]
        String CultureSupported
        {
            get; set;
        }

        /// <summary>
        /// The main theme
        /// </summary>
        /// <value>The theme.</value>
        [DataObjectField(false)]
        [XmlAttribute]
        string Theme { get; set; }

        /// <summary>
        /// The Uri for the logo to display in this application
        /// </summary>
        /// <value>The logo URI.</value>
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
        /// <value>The primary colour hexadecimal.</value>
        [DataObjectField(false)]
        [XmlAttribute]
        String PrimaryColourHex
        {
            get; set;
        }

        /// <summary>
        /// The default display time zone of the application
        /// </summary>
        /// <value>The default time zone.</value>
        [DataObjectField(false)]
        [XmlAttribute]
        String DefaultTimeZone
        {
            get; set;
        }

    }
}
