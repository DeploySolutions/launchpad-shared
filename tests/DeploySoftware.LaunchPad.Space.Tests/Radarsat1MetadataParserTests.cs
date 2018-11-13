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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using FluentAssertions;
using DeploySoftware.LaunchPad.Shared.Domain;
using DeploySoftware.LaunchPad.Space.Satellites.Canada;
using DeploySoftware.LaunchPad.Shared.Util;

namespace DeploySoftware.LaunchPad.Space.Tests
{
    public class Radarsat1MetadataParserTests : IClassFixture<Radarsat1MetadataFileFixture>
    {
        #region "Test Classes"



        #endregion

        private Radarsat1MetadataFileFixture _fixture;

        public Radarsat1MetadataParserTests(Radarsat1MetadataFileFixture fixture)
        {
            this._fixture = fixture;
            this._fixture.Initialize(new FileKey("Radarsat1MetadataTest.en.txt"));
        }
        
        [Fact]
        public void Should_Have_NotNull_FileKey_When_Instantiated()
        {
            _fixture.Radarsat1MetadataFileKey.UniqueKey.Should().NotBeNull();
        }

        [Fact]
        public void Scene_ID_Should_Not_Be_NullOrEmpty()
        {
            _fixture.Observation.SceneId.Should().NotBeNullOrEmpty();
        }

        [Fact]
        public void Scene_ID_Should_Have_Correct_Value()
        {

            _fixture.Observation.SceneId.Should().Be("m0700836");
        }

        [Fact]
        public void Mda_Order_Number_Should_Not_Be_NullOrEmpty()
        {
            _fixture.Observation.MdaOrderNumber.Should().NotBeNullOrEmpty();
        }


        [Fact]
        public void Mda_Order_Number_Should_Have_Correct_Value()
        {

            _fixture.Observation.MdaOrderNumber.Should().Be("OGD_12546");
        }
    }
}
