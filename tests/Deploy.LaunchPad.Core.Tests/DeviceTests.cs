// ***********************************************************************
// Assembly         : Deploy.LaunchPad.Core.Tests
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 06-12-2023
// ***********************************************************************
// <copyright file="DeviceTests.cs" company="Deploy.LaunchPad.Core.Tests">
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
    using FluentAssertions;
    using System.Collections.Generic;
    using System;
    using Deploy.LaunchPad.Core.Abp.Domain;
    using Deploy.LaunchPad.Core.Domain.Devices;
    using Deploy.LaunchPad.Core.Geospatial;

    /// <summary>
    /// Class DeviceTests.
    /// Implements the <see cref="Xunit.IClassFixture{Deploy.LaunchPad.Core.Tests.DeviceTestsFixture}" />
    /// </summary>
    /// <seealso cref="Xunit.IClassFixture{Deploy.LaunchPad.Core.Tests.DeviceTestsFixture}" />
    public partial class DeviceTests : IClassFixture<DeviceTestsFixture>
    {
        #region "Test Classes"



        #endregion

        /// <summary>
        /// The fixture
        /// </summary>
        private readonly DeviceTestsFixture _fixture;

        /// <summary>
        /// Initializes a new instance of the <see cref="DeviceTests"/> class.
        /// </summary>
        /// <param name="fixture">The fixture.</param>
        public DeviceTests(DeviceTestsFixture fixture)
        {
            this._fixture = fixture;
            Device<int> device = new Device<int>()
            {
                Id = 1
            };
            this._fixture.Initialize(device);
        }

        /// <summary>
        /// Defines the test method Should_Have_NotNull_Culture_When_Instantiated.
        /// </summary>
        [Fact]
        public void Should_Have_NotNull_Culture_When_Instantiated()
        {
            _fixture.SUT.Culture.Should().NotBeNullOrEmpty();
        }

        /// <summary>
        /// Defines the test method Should_Have_NotNull_CurrentPhysicalLocation_When_Instantiated.
        /// </summary>
        [Fact]
        public void Should_Have_NotNull_CurrentPhysicalLocation_When_Instantiated()
        {
            _fixture.SUT.CurrentLocation.Should().NotBeNull();
        }

        /// <summary>
        /// Defines the test method Should_Have_NotNull_PowerLevel_When_Instantiated.
        /// </summary>
        [Fact]
        public void Should_Have_NotNull_PowerLevel_When_Instantiated()
        {
            _fixture.SUT.Power.Should().NotBeNull();
        }

        /// <summary>
        /// Defines the test method Should_Have_Unknown_PowerLevel_When_Instantiated.
        /// </summary>
        [Fact]
        public void Should_Have_Unknown_PowerLevel_When_Instantiated()
        {
            _fixture.SUT.Power.PowerLevel.Should().Be(DevicePowerChargeLevel.Unknown);
        }

        /// <summary>
        /// Defines the test method Should_Have_Unknown_Power_RemainingChargeTime_When_Instantiated_Without_Providing_Value_In_Constructor.
        /// </summary>
        [Fact]
        public void Should_Have_Unknown_Power_RemainingChargeTime_When_Instantiated_Without_Providing_Value_In_Constructor()
        {
            _fixture.SUT.Power.RemainingChargeTime.Should().NotHaveValue();
        }

        /// <summary>
        /// Defines the test method Should_Have_PreviousPhysicalLocations_When_Instantiated_Without_Providing_Value_In_Constructor.
        /// </summary>
        [Fact]
        public void Should_Have_PreviousPhysicalLocations_When_Instantiated_Without_Providing_Value_In_Constructor()
        {
            _fixture.SUT.PreviousLocations.Should().HaveCount(0);
        }

        /// <summary>
        /// Defines the test method Should_Allow_PreviousPhysicalLocations_To_Be_Added.
        /// </summary>
        [Fact]
        public void Should_Allow_PreviousPhysicalLocations_To_Be_Added()
        {
            SpaceTimeInformation sydney = new SpaceTimeInformation(new GeographicPosition(151.2094, 33.8650));
            SpaceTimeInformation london = new SpaceTimeInformation(new GeographicPosition(0.1275, 51.5072));
            SpaceTimeInformation newyork = new SpaceTimeInformation(new GeographicPosition(74.0059, 40.7127));
            SpaceTimeInformation kingston = new SpaceTimeInformation(new GeographicPosition(76.5000, 44.2333 ));
            SpaceTimeInformation halifax = new SpaceTimeInformation(new GeographicPosition(63.5714, 44.6478 ));
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

        /// <summary>
        /// Defines the test method Should_Be_Equal.
        /// </summary>
        [Fact]
        public void Should_Be_Equal()
        {
            Device<Guid> a = new Device<Guid>();
            a.Culture = "en-CA";
            a.Id = new Guid("9fa65d30-ecc4-446f-b9ad-6ca29be9dab8");
            Device<Guid> b = new Device<Guid>();
            b.Culture = "en-CA";
            b.Id = new Guid("9fa65d30-ecc4-446f-b9ad-6ca29be9dab8");
            Assert.Equal(a, b);
        }
    }
}
