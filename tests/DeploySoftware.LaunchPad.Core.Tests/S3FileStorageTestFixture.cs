//LaunchPad Shared
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
    using DeploySoftware.LaunchPad.Core.Domain;
    using DeploySoftware.LaunchPad.Core.Util;
    using System;
    using DeploySoftware.LaunchPad.AWS;
    using DeploySoftware.LaunchPad.AWS.S3;
    using DeploySoftware.LaunchPad.Core.Abp.Util;
    using DeploySoftware.LaunchPad.Core.Abp.Domain;

    public class S3FileStorageTestsFixture : IDisposable
    {
        public readonly TifWorldFileParser<Guid,S3BucketStorageLocation> TfwParser;

        public readonly TifWorldFile<Guid> TfwFile;

        public S3FileStorageTestsFixture()
        {
            string codeBase = Assembly.GetExecutingAssembly().CodeBase;
            UriBuilder uri = new UriBuilder(codeBase);
            string path = Uri.UnescapeDataString(uri.Path);
            string fileName = Path.GetDirectoryName(path) + "\\" + "TifWorldFileTest.tfw";
            TfwParser = new TifWorldFileParser<Guid,S3BucketStorageLocation>();
            TfwFile = TfwParser.GetTifWorldFileFromMetadataFile(fileName);
        }

        public void Initialize(string radarsat1MetadataFilename)
        {
            
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
