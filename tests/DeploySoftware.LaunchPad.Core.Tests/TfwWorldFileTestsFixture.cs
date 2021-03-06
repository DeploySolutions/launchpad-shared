﻿//LaunchPad Shared
// Copyright (c) 2018-2020 Deploy Software Solutions, inc. 

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
    using DeploySoftware.LaunchPad.Core.Domain;
    using DeploySoftware.LaunchPad.Core.Util;
    using System;

    public class TfwWorldFileTestsFixture : IDisposable
    {
        public readonly TifWorldFileParser<Guid,WindowsFileStorageLocation> TfwParser;

        public readonly TifWorldFile<Guid,WindowsFileStorageLocation> TfwFile;

        public TfwWorldFileTestsFixture()
        {
            string codeBase = Assembly.GetExecutingAssembly().CodeBase;
            UriBuilder uri = new UriBuilder(codeBase);
            string path = Uri.UnescapeDataString(uri.Path);
            string fileName = Path.GetDirectoryName(path) + "\\" + "TifWorldFileTest.tfw";
            TfwParser = new TifWorldFileParser<Guid,WindowsFileStorageLocation>();
            TfwFile = TfwParser.GetTifWorldFileFromMetadataFile(fileName);
        }

        public void Initialize(string radarsat1MetadataFilename)
        {
            
        }

        public void Dispose()
        {

        }
    }
}
