// ***********************************************************************
// Assembly         : Deploy.LaunchPad.Space.Satellites
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 08-28-2023
// ***********************************************************************
// <copyright file="ISatellite.cs" company="Deploy Software Solutions, inc.">
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
    using Deploy.LaunchPad.Domain.Geospatial.ReferencePoint;
    using Deploy.LaunchPad.Space.Satellites.Core.Observations;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// Interface ISatellite
    /// </summary>
    public partial interface ISatellite
    {
        /// <summary>
        /// Gets or sets the reference system.
        /// </summary>
        /// <value>The reference system.</value>
        public string ReferenceSystem {get;set;}

        /// <summary>
        /// Gets or sets the orbital regime.
        /// </summary>
        /// <value>The orbital regime.</value>
        public string OrbitalRegime {get;set;}

        /// <summary>
        /// Gets or sets the altitude in km.
        /// </summary>
        /// <value>The altitude in km.</value>
        public Altitude Altitude { get; set; }

        /// <summary>
        /// Gets or sets the inclination degrees.
        /// </summary>
        /// <value>The inclination degrees.</value>
        public double InclinationDegrees { get; set; }

        /// <summary>
        /// Gets or sets the orbital period in minutes.
        /// </summary>
        /// <value>The orbital period in minutes.</value>
        public double OrbitalPeriodInMinutes { get; set; }

        /// <summary>
        /// Gets or sets the operators.
        /// </summary>
        /// <value>The operators.</value>
        IDictionary<Guid, ISatelliteOperator<Guid>> Operators { get; set; }

        /// <summary>
        /// Gets or sets the cospar identifier.
        /// </summary>
        /// <value>The cospar identifier.</value>
        string CosparID { get; set; }

        /// <summary>
        /// Gets or sets the satellite catalog number.
        /// </summary>
        /// <value>The satellite catalog number.</value>
        string SatelliteCatalogNumber { get; set; }

        /// <summary>
        /// Gets or sets the website.
        /// </summary>
        /// <value>The website.</value>
        Uri Website { get; set; }

        /// <summary>
        /// Gets or sets the sensors.
        /// </summary>
        /// <value>The sensors.</value>
        [Required]
        public IDictionary<string, ISensor> Sensors { get; set; }

    }
}
