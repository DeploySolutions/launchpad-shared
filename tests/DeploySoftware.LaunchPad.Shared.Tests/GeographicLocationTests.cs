//LaunchPad Shared
// Copyright (c) 2016 Deploy Software Solutions, inc. 

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
    using DeploySoftware.LaunchPad.Common.Domain.Entities;
    using DeploySoftware.LaunchPad.Common.Util;
    using System;
    using DeploySoftware.LaunchPad.Common;

    public class GeographicLocationTests
    {
        #region "Test Classes"



        #endregion



        [Fact]
        public void Should_Have_Valid_Elevation_Number_When_Instantiated()
        {
            GeographicLocation location = new GeographicLocation();
            double elevation = double.NaN;
            
            Action act = () => location.Elevation = elevation;
            act.Should().Throw<ArgumentException>()
                 .WithMessage("*" + DeploySoftware_LaunchPad_Shared_Common_Resources.Guard_GeographicLocation_Set_Elevation + "*");

        }

        [Fact]
        public void Should_Have_Valid_Latitude_Number_When_Instantiated()
        {
            GeographicLocation location = new GeographicLocation();
            double latitude = double.NaN;
            
            Action act = () => location.Latitude = latitude;
            act.Should().Throw<ArgumentException>()
                .WithMessage("*" + DeploySoftware_LaunchPad_Shared_Common_Resources.Guard_GeographicLocation_Set_Latitude_NaN + "*");
        }

        //latitude value are wrong if < -90 || value > 90
        [Fact]
        public void Should_Have_Latitude_Less_Than_Minus90_When_Instantiated()
        {
            GeographicLocation location = new GeographicLocation();
            double latitude = -90.001;
            
            Action act = () => location.Latitude = latitude;
            act.Should().Throw<ArgumentException>()
                .WithMessage("*" + DeploySoftware_LaunchPad_Shared_Common_Resources.Guard_GeographicLocation_Set_Latitude_InvalidRange + "*");
        }

        //latitude values are wrong if < -90 || value > 90
        [Fact]
        public void Should_Have_Latitude_Less_Than_90_When_Instantiated()
        {
            GeographicLocation location = new GeographicLocation();
            double latitude = 90.001;
            
            Action act = () => location.Latitude = latitude;
            act.Should().Throw<ArgumentException>()
                .WithMessage("*" + DeploySoftware_LaunchPad_Shared_Common_Resources.Guard_GeographicLocation_Set_Latitude_InvalidRange + "*");
        }

        [Fact]
        public void Should_Have_Valid_Longitude_Number_When_Instantiated()
        {
            GeographicLocation location = new GeographicLocation();
            double longitude = double.NaN;

            Action act = () => location.Longitude = longitude ;
            act.Should().Throw<ArgumentException>()
                 .WithMessage("*" + DeploySoftware_LaunchPad_Shared_Common_Resources.Guard_GeographicLocation_Set_Longitude_NaN + "*");
        }


        //longitude value are wrong if <= -180 || value > 180
        [Fact]
        public void Should_Have_Longitude_Less_Than_Minus180_When_Instantiated()
        {
            GeographicLocation location = new GeographicLocation();
            double longitude = -180.001;

            Action act = () => location.Longitude = longitude;
            act.Should().Throw<ArgumentException>()
                 .WithMessage("*" + DeploySoftware_LaunchPad_Shared_Common_Resources.Guard_GeographicLocation_Set_Longitude_InvalidRange + "*");
        }

        //longitude value are wrong if <= -180 || value > 180
        [Fact]
        public void Should_Have_Longitude_Less_Than_180_When_Instantiated()
        {
            GeographicLocation location = new GeographicLocation();
            double longitude = 180.001;

            Action act = () => location.Longitude = longitude;
            act.Should().Throw<ArgumentException>()
                .WithMessage("*" + DeploySoftware_LaunchPad_Shared_Common_Resources.Guard_GeographicLocation_Set_Longitude_InvalidRange + "*");
        }
      
    }
}
