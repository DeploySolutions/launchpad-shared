
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

namespace DeploySoftware.LaunchPad.Images.Tests
{
    using DeploySoftware.LaunchPad.Images;
    using ImageMagick;
    using System;

    public class ImageManagerTestsFixture : IDisposable
    {
        public ImageManager SUT { get; set; }

        public CompareSettings Settings { get; set; }

        public byte[] NotEmptyBytes { get; set; }
        

        public ImageManagerTestsFixture()
        {
        }

        public void Initialize(ImageManager imageMan)
        {
            SUT = imageMan;
            CompareSettings compareSettings = new CompareSettings();
            NotEmptyBytes = new byte[1] { 0x20 };
        }

        public void Dispose()
        {

        }
    }
}
