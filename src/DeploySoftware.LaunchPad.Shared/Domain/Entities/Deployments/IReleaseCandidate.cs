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
    using System;
    using System.ComponentModel;
    using System.Xml.Serialization;

    /// <summary>
    /// Represents a release (set of code, data, and resources) that is a candidate to be deployed to a destination environment.
    /// </summary>
    /// <typeparam name="TIdType">The type of the Id</typeparam>
    public interface IReleaseCandidate<TIdType> : IDomainEntity<TIdType>
    {

        /// <summary>
        /// The checksum of this release candidate
        /// </summary>
        [DataObjectField(false)]
        [XmlAttribute]
        String Checksum { get; set; }

        /// <summary>
        /// The version of this release candidate
        /// </summary>
        [DataObjectField(false)]
        [XmlAttribute]
        String Version { get; set; }

        /// <summary>
        /// The current state of the release candidate
        /// </summary>
        [DataObjectField(false)]
        [XmlAttribute]
        ReleaseCandidate<TIdType>.ReleaseStates ReleaseState { get; set; }

        /// <summary>
        /// The release date and time
        /// </summary>
        [DataObjectField(false)]
        [XmlAttribute]
        DateTime? ReleaseDate { get; set; }

        /// <summary>
        /// The URI where the release candidate package is located
        /// </summary>
        [DataObjectField(false)]
        [XmlAttribute]
        Uri PackageUri { get; set; }

    }
}
