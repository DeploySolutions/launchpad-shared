﻿//LaunchPad Space
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

// Radarsat metadata is licensed under the Open Government License of the Canadian Federal Government, 2.0
// For more information, please consult the terms and conditions of this license at
// https://open.canada.ca/en/open-government-licence-canada 

namespace DeploySoftware.LaunchPad.Space.Satellites.Canada
{

    using System;
    using System.Collections.Generic;
    using DeploySoftware.LaunchPad.Space.Satellites.Common;
    using DeploySoftware.LaunchPad.Organizations.Canada;

    public class RadarsatConstellationMissionSatellite<TPrimaryKey> : SatelliteBase<TPrimaryKey>
    {
        protected RadarsatConstellationMissionSatellite(int? tenantId) : base()
        {
            Operators = new List<ISatelliteOperator<Guid>>() { new CanadaSpaceAgency(tenantId) as ISatelliteOperator<Guid> };
        }
    }
}
