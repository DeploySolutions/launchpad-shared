
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
using Deploy.LaunchPad.Core;
using Deploy.LaunchPad.Core.Util;

namespace Deploy.LaunchPad.Images.Tests
{
    using ImageMagick;
    using System;
    using Deploy.LaunchPad.Images.Domain;

    public class ImageManagerTestsFixture : IDisposable
    {
        public ImageManager SUT { get; set; }

        public CompareSettings Settings { get; set; }

        public byte[] NotEmptyBytes { get; set; }

        public byte[] LogoWhite { get; set; }
        
        public byte[] LogoDark { get; set; }
        
        public void Initialize(ImageManager imageMan, CompareSettings compareSettings)
        {
            SUT = imageMan;
            Settings = compareSettings;
            NotEmptyBytes = new byte[1] { 0x20 };
            System.Reflection.Assembly a = 
                System.Reflection.Assembly.GetExecutingAssembly();
            string fileName = a.GetName().Name + "." + "logoWhite.png";
            Stream s = a.GetManifestResourceStream(fileName);
            Guard.Against< ArgumentException>(s == null, "Logo White not found");
            using (BinaryReader br = new BinaryReader(s))
            {

                LogoWhite = br.ReadBytes((int)s.Length);
            }
            
            string logoDarkFilename = a.GetName().Name + "." + "logoDark.png";
            Stream s2 = a.GetManifestResourceStream(logoDarkFilename);
            Guard.Against< ArgumentException>(s2 == null, "Logo Dark not found");
            using (BinaryReader br2 = new BinaryReader(s2))
            {

                LogoDark = br2.ReadBytes((int)s2.Length);
            }

        }

        public void Dispose()
        {

        }
    }
}
