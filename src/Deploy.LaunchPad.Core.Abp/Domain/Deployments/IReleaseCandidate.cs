// ***********************************************************************
// Assembly         : Deploy.LaunchPad.Core.Abp
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 07-26-2023
// ***********************************************************************
// <copyright file="IReleaseCandidate.cs" company="Deploy Software Solutions, inc.">
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
using System.Xml.Serialization;

namespace Deploy.LaunchPad.Core.Abp.Domain
{

    /// <summary>
    /// Represents a release (set of code, data, and resources) that is a candidate to be deployed to a destination environment.
    /// </summary>
    /// <typeparam name="TIdType">The type of the Id</typeparam>
    public partial interface IReleaseCandidate<TIdType> : ILaunchPadDomainEntity<TIdType>, IMustHaveTenant
    {

        /// <summary>
        /// The checksum of this release candidate
        /// </summary>
        /// <value>The checksum.</value>
        [DataObjectField(false)]
        [XmlAttribute]
        String Checksum { get; set; }

        /// <summary>
        /// The version of this release candidate
        /// </summary>
        /// <value>The version.</value>
        [DataObjectField(false)]
        [XmlAttribute]
        String Version { get; set; }

        /// <summary>
        /// The current state of the release candidate
        /// </summary>
        /// <value>The state of the release.</value>
        [DataObjectField(false)]
        [XmlAttribute]
        ReleaseCandidateBase<TIdType>.ReleaseStates ReleaseState { get; set; }

        /// <summary>
        /// The release date and time
        /// </summary>
        /// <value>The release date.</value>
        [DataObjectField(false)]
        [XmlAttribute]
        DateTime? ReleaseDate { get; set; }

        /// <summary>
        /// The URI where the release candidate package is located
        /// </summary>
        /// <value>The package URI.</value>
        [DataObjectField(false)]
        [XmlAttribute]
        Uri PackageUri { get; set; }

    }
}
