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

using Abp.Domain.Entities;
using Deploy.LaunchPad.Core.Abp.Domain.Model;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

namespace Deploy.LaunchPad.Core.Abp.Domain
{

    /// <summary>
    /// Represents a an event that is related to a deployment
    /// </summary>
    /// <typeparam name="TIdType">The type of the Id</typeparam>
    public interface IDeploymentEvent<TIdType> : ILaunchPadDomainEntity<TIdType>, IMustHaveTenant
    {
        /// <summary>
        /// The id of the release candidate this deployment is for
        /// </summary>
        [DataObjectField(false)]
        [XmlAttribute]
        [Required]
        TIdType DeploymentId { get; set; }

        /// <summary>
        /// The category of this deployment event
        /// </summary>
        [DataObjectField(false)]
        [XmlAttribute]
        String EventCategory { get; set; }

        /// <summary>
        /// The deployment event start date and time
        /// </summary>
        [DataObjectField(false)]
        [XmlAttribute]
        DateTime? Started { get; set; }

        /// <summary>
        /// The deployment end date and time. May be null if the event is ongoing
        /// </summary>
        [DataObjectField(false)]
        [XmlAttribute]
        DateTime? Ended { get; set; }

        /// <summary>
        /// The URI where the deployment event log is located
        /// </summary>
        [DataObjectField(false)]
        [XmlAttribute]
        Uri LogUri { get; set; }

    }
}
