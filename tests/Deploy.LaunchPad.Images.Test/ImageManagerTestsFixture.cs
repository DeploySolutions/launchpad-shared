
// ***********************************************************************
// Assembly         : Deploy.LaunchPad.Images.Tests
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 01-08-2023
// ***********************************************************************
// <copyright file="ImageManagerTestsFixture.cs" company="Deploy.LaunchPad.Images.Tests">
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

using System.Drawing;
using System.IO;
using System.Resources;
using Deploy.LaunchPad.Core;
using Deploy.LaunchPad.Util;

namespace Deploy.LaunchPad.Images.Tests
{
    using ImageMagick;
    using System;
    using Deploy.LaunchPad.Images.Domain;

    /// <summary>
    /// Class ImageManagerTestsFixture.
    /// Implements the <see cref="IDisposable" />
    /// </summary>
    /// <seealso cref="IDisposable" />
    public partial class ImageManagerTestsFixture : IDisposable
    {
        /// <summary>
        /// Gets or sets the sut.
        /// </summary>
        /// <value>The sut.</value>
        public ImageManager SUT { get; set; }

        /// <summary>
        /// Gets or sets the settings.
        /// </summary>
        /// <value>The settings.</value>
        public CompareSettings Settings { get; set; }

        /// <summary>
        /// Gets or sets the not empty bytes.
        /// </summary>
        /// <value>The not empty bytes.</value>
        public byte[] NotEmptyBytes { get; set; }

        /// <summary>
        /// Gets or sets the logo white.
        /// </summary>
        /// <value>The logo white.</value>
        public byte[] LogoWhite { get; set; }

        /// <summary>
        /// Gets or sets the logo dark.
        /// </summary>
        /// <value>The logo dark.</value>
        public byte[] LogoDark { get; set; }

        /// <summary>
        /// Initializes the specified image man.
        /// </summary>
        /// <param name="imageMan">The image man.</param>
        /// <param name="compareSettings">The compare settings.</param>
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

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {

        }
    }
}
