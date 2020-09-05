// Copyright (c) 2018 Deploy Software Solutions, inc. 

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

namespace DeploySoftware.LaunchPad.Space.Satellites.Canada
{
    using System;
    using DeploySoftware.LaunchPad.Shared.Domain;

    /// <summary>
    /// Implements the Radarsat1 observation data copyright information.
    /// </summary>
    public class Radarsat1DataUsageRights: UsageRights
    {

        public Radarsat1DataUsageRights() : base()
        {
            Owner = DeploySoftware_LaunchPad_Space_Resources.Text_Radarsat1DataUsageRights_Owner;
            Attribution = DeploySoftware_LaunchPad_Space_Resources.Text_Radarsat1DataUsageRights_Attribution;
            GoverningLicense = new OpenGovernmentCanadaLicense();
            ProjectLink = new Uri("http://www.asc-csa.gc.ca/eng/satellites/radarsat1/");
        }
        
    }
}
