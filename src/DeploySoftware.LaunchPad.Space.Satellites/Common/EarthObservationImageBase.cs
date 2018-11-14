//LaunchPad Shared
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

namespace DeploySoftware.LaunchPad.Space.Satellites.Common
{

    using System;
    using System.ComponentModel.DataAnnotations;
    using DeploySoftware.LaunchPad.Shared.Domain;

    public abstract class EarthObservationImageBase<TPrimaryKey> : DomainEntityBase<TPrimaryKey>, IEarthObservationImage<TPrimaryKey>
    {
        public const int MaxNameLength = 4 * 1024; //4KB
        public const int MaxDescriptionLength = 4 * 1024; //4KB
        
        [Required]
        public virtual GeographicLocation SceneCentre { get; set; }

        [Required]
        public virtual ImageObservationCornerCoordinates Corners { get; set; }

        [Required]
        public virtual string TIFImagePath { get; set; }

        [Required]
        [StringLength(MaxNameLength)]
        public virtual string Name { get; set; }

        [Required]
        [StringLength(MaxDescriptionLength)]
        public virtual string Description { get; set; }

        public virtual DateTime CreationTime { get; set; }

        public virtual DateTime? LastModificationTime { get; set; }
        
    }
}
