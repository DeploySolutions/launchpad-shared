//LaunchPad Shared
// Copyright (c) 2016-2021 Deploy Software Solutions, inc. 

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

namespace DeploySoftware.LaunchPad.Shared.Domain
{
    using System;
    using System.Runtime.Serialization;
    using CoordinateSharp;

    /// <summary>
    /// This interface defines the physical position of something, in terms of its latitude, longitude, and elevation.
    /// </summary>
    public interface IGeographicLocation : ISerializable
    {
        /// <summary>
        /// The Coordinates of this object on Earth
        /// </summary>
        Coordinate EarthCoordinate { get; set; }

        /// <summary>
        /// The latitude of this object
        /// </summary>
        double Latitude { get; }
        /// <summary>
        /// The longitude of this object
        /// </summary>
        double Longitude { get; }

        /// <summary>
        /// The elevation of this object (compared to "sea level" on Earth, or an arbitrary point on another body)
        /// </summary>
        double Elevation { get; set; }
    }
}