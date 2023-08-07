//LaunchPad Space
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
    using Deploy.LaunchPad.Space.Satellites.Core.Observations;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial interface ISatellite
    {
        public string ReferenceSystem {get;set;}

        public string OrbitalRegime {get;set;}

        public double AltitudeInKm { get; set; }

        public double InclinationDegrees { get; set; }

        public double OrbitalPeriodInMinutes { get; set; }

        IDictionary<Guid, ISatelliteOperator<Guid>> Operators { get; set; }

        string CosparID { get; set; }

        string SatelliteCatalogNumber { get; set; }

        Uri Website { get; set; }

        [Required]
        public IDictionary<string, ISensor> Sensors { get; set; }

    }
}
