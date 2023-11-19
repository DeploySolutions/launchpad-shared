// ***********************************************************************
// Assembly         : Deploy.LaunchPad.Space.Tests
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 07-26-2023
// ***********************************************************************
// <copyright file="Radarsat1MetadataFileFixture.cs" company="Deploy.LaunchPad.Space.Tests">
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

namespace Deploy.LaunchPad.Space.Tests
{
    using Deploy.LaunchPad.Space.Satellites.GoC;
    using Deploy.LaunchPad.Core.Domain;
    using System;
    using Deploy.LaunchPad.Core.Abp.Domain;

    /// <summary>
    /// Class Radarsat1MetadataFileFixture.
    /// Implements the <see cref="IDisposable" />
    /// </summary>
    /// <seealso cref="IDisposable" />
    public partial class Radarsat1MetadataFileFixture : IDisposable
    {
        /// <summary>
        /// Gets or sets the observation.
        /// </summary>
        /// <value>The observation.</value>
        public Radarsat1ObservationScene Observation { get; set; }

        /// <summary>
        /// Initializes the specified radarsat1 metadata filename.
        /// </summary>
        /// <param name="radarsat1MetadataFilename">The radarsat1 metadata filename.</param>
        public void Initialize(string radarsat1MetadataFilename)
        {
            Radarsat1MetadataParser<Guid, WindowsFileStorageLocation> parser = new Radarsat1MetadataParser<Guid, WindowsFileStorageLocation>();
            Observation = parser.GetRadarsat1ObservationFromMetadataFile(radarsat1MetadataFilename);
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {

        }
    }
}
