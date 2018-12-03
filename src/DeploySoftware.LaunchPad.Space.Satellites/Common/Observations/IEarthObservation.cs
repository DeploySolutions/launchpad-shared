﻿//LaunchPad Shared
// Copyright (c) 2018 Deploy Software Solutions, inc. 

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


using System.Collections;

namespace DeploySoftware.LaunchPad.Space.Satellites.Common
{

    using Abp.Domain.Entities;
    using Abp.Domain.Entities.Auditing;
    using DeploySoftware.LaunchPad.Shared.Domain;
    using System.ComponentModel.DataAnnotations;

    public interface IEarthObservation<TPrimaryKey> : IDomainEntity<TPrimaryKey>, IPhysicallyLocatable, IHasCreationTime, IHasModificationTime
    {
        [Required]
        GeographicLocation SceneCentre { get; set; }

        [Required]
        ImageObservationCornerCoordinates Corners { get; set; }
        
        [Required]
        string Name { get; set; }
        
        string Description { get; set; }

        [Required]
        IObservationFiles<TPrimaryKey> ObservationFiles { get; set; }

        /// <summary>
        /// The license under which this observation may be used
        /// </summary>
        [Required]
        ILicense License { get; set; }


    }
}
