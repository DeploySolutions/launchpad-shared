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
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

namespace Deploy.LaunchPad.Core.Abp.Domain
{

    /// <summary>
    /// Represents a deployment that will take a release candidate (set of code, data, and resources) and place it in a destination environment.
    /// </summary>
    /// <typeparam name="TIdType">The type of the Id</typeparam>
    public interface IDeployment<TIdType> : IDomainEntity<TIdType>, IMustHaveTenant
    {

        /// <summary>
        /// The id of the release candidate this deployment is for
        /// </summary>
        [DataObjectField(false)]
        [XmlAttribute]
        [Required]
        TIdType ReleaseCandidateId { get; set; }

        /// <summary>
        /// The id of the process that will be followed during the deployment (if known)
        /// </summary>
        [DataObjectField(false)]
        [XmlAttribute]
        TIdType DeploymentProcessId { get; set; }

        /// <summary>
        /// The current state of the deployment
        /// </summary>
        [DataObjectField(false)]
        [XmlAttribute]
        DeploymentBase<TIdType>.DeploymentStates DeploymentState { get; set; }

        /// <summary>
        /// The intended deployment date and time
        /// </summary>
        [DataObjectField(false)]
        [XmlAttribute]
        DateTime? DateScheduled { get; set; }

        /// <summary>
        /// The actual deployment date and time
        /// </summary>
        [DataObjectField(false)]
        [XmlAttribute]
        DateTime? DateDeployed { get; set; }

        [DataObjectField(false)]
        [XmlAttribute]
        long? PrimaryDeployerUserId { get; set; }

    }
}
