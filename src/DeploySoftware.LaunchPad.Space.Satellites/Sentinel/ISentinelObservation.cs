//LaunchPad Shared
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

namespace DeploySoftware.LaunchPad.Space.Satellites.Landsat
{
    using DeploySoftware.LaunchPad.Core.Domain;
    using DeploySoftware.LaunchPad.Space.Satellites.Common;
    using Newtonsoft.Json.Linq;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Marker interface for Sentinel satellite series observations
    /// </summary>
    /// <typeparam name="TPrimaryKey"></typeparam>
    public interface ISentinelObservation<TPrimaryKey, TFileStorageLocationType> : IEarthObservation<TPrimaryKey, TFileStorageLocationType>
        where TFileStorageLocationType : IFileStorageLocation, new()
    {

        public string Path { get; set; }

        public string Bucket { get; set; }

        public string MissionId { get; set; }

        public string ProductId { get; set; }

        public string ProductType { get; set; }

        public string Resolution { get; set; }

        public string Mode { get; set; }

        public string Polarization { get; set; }

        public long AbsoluteOrbitNumber { get; set; }

        public long MissionDataTakeId { get; set; }

        public string ProductUniqueIdentifier { get; set; }

        public DateTime SciHubIngestion { get; set; }

        public DateTime S3Ingestion { get; set; }

        public Guid SciHubId { get; set; }
        
        // To DO Footprint object?
        public JObject FootprintJson { get; set; }

        public string Version { get; set; }

    }
}
