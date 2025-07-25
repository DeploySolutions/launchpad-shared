﻿// ***********************************************************************
// Assembly         : Deploy.LaunchPad.Core.Tests
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 06-12-2023
// ***********************************************************************
// <copyright file="GeographicLocationTests.cs" company="Deploy.LaunchPad.Core.Tests">
//     Copyright (c) Deploy Software Solutions, Inc.. All rights reserved.
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

namespace Deploy.LaunchPad.Core.Tests
{
    using Xunit;
    
    using System;
    using Xunit.Sdk;
    using Deploy.LaunchPad.Core;
    using NetTopologySuite.Geometries;
    using Deploy.LaunchPad.Core.Geospatial.Position;

    /// <summary>
    /// Class GeographicLocationTests.
    /// </summary>
    public partial class GeographicLocationTests
    {
        #region "Test Classes"



        #endregion


        /// <summary>
        /// Defines the test method Eager_Loading_GeographicLocation_Should_Not_Throw_Error.
        /// </summary>
        [Fact]
        public void Eager_Loading_GeographicLocation_Should_Not_Throw_Error()
        {
            double lat = 0.0;
            double longi = 0.0;
            Action act = () => new GeographicPosition(new Point(longi, lat),0);
            var ex = Record.Exception(act);
            Assert.Null(ex);
        }

        /// <summary>
        /// Defines the test method Should_Have_Valid_Elevation_Number_When_Instantiated.
        /// </summary>
        [Fact]
        public void Should_Have_Valid_Elevation_Number_When_Instantiated()
        {
            GeographicPosition location = new GeographicPosition();
            double elevation = double.NaN;
            
            Action act = () => location.Elevation.Maximum = elevation;
            var ex = Assert.Throws<ArgumentException>(act);
            Assert.Contains(Deploy_LaunchPad_Core_Resources.Guard_GeographicLocation_Set_Elevation, ex.Message);
        }

        /// <summary>
        /// Defines the test method Should_Have_Valid_Latitude_Number_When_Instantiated.
        /// </summary>
        [Fact]
        public void Should_Have_Valid_Latitude_Number_When_Instantiated()
        {
            double latitude = double.NaN;
            double longitude = 0.0;
            Point p = new Point(longitude, latitude);
            ArgumentException ex = Assert.Throws<ArgumentException>(
                () => new GeographicPosition(p,0)
            );
            Assert.Contains(Deploy_LaunchPad_Core_Resources.Guard_GeographicLocation_Validate_Point, ex.Message);
        }

        //latitude value are wrong if < -90 || value > 90
        /// <summary>
        /// Defines the test method Should_Not_Allow_Latitude_Less_Than_Minus_90.
        /// </summary>
        [Fact]
        public void Should_Not_Allow_Latitude_Less_Than_Minus_90()
        {
            double latitude = -90.001;
            double longitude = 0.0;
            Point p = new Point(longitude, latitude);
            ArgumentOutOfRangeException ex = Assert.Throws<ArgumentOutOfRangeException>(
               () => new GeographicPosition(p, 0)
            );
            Assert.Contains(Deploy_LaunchPad_Core_Resources.Guard_GeographicLocation_Set_Latitude_Not_LessThan_Minus_90, ex.Message);
        }

        //latitude values are wrong if < -90 || value > 90
        /// <summary>
        /// Defines the test method Should_Not_Allow_Latitude_Greater_Than_90.
        /// </summary>
        [Fact]
        public void Should_Not_Allow_Latitude_Greater_Than_90()
        {
            double latitude = 90.001;
            double longitude = 0.0;
            Point p = new Point(longitude, latitude);
            ArgumentOutOfRangeException ex = Assert.Throws<ArgumentOutOfRangeException>(
               () => new GeographicPosition(p, 0)
            );
            Assert.Contains(Deploy_LaunchPad_Core_Resources.Guard_GeographicLocation_Set_Latitude_Not_GreaterThan_90, ex.Message);
        }

        /// <summary>
        /// Defines the test method Should_Have_Valid_Longitude_Number_When_Instantiated.
        /// </summary>
        [Fact]
        public void Should_Have_Valid_Longitude_Number_When_Instantiated()
        {
            double latitude = 0.0;
            double longitude = double.NaN;
            Point p = new Point(longitude, latitude);
            ArgumentException ex = Assert.Throws<ArgumentException>(
                () => new GeographicPosition(p, 0)
            ); 
            Assert.Contains(Deploy_LaunchPad_Core_Resources.Guard_GeographicLocation_Validate_Point, ex.Message);
        }

        //longitude value are wrong if <= -180 || value > 180
        /// <summary>
        /// Defines the test method Should_Not_Allow_Longitude_LessThan_Minus180.
        /// </summary>
        [Fact]
        public void Should_Not_Allow_Longitude_LessThan_Minus180()
        {
            GeographicPosition location = new GeographicPosition();
            double latitude = 0.0;
            double longitude = -180.001;
            Point p = new Point(longitude, latitude);
            ArgumentOutOfRangeException ex = Assert.Throws<ArgumentOutOfRangeException>(
               () => new GeographicPosition(p, 0)
            );
            Assert.Contains(Deploy_LaunchPad_Core_Resources.Guard_GeographicLocation_Set_Longitude_Not_LessThan_Minus180, ex.Message);
        }

        //longitude value are wrong if <= -180 || value > 180
        /// <summary>
        /// Defines the test method Should_Not_Allow_Longitude_MoreThan_180.
        /// </summary>
        [Fact]
        public void Should_Not_Allow_Longitude_MoreThan_180()
        {
            double latitude = 0.0;
            double longitude = 180.001;
            Point p = new Point(longitude, latitude);
            ArgumentOutOfRangeException ex = Assert.Throws<ArgumentOutOfRangeException>(
               () => new GeographicPosition(p, 0)
            );
            Assert.Contains(Deploy_LaunchPad_Core_Resources.Guard_GeographicLocation_Set_Longitude_Not_GreaterThan_180, ex.Message);
        }
        

    }
}
