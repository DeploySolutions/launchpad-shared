// ***********************************************************************
// Assembly         : Deploy.LaunchPad.Space.Satellites
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 08-28-2023
// ***********************************************************************
// <copyright file="EarthObservationModelBase.cs" company="Deploy Software Solutions, inc.">
//     2018-2024 Deploy Software Solutions, inc.
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


namespace Deploy.LaunchPad.Space.Satellites.Core
{

    using Deploy.LaunchPad.Core.Abp;
    using Deploy.LaunchPad.Core.Abp.Model;
    using Deploy.LaunchPad.Geospatial;
    using Deploy.LaunchPad.Geospatial.Position;
    using Deploy.LaunchPad.Core.Licenses;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Deploy.LaunchPad.Domain.Metadata;

    /// <summary>
    /// Class EarthObservationModelBase.
    /// Implements the <see cref="LaunchPadModelBase" />
    /// Implements the <see cref="Deploy.LaunchPad.Space.Satellites.Core.IEarthObservationScene" />
    /// </summary>
    /// <seealso cref="LaunchPadModelBase" />
    /// <seealso cref="Deploy.LaunchPad.Space.Satellites.Core.IEarthObservationScene" />
    public abstract class EarthObservationModelBase : LaunchPadModelBase,
        IEarthObservationScene

    {
        /// <summary>
        /// The maximum name length
        /// </summary>
        public const int MaxNameLength = 4 * 1024; //4KB
        /// <summary>
        /// The maximum description length
        /// </summary>
        public const int MaxDescriptionLength = 4 * 1024; //4KB

        /// <summary>
        /// Gets or sets the scene identifier.
        /// </summary>
        /// <value>The scene identifier.</value>
        [Required]
        public virtual string SceneId { get; set; }

        /// <summary>
        /// The scene centre
        /// </summary>
        private GeographicPosition _sceneCentre;

        /// <summary>
        /// Gets or sets the scene centre.
        /// </summary>
        /// <value>The scene centre.</value>
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

        /// <summary>
        /// Gets or sets the corners.
        /// </summary>
        /// <value>The corners.</value>
        [Required]
        public virtual ImageObservationCornerCoordinates Corners { get; set; }


        /// <summary>
        /// Gets or sets the objects.
        /// </summary>
        /// <value>The objects.</value>
        [Required]
        public virtual IDictionary<string, dynamic> Objects { get; set; }

        /// <summary>
        /// The current physical location of the object in time and space
        /// </summary>
        /// <value>The current location.</value>
        [Required]
        public virtual SpaceTimeInformation CurrentLocation { get; set; }

        /// <summary>
        /// Gets or sets the type of the imagery.
        /// </summary>
        /// <value>The type of the imagery.</value>
        [Required]
        public virtual EarthObservationImageryType ImageryType { get; set; }

        /// <summary>
        /// Gets or sets the scene start time.
        /// </summary>
        /// <value>The scene start time.</value>
        public virtual DateTime SceneStartTime { get; set; }

        /// <summary>
        /// Gets or sets the scene end time.
        /// </summary>
        /// <value>The scene end time.</value>
        public virtual DateTime SceneEndTime { get; set; }

        /// <summary>
        /// A list (not necessarily comprehensive) of this object's previous (but not current) physical positions.
        /// </summary>
        /// <value>The previous locations.</value>
        public virtual IList<SpaceTimeInformation> PreviousLocations { get; set; }

        /// <summary>
        /// The copyright information and license under which this observation may be used
        /// </summary>
        /// <value>The copyright.</value>
        [Required]
        public virtual IUsageRights Copyright { get; set; }

        /// <summary>
        /// The NASA EOSDIS data processing level of this observation
        /// (https://earthdata.nasa.gov/collaborate/open-data-services-and-software/data-information-policy/data-levels)
        /// </summary>
        /// <value>The level.</value>
        public virtual EOSDISLevelEnum Level { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="EarthObservationModelBase"/> class.
        /// </summary>
        protected EarthObservationModelBase() : base()
        {
            var comparer = StringComparer.OrdinalIgnoreCase;
            Objects = new Dictionary<string, dynamic>(comparer);
        }

    }
}
