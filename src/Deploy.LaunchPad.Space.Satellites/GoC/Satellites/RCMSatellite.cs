// ***********************************************************************
// Assembly         : Deploy.LaunchPad.Space.Satellites
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 08-28-2023
// ***********************************************************************
// <copyright file="RCMSatellite.cs" company="Deploy Software Solutions, inc.">
//     2018-2023 Deploy Software Solutions, inc.
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

// Radarsat metadata is licensed under the Open Government License of the Canadian Federal Government, 2.0
// For more information, please consult the terms and conditions of this license at
// https://open.canada.ca/en/open-government-licence-canada 

namespace Deploy.LaunchPad.Space.Satellites.GoC
{

    using Deploy.LaunchPad.Organizations.Canada;
    using Deploy.LaunchPad.Space.Satellites.Core;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Class RCMSatellite.
    /// Implements the <see cref="SatelliteBase{TPrimaryKey}" />
    /// </summary>
    /// <typeparam name="TPrimaryKey">The type of the t primary key.</typeparam>
    /// <seealso cref="SatelliteBase{TPrimaryKey}" />
    public partial class RCMSatellite<TPrimaryKey> : SatelliteBase<TPrimaryKey>
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="RCMSatellite{TPrimaryKey}"/> class.
        /// </summary>
        /// <param name="tenantId">The tenant identifier.</param>
        protected RCMSatellite(int? tenantId) : base()
        {
            var csaOperator = new CanadaSpaceAgency(tenantId);
            csaOperator.Id = Guid.NewGuid();
            Operators = new Dictionary<Guid, ISatelliteOperator<Guid>>
            {
                { csaOperator.Id, csaOperator as ISatelliteOperator<Guid> }
            };
            ReferenceSystem = "Geocentric";
            OrbitalRegime = "Sun-synchronous orbit";
            AltitudeInKm = 600;
            InclinationDegrees = 97.74;
            OrbitalPeriodInMinutes = 96.4;
             

        }
    }
}
