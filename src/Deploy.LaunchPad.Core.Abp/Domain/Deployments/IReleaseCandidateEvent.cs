// ***********************************************************************
// Assembly         : Deploy.LaunchPad.Core.Abp
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 07-26-2023
// ***********************************************************************
// <copyright file="IReleaseCandidateEvent.cs" company="Deploy Software Solutions, inc.">
//     2018-2023 Deploy Software Solutions, inc.
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

using Abp.Domain.Entities;
using Deploy.LaunchPad.Core.Abp.Domain.Model;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

namespace Deploy.LaunchPad.Core.Abp.Domain
{

    /// <summary>
    /// Represents a an event that is related to a release candidate.
    /// </summary>
    /// <typeparam name="TIdType">The type of the Id</typeparam>
    public partial interface IReleaseCandidateEvent<TIdType> : ILaunchPadDomainEntity<TIdType>, IMustHaveTenant
    {
        /// <summary>
        /// The id of the release candidate this deployment is for
        /// </summary>
        /// <value>The release candidate identifier.</value>
        [DataObjectField(false)]
        [XmlAttribute]
        [Required]
        TIdType ReleaseCandidateId { get; set; }

        /// <summary>
        /// The category of this release candidate event
        /// </summary>
        /// <value>The event category.</value>
        [DataObjectField(false)]
        [XmlAttribute]
        String EventCategory { get; set; }

        /// <summary>
        /// The event start date and time
        /// </summary>
        /// <value>The started.</value>
        [DataObjectField(false)]
        [XmlAttribute]
        DateTime? Started { get; set; }

        /// <summary>
        /// The event end date and time. May be null if the event is ongoing
        /// </summary>
        /// <value>The ended.</value>
        [DataObjectField(false)]
        [XmlAttribute]
        DateTime? Ended { get; set; }

        /// <summary>
        /// The URI where the release candidate event log is located
        /// </summary>
        /// <value>The log URI.</value>
        [DataObjectField(false)]
        [XmlAttribute]
        Uri LogUri { get; set; }

    }
}
