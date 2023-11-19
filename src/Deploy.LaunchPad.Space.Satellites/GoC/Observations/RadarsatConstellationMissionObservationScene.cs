// ***********************************************************************
// Assembly         : Deploy.LaunchPad.Space.Satellites
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 07-26-2023
// ***********************************************************************
// <copyright file="RadarsatConstellationMissionObservationScene.cs" company="Deploy Software Solutions, inc.">
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
    /// Class RadarsatConstellationMissionObservationScene.
    /// Implements the <see cref="Deploy.LaunchPad.Space.Satellites.GoC.RadarsatObservationSceneCommonMetadata" />
    /// Implements the <see cref="Deploy.LaunchPad.Space.Satellites.GoC.IRadarsatObservationScene" />
    /// </summary>
    /// <seealso cref="Deploy.LaunchPad.Space.Satellites.GoC.RadarsatObservationSceneCommonMetadata" />
    /// <seealso cref="Deploy.LaunchPad.Space.Satellites.GoC.IRadarsatObservationScene" />
    [Serializable()]
    public partial class RadarsatConstellationMissionObservationScene : RadarsatObservationSceneCommonMetadata,
        IRadarsatObservationScene
    {


        /// <summary>
        /// Gets or sets the product identifier.
        /// </summary>
        /// <value>The product identifier.</value>
        [JsonProperty("productId")]
        public string ProductId { get; set; }


        /// <summary>
        /// Gets or sets a value indicating whether [open data].
        /// </summary>
        /// <value><c>true</c> if [open data]; otherwise, <c>false</c>.</value>
        [JsonProperty("openData")]
        public bool OpenData { get; set; } = false;



        /// <summary>
        /// Gets or sets the product datum.
        /// </summary>
        /// <value>The product datum.</value>
        [JsonProperty("productDatum")]
        public string ProductDatum { get; set; }

        /// <summary>
        /// Gets or sets the processor version.
        /// </summary>
        /// <value>The processor version.</value>
        [JsonProperty("processorVersion")]
        public string ProcessorVersion { get; set; }

        /// <summary>
        /// Gets or sets the sampled pixel spacing.
        /// </summary>
        /// <value>The sampled pixel spacing.</value>
        [JsonProperty("sampledPixelSpacing")]
        public double SampledPixelSpacing { get; set; }



        /// <summary>
        /// Gets or sets the client order item number.
        /// </summary>
        /// <value>The client order item number.</value>
        [JsonProperty("clientOrderItemNumber")]
        public string ClientOrderItemNumber { get; set; }

        /// <summary>
        /// Gets or sets the type of the beam mode.
        /// </summary>
        /// <value>The type of the beam mode.</value>
        [JsonProperty("beamModeType")]
        public string BeamModeType { get; set; }


        /// <summary>
        /// Gets or sets the height of the geodetic terrain.
        /// </summary>
        /// <value>The height of the geodetic terrain.</value>
        [JsonProperty("geodeticTerrainHeight")]
        public double GeodeticTerrainHeight { get; set; }

        /// <summary>
        /// Gets or sets the client order number.
        /// </summary>
        /// <value>The client order number.</value>
        [JsonProperty("clientOrderNumber")]
        public string ClientOrderNumber { get; set; }

        /// <summary>
        /// Gets or sets the acquisition start date.
        /// </summary>
        /// <value>The acquisition start date.</value>
        [JsonProperty("acquisitionStartDate")]
        public DateTime AcquisitionStartDate { get; set; }

        /// <summary>
        /// Gets or sets the supplier order item number.
        /// </summary>
        /// <value>The supplier order item number.</value>
        [JsonProperty("supplierOrderItemNumber")]
        public string SupplierOrderItemNumber { get; set; }

        /// <summary>
        /// Gets or sets the product catalog identifier.
        /// </summary>
        /// <value>The product catalog identifier.</value>
        [JsonProperty("productCatalogId")]
        public string ProductCatalogId { get; set; }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>The title.</value>
        [JsonProperty("title")]
        public string Title { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="RadarsatConstellationMissionObservationScene"/> class.
        /// </summary>
        protected RadarsatConstellationMissionObservationScene() : base()
        {
            CurrentLocation = new SpaceTimeInformation();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RadarsatConstellationMissionObservationScene"/> class.
        /// </summary>
        /// <param name="title">The title.</param>
        public RadarsatConstellationMissionObservationScene(string title) : base()
        {
            Title = title;
            CurrentLocation = new SpaceTimeInformation();
        }
    }
}
