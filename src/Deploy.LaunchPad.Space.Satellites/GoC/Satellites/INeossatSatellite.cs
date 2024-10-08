﻿// ***********************************************************************
// Assembly         : Deploy.LaunchPad.Space.Satellites
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 08-28-2023
// ***********************************************************************
// <copyright file="INeossatSatellite.cs" company="Deploy Software Solutions, inc.">
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

// NEOSSat metadata is licensed under the Open Government License of the Canadian Federal Government, 2.0
// For more information, please consult the terms and conditions of this license at
// https://open.canada.ca/en/open-government-licence-canada 


using Deploy.LaunchPad.Space.Satellites.Core.Observations;
using Deploy.LaunchPad.Space.Satellites.Core.Satellites;

namespace Deploy.LaunchPad.Space.Satellites.GoC
{
    /// <summary>
    /// Interface INeossatSatellite
    /// Extends the <see cref="IAsteroidObservationSatellite" />
    /// </summary>
    /// <seealso cref="IAsteroidObservationSatellite" />
    public partial interface INeossatSatellite : IAsteroidObservationSatellite
    {

    }
}
