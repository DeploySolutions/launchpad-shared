﻿//LaunchPad Shared
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

namespace Deploy.LaunchPad.Space.Satellites.Landsat
{
    using Deploy.LaunchPad.Core.Domain;
    using Deploy.LaunchPad.Space.Satellites.Core;

    /// <summary>
    /// Marker interface for Landsat satellite series observations
    /// </summary>
    /// <typeparam name="TPrimaryKey"></typeparam>
    public partial interface ILandsatObservationScene : IEarthObservationScene
    {
        public string Bucket { get; set; }

        public string ProductId { get; set; }

        public string Collection { get; set; }

        public string Projection { get; set; }

        public string SensorName { get; set; }

        public string YearAcquired { get; set; }

        public string Path { get; set; }

        public string Row { get; set; }

    }
}
