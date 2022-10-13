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


namespace DeploySoftware.LaunchPad.Space.Satellites.Core
{

    using System;
    using System.ComponentModel.DataAnnotations;
    using DeploySoftware.LaunchPad.Core.Domain;
    using System.Collections.Generic;
    using DeploySoftware.LaunchPad.Core.Abp.Domain;

    public abstract class EarthObservationBase<TPrimaryKey, TFileStorageLocationType> : DomainEntityBase<TPrimaryKey>, 
        IEarthObservationScene<TPrimaryKey, TFileStorageLocationType>
        where TFileStorageLocationType : IFileStorageLocation, new()

    {
        public const int MaxNameLength = 4 * 1024; //4KB
        public const int MaxDescriptionLength = 4 * 1024; //4KB

        [Required]
        public virtual string SceneId { get; set; }

        private GeographicLocation _sceneCentre;

        [Required]
        public virtual GeographicLocation SceneCentre {
            get
            {
                return _sceneCentre;
            }
            set
            {
                _sceneCentre = value;
                CurrentLocation.PhysicalLocation = _sceneCentre;
            }
        }

        [Required]
        public virtual ImageObservationCornerCoordinates Corners { get; set; }

        
        [Required]
        public virtual IDictionary<string, FileBase<TPrimaryKey, byte[], TFileStorageLocationType>> Objects { get; set; }

        [Required]
        public virtual SpaceTimeInformation CurrentLocation { get; set; }


        public virtual DateTime SceneStartTime { get; set; }

        public virtual DateTime SceneEndTime { get; set; }

        public virtual IList<SpaceTimeInformation> PreviousLocations { get; set; }

        /// <summary>
        /// The copyright information and license under which this observation may be used
        /// </summary>
        [Required]
        public virtual IUsageRights Copyright { get; set; }

        /// <summary>
        /// The NASA EOSDIS data processing level of this observation
        /// (https://earthdata.nasa.gov/collaborate/open-data-services-and-software/data-information-policy/data-levels)
        /// </summary>
        public virtual EOSDISLevelEnum Level { get; set; }

        protected EarthObservationBase() : base()
        {
            var comparer = StringComparer.OrdinalIgnoreCase;
            Objects = new Dictionary<string, FileBase<TPrimaryKey, byte[], TFileStorageLocationType>>(comparer);
        }

    }
}
