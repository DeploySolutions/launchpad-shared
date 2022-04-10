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

namespace DeploySoftware.LaunchPad.Space.Satellites.GoC
{
    using DeploySoftware.LaunchPad.Core.Domain;
    using DeploySoftware.LaunchPad.Space.Satellites.Core;

    /// <summary>
    /// Marker interface for Canada's RADARSAT satellite series
    /// </summary>
    /// <typeparam name="TPrimaryKey"></typeparam>
    public partial interface IRadarsatObservationScene<TPrimaryKey, TFileStorageLocationType> : IEarthObservationScene<TPrimaryKey,TFileStorageLocationType>
        where TFileStorageLocationType : IFileStorageLocation, new()
    {
    }
}
