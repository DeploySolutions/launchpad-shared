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
    using DeploySoftware.LaunchPad.Core.Abp.Domain;
    using Microsoft.CodeAnalysis;
    using System.IO;
    using System.Reflection;
    using System;

    public class FileTests : IClassFixture<FileTestsFixture>
    {
        #region "Test Classes"


        private readonly FileTestsFixture _fixture;

        #endregion

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
            location = new WindowsFileStorageLocation(drive.Name, new Uri(driveRoot));
            this._fixture.Initialize(location);
        }

        [Fact]
        public void Root_Folder_Name_Should_NotBeNullOrEmpty()
        {
            _fixture.SUT.Name.Should().NotBeNullOrEmpty();
        }


    }
}
