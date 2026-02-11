// ***********************************************************************
// Assembly         : Deploy.LaunchPad.Space.Satellites
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 08-28-2023
// ***********************************************************************
// <copyright file="IEarthObservationScene.cs" company="Deploy Software Solutions, inc.">
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
    using Deploy.LaunchPad.Domain.Model;
    using Deploy.LaunchPad.Domain.Geospatial;
    using Deploy.LaunchPad.Domain.Geospatial.Position;
    using Deploy.LaunchPad.Domain.Licenses;
    using Deploy.LaunchPad.Util.Elements;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Interface IEarthObservationScene
    /// Extends the <see cref="ILaunchPadCoreProperties" />
    /// Extends the <see cref="ILaunchPadObject" />
    /// Extends the <see cref="IMustBePhysicallyLocatable" />
    /// </summary>
    /// <seealso cref="ILaunchPadCoreProperties" />
    /// <seealso cref="ILaunchPadObject" />
    /// <seealso cref="IMustBePhysicallyLocatable" />
    public partial interface IEarthObservationScene : ILaunchPadCoreProperties, ILaunchPadObject, IMustBePhysicallyLocatable
    {

        /// <summary>
        /// Gets or sets the scene identifier.
        /// </summary>
        /// <value>The scene identifier.</value>
        [Required]
        public string SceneId { get; set; }

        /// <summary>
        /// Gets or sets the scene centre.
        /// </summary>
        /// <value>The scene centre.</value>
        [Required]
        public GeographicPosition SceneCentre { get; set; }

        /// <summary>
        /// Gets or sets the corners.
        /// </summary>
        /// <value>The corners.</value>
        [Required]
        public ImageObservationCornerCoordinates Corners { get; set; }

        /// <summary>
        /// Gets or sets the type of the imagery.
        /// </summary>
        /// <value>The type of the imagery.</value>
        [Required]
        public EarthObservationImageryType ImageryType { get; set; }

        /// <summary>
        /// Gets or sets the scene start time.
        /// </summary>
        /// <value>The scene start time.</value>
        public DateTime SceneStartTime { get; set; }

        /// <summary>
        /// Gets or sets the scene end time.
        /// </summary>
        /// <value>The scene end time.</value>
        public DateTime SceneEndTime { get; set; }


        /// <summary>
        /// Gets or sets the objects.
        /// </summary>
        /// <value>The objects.</value>
        [Required]
        public IDictionary<string, dynamic> Objects { get; set; }

        /// <summary>
        /// The copyright information and license under which this observation may be used
        /// </summary>
        /// <value>The copyright.</value>
        [Required]
        public IUsageRights Copyright { get; set; }

        /// <summary>
        /// Gets or sets the level.
        /// </summary>
        /// <value>The level.</value>
        public EOSDISLevelEnum Level { get; set; }

    }
}
