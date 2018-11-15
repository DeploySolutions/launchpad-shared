//LaunchPad Shared
// Copyright (c) 2018 Deploy Software Solutions, inc. 

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

namespace DeploySoftware.LaunchPad.Shared.Tests
{
    using Xunit;
    using FluentAssertions;
    using DeploySoftware.LaunchPad.Shared.Domain;
    using System;
    using CoordinateSharp;
    using Xunit.Sdk;

    public class GeographicLocationTests
    {
        #region "Test Classes"



        #endregion


        [Fact]
        public void Eager_Loading_GeographicLocation_Should_Not_Throw_Error()
        {
            double lat = 0.0;
            double longi = 0.0;
            EagerLoad load = new EagerLoad()
            {
                UTM_MGRS = true,
                Cartesian = true,
                Celestial = true
            };
            Action act = () => new GeographicLocation(lat, longi, load);
            act.Should().NotThrow<Exception>();
        }

        [Fact]
        public void Should_Have_Valid_Elevation_Number_When_Instantiated()
        {
            GeographicLocation location = new GeographicLocation();
            double elevation = double.NaN;
            
            Action act = () => location.Elevation = elevation;
            act.Should().Throw<ArgumentException>()
                 .WithMessage("*" + DeploySoftware_LaunchPad_Shared_Resources.Guard_GeographicLocation_Set_Elevation + "*");

        }

        [Fact]
        public void Should_Have_Valid_Latitude_Number_When_Instantiated()
        {
            double latitude = double.NaN;
            double longitude = 0.0;
            OverflowException ex = Assert.Throws<OverflowException>(
                () => new GeographicLocation(latitude, longitude)
            );
            Assert.Contains(DeploySoftware_LaunchPad_Shared_Resources.Guard_GeographicLocation_Set_Latitude_NaN, ex.Message);
        }

        //latitude value are wrong if < -90 || value > 90
        [Fact]
        public void Should_Not_Allow_Latitude_Less_Than_Minus_90()
        {
            GeographicLocation location = new GeographicLocation();
            double latitude = -90.001;
            double longitude = 0.0;
            ArgumentOutOfRangeException ex = Assert.Throws<ArgumentOutOfRangeException>(
               () => new GeographicLocation(latitude, longitude)
            );
            Assert.Contains(DeploySoftware_LaunchPad_Shared_Resources.Guard_GeographicLocation_Set_Latitude_Not_LessThan_Minus_90, ex.Message);
        }

        //latitude values are wrong if < -90 || value > 90
        [Fact]
        public void Should_Not_Allow_Latitude_Greater_Than_90()
        {
            GeographicLocation location = new GeographicLocation();
            double latitude = 90.001;
            double longitude = 0.0;
            ArgumentOutOfRangeException ex = Assert.Throws<ArgumentOutOfRangeException>(
               () => new GeographicLocation(latitude, longitude)
            );
            Assert.Contains(DeploySoftware_LaunchPad_Shared_Resources.Guard_GeographicLocation_Set_Latitude_Not_GreaterThan_90, ex.Message);
        }

        [Fact]
        public void Should_Have_Valid_Longitude_Number_When_Instantiated()
        {
            double latitude = 0.0;
            double longitude = double.NaN;
            OverflowException ex = Assert.Throws<OverflowException>(
                () => new GeographicLocation(latitude, longitude)
            ); 
            Assert.Contains(DeploySoftware_LaunchPad_Shared_Resources.Guard_GeographicLocation_Set_Longitude_NaN,ex.Message);
        }

        //longitude value are wrong if <= -180 || value > 180
        [Fact]
        public void Should_Not_Allow_Longitude_LessThan_Minus180()
        {
            GeographicLocation location = new GeographicLocation();
            double latitude = 0.0;
            double longitude = -180.001;
            ArgumentOutOfRangeException ex = Assert.Throws<ArgumentOutOfRangeException>(
               () => new GeographicLocation(latitude, longitude)
            );
            Assert.Contains(DeploySoftware_LaunchPad_Shared_Resources.Guard_GeographicLocation_Set_Longitude_Not_LessThan_Minus180, ex.Message);
        }

        //longitude value are wrong if <= -180 || value > 180
        [Fact]
        public void Should_Not_Allow_Longitude_MoreThan_180()
        {
            GeographicLocation location = new GeographicLocation();
            double latitude = 0.0;
            double longitude = 180.001;
            ArgumentOutOfRangeException ex = Assert.Throws<ArgumentOutOfRangeException>(
               () => new GeographicLocation(latitude, longitude)
            );
            Assert.Contains(DeploySoftware_LaunchPad_Shared_Resources.Guard_GeographicLocation_Set_Longitude_Not_MoreThan_180, ex.Message);
        }
        
        [Fact]
        public void Should_Not_Throw_FormatException_When_Instantiated()
        {
            GeographicLocation location = new GeographicLocation();

            Action act = () => location.EarthCoordinate = new Coordinate(51.476852, -0.000500, new DateTime(2000, 1, 1));
            act.Should().NotThrow<FormatException>();
        }

    }
}
