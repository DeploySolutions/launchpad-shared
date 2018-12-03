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


using DeploySoftware.LaunchPad.Shared.Domain.Licenses;

namespace DeploySoftware.LaunchPad.Space.Tests
{

    using System;
    using Xunit;
    using FluentAssertions;
    using DeploySoftware.LaunchPad.Shared.Domain;

    public class Radarsat1MetadataParserTests : IClassFixture<Radarsat1MetadataFileFixture>
    {
        #region "Test Classes"



        #endregion

        private readonly Radarsat1MetadataFileFixture _fixture;

        public Radarsat1MetadataParserTests(Radarsat1MetadataFileFixture fixture)
        {
            _fixture = fixture;
            _fixture.Initialize(new FileKey("Radarsat1MetadataTest.en.txt"));
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
  
        [Fact]
        public void Geographical_Area_Should_Not_Be_NullOrEmpty()
        {
            _fixture.Observation.GeographicalArea.Should().NotBeNullOrEmpty();
        }

        [Fact]
        public void Geographical_Area_Should_Have_Correct_Value()
        {

            _fixture.Observation.GeographicalArea.Should().Be("Toronto");
        }

        [Fact]
        public void Scene_Start_Time_Should_Not_Be_NullOrEmpty()
        {
            _fixture.Observation.SceneStartTime.Should().NotHaveYear(0001);
        }

        [Fact]
        public void Scene_Start_Time_Should_Have_Correct_Value()
        {
            DateTime startDate = new DateTime(1997, 08, 20, 23, 14, 59, 649);
            _fixture.Observation.SceneStartTime.Should().Be(startDate);
        }


        [Fact]
        public void Scene_Stop_Time_Should_Not_Be_NullOrEmpty()
        {
            _fixture.Observation.SceneStopTime.Should().NotHaveYear(0001);
        }

        [Fact]
        public void Scene_Stop_Time_Should_Have_Correct_Value()
        {
            DateTime stopDate = new DateTime(1997, 08, 20, 23, 15, 15, 369);
            _fixture.Observation.SceneStopTime.Should().Be(stopDate);
        }
        
        [Fact]
        public void License_Should_Be_Open_Government_Canada()
        {
            _fixture.Observation.License.Should().BeOfType(typeof(OpenGovernmentCanadaLicense));
        }
        
        [Fact]
        public void License_Name_Should_Be_Open_Government_Canada()
        {
            string name = "Open Government License - Canada";
            _fixture.Observation.License.LicenseName.Should().Be(name);
        }
         
        [Fact]
        public void License_Description_Should_Be_Open_Government_Canada()
        {
            string description = @"Open Government is about making government more accessible to everyone. 
                                        This means giving greater access to government data and information to the Canadian public and the businesses community. 
                                        The Information Provider grants you a worldwide, royalty-free, perpetual, non-exclusive licence to use the Information, 
                                        including for commercial purposes, subject to the license terms. 
                                        This licence is governed by the laws of the province of Ontario and the applicable laws of Canada.";
            _fixture.Observation.License.LicenseDescription.Should().Be(description);
        }

        [Fact]
        public void License_Uri_Should_Be_To_Open_Government_Canada_Online()
        {
            Uri openGovtTerms = new Uri("https://open.canada.ca/en/open-government-licence-canada");
            _fixture.Observation.License.LicenseTerms.Should().Be(openGovtTerms);
        }
    }
}
