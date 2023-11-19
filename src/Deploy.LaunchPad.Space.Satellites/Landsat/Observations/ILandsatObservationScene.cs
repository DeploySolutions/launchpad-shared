// ***********************************************************************
// Assembly         : Deploy.LaunchPad.Space.Satellites
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 07-26-2023
// ***********************************************************************
// <copyright file="ILandsatObservationScene.cs" company="Deploy Software Solutions, inc.">
//     2018-2023 Deploy Software Solutions, inc.
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

namespace Deploy.LaunchPad.Space.Satellites.Landsat
{
    using Deploy.LaunchPad.Core.Domain;
    using Deploy.LaunchPad.Space.Satellites.Core;

    /// <summary>
    /// Marker interface for Landsat satellite series observations
    /// </summary>
    public partial interface ILandsatObservationScene : IEarthObservationScene
    {
        /// <summary>
        /// Gets or sets the bucket.
        /// </summary>
        /// <value>The bucket.</value>
        public string Bucket { get; set; }

        /// <summary>
        /// Gets or sets the product identifier.
        /// </summary>
        /// <value>The product identifier.</value>
        public string ProductId { get; set; }

        /// <summary>
        /// Gets or sets the collection.
        /// </summary>
        /// <value>The collection.</value>
        public string Collection { get; set; }

        /// <summary>
        /// Gets or sets the projection.
        /// </summary>
        /// <value>The projection.</value>
        public string Projection { get; set; }

        /// <summary>
        /// Gets or sets the name of the sensor.
        /// </summary>
        /// <value>The name of the sensor.</value>
        public string SensorName { get; set; }

        /// <summary>
        /// Gets or sets the year acquired.
        /// </summary>
        /// <value>The year acquired.</value>
        public string YearAcquired { get; set; }

        /// <summary>
        /// Gets or sets the path.
        /// </summary>
        /// <value>The path.</value>
        public string Path { get; set; }

        /// <summary>
        /// Gets or sets the row.
        /// </summary>
        /// <value>The row.</value>
        public string Row { get; set; }

    }
}
