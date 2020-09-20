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

namespace DeploySoftware.LaunchPad.Shared.Tests
{
    using Xunit;
    using FluentAssertions;
    using DeploySoftware.LaunchPad.Shared.Domain;
    using System.Collections.Generic;

    public class DeviceTests : IClassFixture<DeviceTestsFixture>
    {
        #region "Test Classes"



        #endregion

        private readonly DeviceTestsFixture _fixture;
        private int? tenantId = 1;

        public DeviceTests(DeviceTestsFixture fixture)
        {
            this._fixture = fixture;
            Device<int> device = new Device<int>(tenantId)
            {
                Id = 1
            };
            this._fixture.Initialize(device);
        }

        [Fact]
        public void Should_Have_NotNull_Culture_When_Instantiated()
        {
            _fixture.SUT.Key.Culture.Should().NotBeNullOrEmpty();
        }

        [Fact]
        public void Should_Have_NotNull_Metadata_When_Instantiated()
        {
            _fixture.SUT.Metadata.Should().NotBeNull();
        }

        [Fact]
        public void Should_Have_NotNull_CurrentPhysicalLocation_When_Instantiated()
        {
            _fixture.SUT.CurrentLocation.Should().NotBeNull();
        }

        [Fact]
        public void Should_Have_NotNull_PowerLevel_When_Instantiated()
        {
            _fixture.SUT.Power.Should().NotBeNull();
        }

        [Fact]
        public void Should_Have_Unknown_PowerLevel_When_Instantiated()
        {
            _fixture.SUT.Power.PowerLevel.Should().Be(DevicePower.PowerChargeLevel.Unknown);
        }

        [Fact]
        public void Should_Have_Unknown_Power_RemainingChargeTime_When_Instantiated_Without_Providing_Value_In_Constructor()
        {
            _fixture.SUT.Power.RemainingChargeTime.Should().NotHaveValue();
        }

        [Fact]
        public void Should_Have_PreviousPhysicalLocations_When_Instantiated_Without_Providing_Value_In_Constructor()
        {
            _fixture.SUT.PreviousLocations.Should().HaveCount(0);
        }

        [Fact]
        public void Should_Allow_PreviousPhysicalLocations_To_Be_Added()
        {
            SpaceTimeInformation sydney = new SpaceTimeInformation(new GeographicLocation(33.8650,151.2094));
            SpaceTimeInformation london = new SpaceTimeInformation(new GeographicLocation(51.5072, 0.1275));
            SpaceTimeInformation newyork = new SpaceTimeInformation(new GeographicLocation(40.7127,74.0059));
            SpaceTimeInformation kingston = new SpaceTimeInformation(new GeographicLocation(44.2333,76.5000));
            SpaceTimeInformation halifax = new SpaceTimeInformation(new GeographicLocation(44.6478,63.5714));
            IList<SpaceTimeInformation> previousLocations = new List<SpaceTimeInformation>();
            _fixture.SUT.PreviousLocations.Add(sydney);
            _fixture.SUT.PreviousLocations.Add(london);
            _fixture.SUT.PreviousLocations.Add(newyork);
            _fixture.SUT.PreviousLocations.Add(kingston);
            _fixture.SUT.PreviousLocations.Add(halifax);
            _fixture.SUT.PreviousLocations.Should().HaveCount(5);
            _fixture.SUT.PreviousLocations[0].Should().Be(sydney);
            _fixture.SUT.PreviousLocations[4].Should().Be(halifax);
            Assert.Equal(5, _fixture.SUT.PreviousLocations.Count);
        }
    }
}
