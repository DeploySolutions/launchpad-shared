﻿// ***********************************************************************
// Assembly         : Deploy.LaunchPad.Core
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 07-26-2023
// ***********************************************************************
// <copyright file="IHaveGeographicPosition.cs" company="Deploy Software Solutions, inc.">
//     2018-2023 Deploy Software Solutions, inc.
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

using Deploy.LaunchPad.Core.Domain;

namespace Deploy.LaunchPad.Core.Geospatial
{
    using Deploy.LaunchPad.Core.Geospatial.GeoJson;
    using Deploy.LaunchPad.Core.Geospatial.H3;
    using NetTopologySuite.Geometries;
    using System.Runtime.Serialization;

    /// <summary>
    /// This interface defines the physical position of something, in terms of its latitude, longitude.
    /// </summary>
    public partial interface IHaveGeographicPosition : IMayHaveElevation, IMayHaveAltitude, IMayHaveH3Definition, IMayHaveGeoJsonDefinition
    {
        /// <summary>
        /// Gets or sets the coordinate.
        /// </summary>
        /// <value>The coordinate.</value>
        public Coordinate Coordinate { get; set; }



    }
}