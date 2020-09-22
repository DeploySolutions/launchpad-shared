
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

using System.Drawing;
using System.IO;
using System.Resources;
using DeploySoftware.LaunchPad.Core;
using DeploySoftware.LaunchPad.Core.Util;

namespace DeploySoftware.LaunchPad.Images.Tests
{
    using ImageMagick;
    using System;
    using DeploySoftware.LaunchPad.Images.Domain;

    public class ThumbnailGeneratorTestsFixture : IDisposable
    {
        public ThumbnailGenerator SUT { get; set; }

        public CompareSettings Settings { get; set; }

        public byte[] NotEmptyBytes { get; set; }

        public byte[] Logo { get; set; }

        public readonly string LogoFilePath;

        public readonly string LogoFileName;

        public ThumbnailGeneratorTestsFixture()
        {
            NotEmptyBytes = new byte[1] { 0x20 };
            System.Reflection.Assembly a = 
                System.Reflection.Assembly.GetExecutingAssembly();
            LogoFileName = a.GetName().Name + "." + "logoWhite.png";
            string path = System.IO.Path.GetDirectoryName( 
                System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase);
            LogoFilePath = path.Substring((6)) + "\\" + "logoWhite.png";
            Stream s = a.GetManifestResourceStream(LogoFileName);
            Guard.Against< ArgumentException>(s == null, "Logo not found");
            using (BinaryReader br = new BinaryReader(s))
            {
                Logo = br.ReadBytes((int)s.Length);
            }
        }
        
        public void Initialize(ThumbnailGenerator generator, CompareSettings compareSettings)
        {
            SUT = generator;
            Settings = compareSettings;
        }

        public void Dispose()
        {

        }
    }
}
