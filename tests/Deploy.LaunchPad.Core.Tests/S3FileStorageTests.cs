// ***********************************************************************
// Assembly         : Deploy.LaunchPad.Core.Tests
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 01-22-2023
// ***********************************************************************
// <copyright file="S3FileStorageTests.cs" company="Deploy.LaunchPad.Core.Tests">
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


namespace Deploy.LaunchPad.Domain.Tests
{
    using Deploy.LaunchPad.AWS;
    using Deploy.LaunchPad.AWS.S3;
    using Deploy.LaunchPad.Domain;
    
    using Xunit;

    /// <summary>
    /// Class S3FileStorageTests.
    /// Implements the <see cref="Xunit.IClassFixture{Deploy.LaunchPad.Core.Tests.S3FileStorageTestsFixture}" />
    /// </summary>
    /// <seealso cref="Xunit.IClassFixture{Deploy.LaunchPad.Core.Tests.S3FileStorageTestsFixture}" />
    public partial class S3FileStorageTests : IClassFixture<S3FileStorageTestsFixture>
    {

        /// <summary>
        /// The fixture
        /// </summary>
        private readonly S3FileStorageTestsFixture _fixture;

        /// <summary>
        /// Initializes a new instance of the <see cref="S3FileStorageTests"/> class.
        /// </summary>
        /// <param name="fixture">The fixture.</param>
        public S3FileStorageTests(S3FileStorageTestsFixture fixture)
        {
            _fixture = fixture;
        }

        /// <summary>
        /// Defines the test method FileExtension_Should_Be_DotTFW.
        /// </summary>
        [Fact]
        public void FileExtension_Should_Be_DotTFW()
        {
            Assert.Equal(".tfw", _fixture.TfwFile.Extension);
        }

        /// <summary>
        /// Defines the test method A_Should_Equal_First_Line_In_File.
        /// </summary>
        [Fact]
        public void A_Should_Equal_First_Line_In_File()
        {
            Assert.Equal(12.413247108000000m, _fixture.TfwFile.A);
        }

        /// <summary>
        /// Defines the test method D_Should_Equal_Second_Line_In_File.
        /// </summary>
        [Fact]
        public void D_Should_Equal_Second_Line_In_File()
        {
            Assert.Equal(0.000000000000000m, _fixture.TfwFile.D);
        }

        /// <summary>
        /// Defines the test method B_Should_Equal_Third_Line_In_File.
        /// </summary>
        [Fact]
        public void B_Should_Equal_Third_Line_In_File()
        {
            Assert.Equal(0.000000000000000m, _fixture.TfwFile.B);
        }

        /// <summary>
        /// Defines the test method E_Should_Equal_Fourth_Line_In_File.
        /// </summary>
        [Fact]
        public void E_Should_Equal_Fourth_Line_In_File()
        {
            Assert.Equal(-12.382885933000001m, _fixture.TfwFile.E);
        }

        /// <summary>
        /// Defines the test method C_Should_Equal_Fifth_Line_In_File.
        /// </summary>
        [Fact]
        public void C_Should_Equal_Fifth_Line_In_File()
        {
            Assert.Equal(511283.0285078580m, _fixture.TfwFile.C);
        }

        /// <summary>
        /// Defines the test method F_Should_Equal_Sixth_Line_In_File.
        /// </summary>
        [Fact]
        public void F_Should_Equal_Sixth_Line_In_File()
        {
            Assert.Equal(7755841.0522885220m, _fixture.TfwFile.F);
        }

        /// <summary>
        /// Defines the test method Location_Region_Should_Be_Us_East.
        /// </summary>
        [Fact]
        public void Location_Region_Should_Be_Us_East()
        {
            Assert.Equal(S3BucketStorageLocation.DEFAULT_REGION, _fixture.TfwParser.Location.Region);
        }

        /// <summary>
        /// Defines the test method Location_Bucket_Root_Should_Be_Us_East.
        /// </summary>
        [Fact]
        public void Location_Bucket_Root_Should_Be_Us_East()
        {
            Assert.StartsWith("https://s3.us-east-1.amazonaws.com", _fixture.TfwParser.Location.RootUri.ToString());
        }

    }
}
