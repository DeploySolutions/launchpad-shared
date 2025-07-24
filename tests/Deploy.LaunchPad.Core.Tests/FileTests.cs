// ***********************************************************************
// Assembly         : Deploy.LaunchPad.Core.Tests
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 03-21-2023
// ***********************************************************************
// <copyright file="FileTests.cs" company="Deploy.LaunchPad.Core.Tests">
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
    
    using Deploy.LaunchPad.Core.Domain;
    using Deploy.LaunchPad.Core.Abp.Domain;
    using Microsoft.CodeAnalysis;
    using System.IO;
    using System.Reflection;
    using System;
    using Castle.Core.Logging;
    using Deploy.LaunchPad.Core.Files.Storage;

    /// <summary>
    /// Class FileTests.
    /// Implements the <see cref="Xunit.IClassFixture{Deploy.LaunchPad.Core.Tests.FileTestsFixture}" />
    /// </summary>
    /// <seealso cref="Xunit.IClassFixture{Deploy.LaunchPad.Core.Tests.FileTestsFixture}" />
    public partial class FileTests : IClassFixture<FileTestsFixture>
    {
        #region "Test Classes"


        /// <summary>
        /// The fixture
        /// </summary>
        private readonly FileTestsFixture _fixture;

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="FileTests"/> class.
        /// </summary>
        /// <param name="fixture">The fixture.</param>
        public FileTests(FileTestsFixture fixture)
        {
            _fixture = fixture;
            WindowsFileStorageLocation location = null;
            string codeBase = Assembly.GetExecutingAssembly().CodeBase;
            UriBuilder uri = new UriBuilder(codeBase);
            string path = Uri.UnescapeDataString(uri.Path);
            FileInfo file = new FileInfo(path);
            DriveInfo drive = new DriveInfo(file.Directory.Root.FullName);
            string driveRoot = drive.RootDirectory.FullName;
            location = new WindowsFileStorageLocation(NullLogger.Instance, drive.Name, new Uri(driveRoot));
            this._fixture.Initialize(location);
        }

        /// <summary>
        /// Defines the test method Root_Folder_Name_Should_NotBeNullOrEmpty.
        /// </summary>
        [Fact]
        public void Root_Folder_Name_Should_NotBeNullOrEmpty()
        {
            Assert.NotNull(_fixture.SUT.Name.Full);
        }


    }
}
