﻿// ***********************************************************************
// Assembly         : Deploy.LaunchPad.Core
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 07-26-2023
// ***********************************************************************
// <copyright file="IMayHaveGeographicPosition.cs" company="Deploy Software Solutions, inc.">
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

using Deploy.LaunchPad.Core.Domain;
using Deploy.LaunchPad.Core.Geospatial.GeoJson;
using Deploy.LaunchPad.Core.Geospatial.ReferencePoint;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Deploy.LaunchPad.Core.Geospatial.Position
{

    /// <summary>
    /// This interface defines the physical position of something, in terms of its latitude, longitude.
    /// </summary>
    public partial interface IMayHaveGeographicPosition :
        IMayHaveBoundingBox,
        IMayHaveGeoJsonDefinition,
        IMayHaveElevation
    {
        public bool? IsPoint { get; }

        public bool? IsArea { get; }

        ///<summary>
        /// Describes GPS location for the asset (ex 45.4201, -75.68775264 )
        ///</summary>
        [DataObjectField(false)]
        [XmlAttribute]
        public double? Latitude { get; }

        ///<summary>
        /// Describes GPS Longitude for the asset (ex 45.4201, -75.68775264 )
        ///</summary>
        [DataObjectField(false)]
        [XmlAttribute]
        public double? Longitude { get; }

        public double? CenterLatitude { get; }
        public double? CenterLongitude { get; }

        /// <summary>
        /// Gets the representative point of the geographic position as a tuple of latitude and longitude.
        /// In NetTopologySuite, geometry.PointOnSurface returns a point guaranteed to lie within the geometry (unlike Centroid, which may fall outside a polygon).
        /// Useful when: You want a "safe for labeling" or "safe for hit-testing" point. You need a representative location inside the area(e.g., for maps, UI, or region tagging).
        /// </summary>
        public (double Latitude, double Longitude)? RepresentativePoint { get; }

        /// <summary>
        /// Gets the centroid of the geographic position as a tuple of latitude and longitude.
        /// For a Point, .Centroid just returns the same point.
        /// For a Polygon, .Centroid returns the geometric center (center of mass).
        /// It may lie outside the polygon for non-convex shapes.
        /// </summary>
        public (double Latitude, double Longitude)? CentroidPoint { get; }        
    }
}