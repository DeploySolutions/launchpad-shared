// ***********************************************************************
// Assembly         : Deploy.LaunchPad.Core.Tests
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 01-08-2023
// ***********************************************************************
// <copyright file="DtoTests.cs" company="Deploy.LaunchPad.Core.Tests">
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
    using Deploy.LaunchPad.Core.Domain;
    using System.Collections.Generic;
    using System;
    using Deploy.LaunchPad.Core.Domain.Devices;
    using Deploy.LaunchPad.Core.Abp.Domain;
    using Deploy.LaunchPad.Core.Domain.Model;
    using Deploy.LaunchPad.Util;

    /// <summary>
    /// Class DtoTests.
    /// Implements the <see cref="Xunit.IClassFixture{Deploy.LaunchPad.Core.Tests.DeviceTestsFixture}" />
    /// </summary>
    /// <seealso cref="Xunit.IClassFixture{Deploy.LaunchPad.Core.Tests.DeviceTestsFixture}" />
    public partial class DtoTests : IClassFixture<DeviceTestsFixture>
    {
        #region "Test Classes"



        #endregion

        /// <summary>
        /// The fixture
        /// </summary>
        private readonly DtoTestsFixture _fixture;

        /// <summary>
        /// Initializes a new instance of the <see cref="DtoTests"/> class.
        /// </summary>
        /// <param name="fixture">The fixture.</param>
        public DtoTests(DtoTestsFixture fixture)
        {
            this._fixture = fixture;
            DevicePower power = new DevicePower();
            power.PowerLevel = DevicePowerChargeLevel.Charged;
            Device<int> device = new Device<int>()
            {
                Id = 1,
                TenantId = 1,
                Name = new ElementName("Device for testing DTOs"),
                Description = new ElementDescription("Short description", "Lorem ipsum et dolor sit amet bla bla bla"),
                IsActive = true,
                IsDeleted = false,
                Culture = "en-CA",
                TranslatedFromId = 1,
                Power = power
            };
            this._fixture.Initialize(device);
        }


    }
}
