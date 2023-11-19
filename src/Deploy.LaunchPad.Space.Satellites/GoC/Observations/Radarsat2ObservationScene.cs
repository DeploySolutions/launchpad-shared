// ***********************************************************************
// Assembly         : Deploy.LaunchPad.Space.Satellites
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 07-26-2023
// ***********************************************************************
// <copyright file="Radarsat2ObservationScene.cs" company="Deploy Software Solutions, inc.">
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

// Radarsat metadata is licensed under the Open Government License of the Canadian Federal Government, 2.0
// For more information, please consult the terms and conditions of this license at
// https://open.canada.ca/en/open-government-licence-canada 

namespace Deploy.LaunchPad.Space.Satellites.GoC
{

    using Deploy.LaunchPad.Core.Domain;
    using Deploy.LaunchPad.Core.Geospatial;
    using Newtonsoft.Json;
    using System;

    /// <summary>
    /// Class Radarsat2ObservationScene.
    /// Implements the <see cref="Deploy.LaunchPad.Space.Satellites.GoC.RadarsatObservationSceneCommonMetadata" />
    /// Implements the <see cref="Deploy.LaunchPad.Space.Satellites.GoC.IRadarsatObservationScene" />
    /// </summary>
    /// <seealso cref="Deploy.LaunchPad.Space.Satellites.GoC.RadarsatObservationSceneCommonMetadata" />
    /// <seealso cref="Deploy.LaunchPad.Space.Satellites.GoC.IRadarsatObservationScene" />
    [Serializable()]
    public partial class Radarsat2ObservationScene : RadarsatObservationSceneCommonMetadata,
        IRadarsatObservationScene
    {

        /// <summary>
        /// Gets or sets the product identifier.
        /// </summary>
        /// <value>The product identifier.</value>
        [JsonProperty("productId")]
        public string ProductId { get; set; }


        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>The title.</value>
        [JsonProperty("title")]
        public string Title { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Radarsat2ObservationScene"/> class.
        /// </summary>
        protected Radarsat2ObservationScene() : base()
        {
            CurrentLocation = new SpaceTimeInformation();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Radarsat2ObservationScene"/> class.
        /// </summary>
        /// <param name="title">The title.</param>
        public Radarsat2ObservationScene(string title) : base()
        {
            Title = title;
            CurrentLocation = new SpaceTimeInformation();
        }
    }
}
