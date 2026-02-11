// ***********************************************************************
// Assembly         : Deploy.LaunchPad.Core.Tests
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 01-22-2023
// ***********************************************************************
// <copyright file="TfwWorldFileTestsFixture.cs" company="Deploy.LaunchPad.Core.Tests">
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

    using System.IO;
    using System.Reflection;
    using Deploy.LaunchPad.Core.Domain;
    using Deploy.LaunchPad.Util;
    using System;
    using Deploy.LaunchPad.Core.Abp.Domain;
    using Deploy.LaunchPad.Util.Files.Storage;
    using Deploy.LaunchPad.Util.Files;
    using Deploy.LaunchPad.Core.Abp;

    /// <summary>
    /// Class TfwWorldFileTestsFixture.
    /// Implements the <see cref="IDisposable" />
    /// </summary>
    /// <seealso cref="IDisposable" />
    public partial class TfwWorldFileTestsFixture : IDisposable
    {
        /// <summary>
        /// The TFW parser
        /// </summary>
        public readonly TifWorldFileParser<Guid,WindowsFileStorageLocation> TfwParser;

        /// <summary>
        /// The TFW file
        /// </summary>
        public readonly TifWorldFile TfwFile;

        /// <summary>
        /// Initializes a new instance of the <see cref="TfwWorldFileTestsFixture"/> class.
        /// </summary>
        public TfwWorldFileTestsFixture()
        {
            string codeBase = Assembly.GetExecutingAssembly().CodeBase;
            UriBuilder uri = new UriBuilder(codeBase);
            string path = Uri.UnescapeDataString(uri.Path);
            string fileName = Path.GetDirectoryName(path) + "\\" + "TifWorldFileTest.tfw";
            TfwParser = new TifWorldFileParser<Guid,WindowsFileStorageLocation>();
            TfwFile = TfwParser.GetTifWorldFileFromMetadataFile(fileName);
        }

        /// <summary>
        /// Initializes the specified radarsat1 metadata filename.
        /// </summary>
        /// <param name="radarsat1MetadataFilename">The radarsat1 metadata filename.</param>
        public void Initialize(string radarsat1MetadataFilename)
        {
            
        }

        /// <summary>
        /// Disposes this instance.
        /// </summary>
        public void Dispose()
        {

        }
    }
}
