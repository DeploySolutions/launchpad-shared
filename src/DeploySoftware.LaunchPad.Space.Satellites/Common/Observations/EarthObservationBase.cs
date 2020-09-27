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

    using System;
    using System.ComponentModel.DataAnnotations;
    using DeploySoftware.LaunchPad.Core.Domain;
    using System.Collections.Generic;

    public abstract class EarthObservationBase<TPrimaryKey> : DomainEntityBase<TPrimaryKey>, IEarthObservation<TPrimaryKey>
    {
        public const int MaxNameLength = 4 * 1024; //4KB
        public const int MaxDescriptionLength = 4 * 1024; //4KB

        private GeographicLocation _sceneCentre;

        [Required]
        public GeographicLocation SceneCentre {
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
        public ImageObservationCornerCoordinates Corners { get; set; }
        
        [Required]
        [StringLength(MaxNameLength)]
        public string Name { get; set; }

        
        [Required]
        public IObservationFiles<TPrimaryKey> ObservationFiles { get; set; }

        [Required]
        public SpaceTimeInformation CurrentLocation { get; set; }

        public IList<SpaceTimeInformation> PreviousLocations { get; set; }

        /// <summary>
        /// The copyright information and license under which this observation may be used
        /// </summary>
        [Required]
        public IUsageRights Copyright { get; set; }

        protected EarthObservationBase() : base()
        {

        }

    }
}
