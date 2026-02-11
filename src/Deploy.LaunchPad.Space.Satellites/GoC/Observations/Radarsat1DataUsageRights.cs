// ***********************************************************************
// Assembly         : Deploy.LaunchPad.Space.Satellites
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 01-22-2023
// ***********************************************************************
// <copyright file="Radarsat1DataUsageRights.cs" company="Deploy Software Solutions, inc.">
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

namespace Deploy.LaunchPad.Space.Satellites.GoC
{
    using Deploy.LaunchPad.Domain.Licenses;
    using System;

    /// <summary>
    /// Implements the Radarsat1 observation data copyright information.
    /// </summary>
    public partial class Radarsat1DataUsageRights : UsageRights
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="Radarsat1DataUsageRights"/> class.
        /// </summary>
        public Radarsat1DataUsageRights() : base()
        {
            Owner = Deploy_LaunchPad_Space_Resources.Text_Radarsat1DataUsageRights_Owner;
            Attribution = Deploy_LaunchPad_Space_Resources.Text_Radarsat1DataUsageRights_Attribution;
            GoverningLicense = new OpenGovernmentCanadaLicense();
            ProjectLink = new Uri("https://www.asc-csa.gc.ca/eng/satellites/radarsat1/");
        }

    }
}
