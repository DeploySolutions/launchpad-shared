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
    using Deploy.LaunchPad.Core.Domain;
    using Deploy.LaunchPad.Core.Domain.Model;
    using Deploy.LaunchPad.Core.Geospatial;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial interface IEarthObservationScene : ILaunchPadCommonProperties, IPhysicallyLocatable
    {

        [Required]
        public string SceneId { get; set; }

        [Required]
        public GeographicPosition SceneCentre { get; set; }

        [Required]
        public ImageObservationCornerCoordinates Corners { get; set; }

        public DateTime SceneStartTime { get; set; }

        public DateTime SceneEndTime { get; set; }


        [Required]
        public IDictionary<string, dynamic> Objects { get; set; }

        /// <summary>
        /// The copyright information and license under which this observation may be used
        /// </summary>
        [Required]
        public IUsageRights Copyright { get; set; }

        public EOSDISLevelEnum Level { get; set; }

    }
}
