// ***********************************************************************
// Assembly         : Deploy.LaunchPad.Core.Tests
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 01-22-2023
// ***********************************************************************
// <copyright file="FileTestsFixture.cs" company="Deploy.LaunchPad.Core.Tests">
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
    using System;
    using Deploy.LaunchPad.Core.Abp.Domain;
    using Deploy.LaunchPad.Util.Files.Storage;

    /// <summary>
    /// Class FileTestsFixture.
    /// Implements the <see cref="IDisposable" />
    /// </summary>
    /// <seealso cref="IDisposable" />
    public partial class FileTestsFixture : IDisposable
    {
        /// <summary>
        /// Gets or sets the sut.
        /// </summary>
        /// <value>The sut.</value>
        public WindowsFileStorageLocation SUT { get; set; }


        /// <summary>
        /// Initializes a new instance of the <see cref="FileTestsFixture"/> class.
        /// </summary>
        public FileTestsFixture()
        {

        }

        /// <summary>
        /// Initializes the specified location.
        /// </summary>
        /// <param name="location">The location.</param>
        public void Initialize(WindowsFileStorageLocation location)
        {
            SUT = location;

        }

        /// <summary>
        /// Disposes this instance.
        /// </summary>
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
