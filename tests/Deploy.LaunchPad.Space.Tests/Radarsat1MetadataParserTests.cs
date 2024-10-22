// ***********************************************************************
// Assembly         : Deploy.LaunchPad.Space.Tests
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 01-22-2023
// ***********************************************************************
// <copyright file="Radarsat1MetadataParserTests.cs" company="Deploy.LaunchPad.Space.Tests">
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


namespace Deploy.LaunchPad.Space.Tests
{

    using System;
    using Xunit;
    using FluentAssertions;
    using Deploy.LaunchPad.Core.Domain;
    using Deploy.LaunchPad.Core;
    using Deploy.LaunchPad.Space.Satellites;

    /// <summary>
    /// Class Radarsat1MetadataParserTests.
    /// Implements the <see cref="Xunit.IClassFixture{Deploy.LaunchPad.Space.Tests.Radarsat1MetadataFileFixture}" />
    /// </summary>
    /// <seealso cref="Xunit.IClassFixture{Deploy.LaunchPad.Space.Tests.Radarsat1MetadataFileFixture}" />
    public partial class Radarsat1MetadataParserTests : IClassFixture<Radarsat1MetadataFileFixture>
    {
        #region "Test Classes"



        #endregion

        /// <summary>
        /// The fixture
        /// </summary>
        private readonly Radarsat1MetadataFileFixture _fixture;

        /// <summary>
        /// Initializes a new instance of the <see cref="Radarsat1MetadataParserTests"/> class.
        /// </summary>
        /// <param name="fixture">The fixture.</param>
        public Radarsat1MetadataParserTests(Radarsat1MetadataFileFixture fixture)
        {
            _fixture = fixture;
            _fixture.Initialize("Radarsat1MetadataTest.en.txt");
        }


        /// <summary>
        /// Defines the test method Scene_ID_Should_Not_Be_NullOrEmpty.
        /// </summary>
        [Fact]
        public void Scene_ID_Should_Not_Be_NullOrEmpty()
        {
            _fixture.Observation.SceneId.Should().NotBeNullOrEmpty();
        }

        /// <summary>
        /// Defines the test method Scene_ID_Should_Have_Correct_Value.
        /// </summary>
        [Fact]
        public void Scene_ID_Should_Have_Correct_Value()
        {

            _fixture.Observation.SceneId.Should().Be("m0700836");
        }

        /// <summary>
        /// Defines the test method Mda_Order_Number_Should_Not_Be_NullOrEmpty.
        /// </summary>
        [Fact]
        public void Mda_Order_Number_Should_Not_Be_NullOrEmpty()
        {
            _fixture.Observation.MdaOrderNumber.Should().NotBeNullOrEmpty();
        }

        /// <summary>
        /// Defines the test method Mda_Order_Number_Should_Have_Correct_Value.
        /// </summary>
        [Fact]
        public void Mda_Order_Number_Should_Have_Correct_Value()
        {

            _fixture.Observation.MdaOrderNumber.Should().Be("OGD_12546");
        }

        /// <summary>
        /// Defines the test method Geographical_Area_Should_Not_Be_NullOrEmpty.
        /// </summary>
        [Fact]
        public void Geographical_Area_Should_Not_Be_NullOrEmpty()
        {
            _fixture.Observation.GeographicalArea.Should().NotBeNullOrEmpty();
        }

        /// <summary>
        /// Defines the test method Geographical_Area_Should_Have_Correct_Value.
        /// </summary>
        [Fact]
        public void Geographical_Area_Should_Have_Correct_Value()
        {

            _fixture.Observation.GeographicalArea.Should().Be("Toronto");
        }

        /// <summary>
        /// Defines the test method Scene_Start_Time_Should_Not_Be_NullOrEmpty.
        /// </summary>
        [Fact]
        public void Scene_Start_Time_Should_Not_Be_NullOrEmpty()
        {
            _fixture.Observation.SceneStartTime.Should().NotHaveYear(0001);
        }

        /// <summary>
        /// Defines the test method Scene_Start_Time_Should_Have_Correct_Value.
        /// </summary>
        [Fact]
        public void Scene_Start_Time_Should_Have_Correct_Value()
        {
            DateTime startDate = new DateTime(1997, 08, 20, 23, 14, 59, 649);
            _fixture.Observation.SceneStartTime.Should().Be(startDate);
        }


        /// <summary>
        /// Defines the test method Scene_Stop_Time_Should_Not_Be_NullOrEmpty.
        /// </summary>
        [Fact]
        public void Scene_Stop_Time_Should_Not_Be_NullOrEmpty()
        {
            _fixture.Observation.SceneStopTime.Should().NotHaveYear(0001);
        }

        /// <summary>
        /// Defines the test method Scene_Stop_Time_Should_Have_Correct_Value.
        /// </summary>
        [Fact]
        public void Scene_Stop_Time_Should_Have_Correct_Value()
        {
            DateTime stopDate = new DateTime(1997, 08, 20, 23, 15, 15, 369);
            _fixture.Observation.SceneStopTime.Should().Be(stopDate);
        }

        /// <summary>
        /// Defines the test method License_Should_Be_Open_Government_Canada.
        /// </summary>
        [Fact]
        public void License_Should_Be_Open_Government_Canada()
        {
            _fixture.Observation.Copyright.GoverningLicense.Should().BeOfType(typeof(OpenGovernmentCanadaLicense));
        }

        /// <summary>
        /// Defines the test method License_Name_Should_Be_Open_Government_Canada.
        /// </summary>
        [Fact]
        public void License_Name_Should_Be_Open_Government_Canada()
        {
            ElementName name = new ElementName( Deploy_LaunchPad_Core_Resources.Text_OpenGovernmentCanadaLicense_LicenseName);
            _fixture.Observation.Copyright.GoverningLicense.Name.Should().Be(name);
        }

        /// <summary>
        /// Defines the test method License_Description_Should_Be_Open_Government_Canada.
        /// </summary>
        [Fact]
        public void License_Description_Should_Be_Open_Government_Canada()
        {
            ElementDescription description = new ElementDescription (Deploy_LaunchPad_Core_Resources.Text_OpenGovernmentCanadaLicense_LicenseDescription);
            _fixture.Observation.Copyright.GoverningLicense.Description.Should().Be(description);
        }

        /// <summary>
        /// Defines the test method License_Uri_Should_Be_To_Open_Government_Canada_Online.
        /// </summary>
        [Fact]
        public void License_Uri_Should_Be_To_Open_Government_Canada_Online()
        {
            Uri openGovtTerms = new Uri(Deploy_LaunchPad_Core_Resources.Text_OpenGovernmentCanadaLicense_LicenseTerms);
            _fixture.Observation.Copyright.GoverningLicense.FullTermsUrl.Should().Be(openGovtTerms);
        }


        /// <summary>
        /// Defines the test method Copyright_Owner_Should_Be_To_Canada_Space_Agency.
        /// </summary>
        [Fact]
        public void Copyright_Owner_Should_Be_To_Canada_Space_Agency()
        {
            
            _fixture.Observation.Copyright.Owner.Should().Be(Deploy_LaunchPad_Space_Resources.Text_Radarsat1DataUsageRights_Owner);
        }

        /// <summary>
        /// Defines the test method Copyright_Information_Should_Be_To_Canada_Space_Agency.
        /// </summary>
        [Fact]
        public void Copyright_Information_Should_Be_To_Canada_Space_Agency()
        {
            _fixture.Observation.Copyright.Attribution.Should().Be(Deploy_LaunchPad_Space_Resources.Text_Radarsat1DataUsageRights_Attribution);
        }

    }
}
