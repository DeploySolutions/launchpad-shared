//LaunchPad Shared
// Copyright (c) 2018-2020 Deploy Software Solutions, inc. 

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

namespace DeploySoftware.LaunchPad.Space.Satellites.Common
{

    using Abp.Domain.Entities.Auditing;
    using DeploySoftware.LaunchPad.Core.Domain;
    using DeploySoftware.LaunchPad.Space.Satellites.Common;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public interface IEarthObservation<TPrimaryKey, TFileStorageLocationType> : IDomainEntity<TPrimaryKey>, IPhysicallyLocatable
        where TFileStorageLocationType : IFileStorageLocation, new()
    {
        [Required]
        public GeographicLocation SceneCentre { get; set; }

        [Required]
        public ImageObservationCornerCoordinates Corners { get; set; }

        public DateTime SceneStart { get; set; }

        public DateTime SceneEnd { get; set; }
        

        [Required]
        public IDictionary<string, FileBase<TPrimaryKey, byte[], TFileStorageLocationType>> Objects { get; set; }

        /// <summary>
        /// The copyright information and license under which this observation may be used
        /// </summary>
        [Required]
        public IUsageRights Copyright { get; set; }

        public EOSDISLevelEnum Level { get; set; }

    }
}
