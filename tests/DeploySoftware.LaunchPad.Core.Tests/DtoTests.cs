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

namespace DeploySoftware.LaunchPad.Core.Tests
{
    using Xunit;
    using FluentAssertions;
    using DeploySoftware.LaunchPad.Core.Domain;
    using System.Collections.Generic;
    using System;
    using Abp.Application.Services.Dto;

    public class DtoTests : IClassFixture<DeviceTestsFixture>
    {
        #region "Test Classes"



        #endregion

        private readonly DtoTestsFixture _fixture;

        public DtoTests(DtoTestsFixture fixture)
        {
            this._fixture = fixture;
            DevicePower power = new DevicePower();
            power.PowerLevel = DevicePower.PowerChargeLevel.Charged;
            Device<int> device = new Device<int>()
            {
                Id = 1,
                TenantId = 1,
                DescriptionShort = "Short description",
                DescriptionFull = "Lorem ipsum et dolor sit amet bla bla bla",
                IsActive = true,
                IsDeleted = false,
                Culture = "en-CA",
                Name = "Device for testing DTOs",
                TranslatedFromId = 1,
                Power = power
            };
            this._fixture.Initialize(device);
        }


    }
}
