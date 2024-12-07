
// ***********************************************************************
// Assembly         : Deploy.LaunchPad.Images.Tests
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 01-08-2023
// ***********************************************************************
// <copyright file="ThumbnailGeneratorTestsFixture.cs" company="Deploy.LaunchPad.Images.Tests">
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
    /// Class ThumbnailGeneratorTestsFixture.
    /// Implements the <see cref="IDisposable" />
    /// </summary>
    /// <seealso cref="IDisposable" />
    public partial class ThumbnailGeneratorTestsFixture : IDisposable
    {
        /// <summary>
        /// Gets or sets the sut.
        /// </summary>
        /// <value>The sut.</value>
        public ThumbnailGenerator SUT { get; set; }

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
        /// Gets or sets the logo.
        /// </summary>
        /// <value>The logo.</value>
        public byte[] Logo { get; set; }

        /// <summary>
        /// The logo file path
        /// </summary>
        public readonly string LogoFilePath;

        /// <summary>
        /// The logo file name
        /// </summary>
        public readonly string LogoFileName;

        /// <summary>
        /// Initializes a new instance of the <see cref="ThumbnailGeneratorTestsFixture"/> class.
        /// </summary>
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

        /// <summary>
        /// Initializes the specified generator.
        /// </summary>
        /// <param name="generator">The generator.</param>
        /// <param name="compareSettings">The compare settings.</param>
        public void Initialize(ThumbnailGenerator generator, CompareSettings compareSettings)
        {
            SUT = generator;
            Settings = compareSettings;
        }

        /// <summary>
        /// Disposes this instance.
        /// </summary>
        public void Dispose()
        {

        }
    }
}
