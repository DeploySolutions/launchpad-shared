//LaunchPad Shared
// Copyright (c) 2018-2023 Deploy Software Solutions, inc. 

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


namespace Deploy.LaunchPad.Space.Satellites.Core
{

    using Deploy.LaunchPad.Core.Abp.Domain;
    using Deploy.LaunchPad.Core.Abp.Domain.Model;
    using Deploy.LaunchPad.Core.Domain;
    using Deploy.LaunchPad.Core.Geospatial;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public abstract class EarthObservationModelBase : LaunchPadModelBase,
        IEarthObservationScene

    {
        public const int MaxNameLength = 4 * 1024; //4KB
        public const int MaxDescriptionLength = 4 * 1024; //4KB

        [Required]
        public virtual string SceneId { get; set; }

        private GeographicPosition _sceneCentre;

        [Required]
        public virtual GeographicPosition SceneCentre
        {
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
        public virtual IDictionary<string, dynamic> Objects { get; set; }

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

        protected EarthObservationModelBase() : base()
        {
            var comparer = StringComparer.OrdinalIgnoreCase;
            Objects = new Dictionary<string, dynamic>(comparer);
        }

    }
}
