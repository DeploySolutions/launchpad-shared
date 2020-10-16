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

namespace DeploySoftware.LaunchPad.Space.Tests
{
    using DeploySoftware.LaunchPad.Space.Satellites.Canada;
    using DeploySoftware.LaunchPad.Core.Domain;
    using System;

    public class Radarsat1MetadataFileFixture : IDisposable
    {
        public Radarsat1Observation<WindowsFileStorageLocation> Observation { get; set; }

        public void Initialize(string radarsat1MetadataFilename)
        {
            Radarsat1MetadataParser<WindowsFileStorageLocation> parser = new Radarsat1MetadataParser<WindowsFileStorageLocation>();
            Observation = parser.GetRadarsat1ObservationFromMetadataFile(radarsat1MetadataFilename);
        }

        public void Dispose()
        {

        }
    }
}
