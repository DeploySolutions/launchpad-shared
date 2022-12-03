﻿//LaunchPad Shared
// Copyright (c) 2018-2022 Deploy Software Solutions, inc. 

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

    using System.IO;
    using System.Reflection;
    using System;
    using DeploySoftware.LaunchPad.Core.Abp.Util;
    using DeploySoftware.LaunchPad.Core.Abp.Domain;

    public class FileTestsFixture : IDisposable
    {
        public WindowsFileStorageLocation SUT { get; set; }


        public FileTestsFixture()
        {

        }

        public void Initialize(WindowsFileStorageLocation location)
        {
            SUT = location;

        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
