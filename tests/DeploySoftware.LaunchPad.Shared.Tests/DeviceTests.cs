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
    using DeploySoftware.LaunchPad.Shared.Domain;
    using System.Collections.Generic;

    public class DeviceTests
    {
        #region "Test Classes"

       

        #endregion

       
        [Fact]
        public void Should_Have_NotNull_Key_When_Instantiated()
        {
            Device<int> device = new Device<int>();
            device.GlobalKey.Should().NotBeNull();
        }

        [Fact]
        public void Should_Have_NotNull_Metadata_When_Instantiated()
        {
            Device<int> device = new Device<int>();
            device.Metadata.Should().NotBeNull();
        }

        [Fact]
        public void Should_Have_NotNull_CurrentPhysicalLocation_When_Instantiated()
        {
            Device<int> device = new Device<int>();
            device.CurrentLocation.Should().NotBeNull();
        }

        [Fact]
        public void Should_Have_NotNull_PowerLevel_When_Instantiated()
        {
            Device<int> device = new Device<int>();
            device.Power.Should().NotBeNull();
        }

        [Fact]
        public void Should_Have_Unknown_PowerLevel_When_Instantiated()
        {
            Device<int> device = new Device<int>();
            device.Power.PowerLevel.Should().Be(DevicePower.PowerChargeLevel.Unknown);
        }

        [Fact]
        public void Should_Have_Unknown_Power_RemainingChargeTime_When_Instantiated_Without_Providing_Value_In_Constructor()
        {
            Device<int> device = new Device<int>();
            device.Power.RemainingChargeTime.Should().NotHaveValue();
        }

        [Fact]
        public void Should_Have_PreviousPhysicalLocations_When_Instantiated_Without_Providing_Value_In_Constructor()
        {
            Device<int> device = new Device<int>();
            device.PreviousLocations.Should().HaveCount(0);
        }

        [Fact]
        public void Should_Allow_PreviousPhysicalLocations_To_Be_Added()
        {
            Device<int> device = new Device<int>();
            device.Id = 1;
            SpaceTimeInformation sydney = new SpaceTimeInformation(new GeographicLocation(33.8650,151.2094));
            SpaceTimeInformation london = new SpaceTimeInformation(new GeographicLocation(51.5072, 0.1275));
            SpaceTimeInformation newyork = new SpaceTimeInformation(new GeographicLocation(40.7127,74.0059));
            SpaceTimeInformation kingston = new SpaceTimeInformation(new GeographicLocation(44.2333,76.5000));
            SpaceTimeInformation halifax = new SpaceTimeInformation(new GeographicLocation(44.6478,63.5714));
            IList<SpaceTimeInformation> previousLocations = new List<SpaceTimeInformation>();
            device.PreviousLocations.Add(sydney);
            device.PreviousLocations.Add(london);
            device.PreviousLocations.Add(newyork);
            device.PreviousLocations.Add(kingston);
            device.PreviousLocations.Add(halifax);
            device.PreviousLocations.Should().HaveCount(5);
            device.PreviousLocations[0].Should().Be(sydney);
            device.PreviousLocations[4].Should().Be(halifax);
            Assert.Equal(device.PreviousLocations.Count, 5);
        }
    }
}
