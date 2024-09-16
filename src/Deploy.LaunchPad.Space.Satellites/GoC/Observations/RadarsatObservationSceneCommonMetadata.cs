// ***********************************************************************
// Assembly         : Deploy.LaunchPad.Space.Satellites
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 07-26-2023
// ***********************************************************************
// <copyright file="RadarsatObservationSceneCommonMetadata.cs" company="Deploy Software Solutions, inc.">
//     2018-2024 Deploy Software Solutions, inc.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Deploy.LaunchPad.Core.Domain;
using Deploy.LaunchPad.Space.Satellites.Core;
using Newtonsoft.Json;
using System;

namespace Deploy.LaunchPad.Space.Satellites.GoC
{
    /// <summary>
    /// Class RadarsatObservationSceneCommonMetadata.
    /// Implements the <see cref="EarthObservationModelBase" />
    /// Implements the <see cref="Deploy.LaunchPad.Space.Satellites.GoC.IRadarsatObservationScene" />
    /// </summary>
    /// <seealso cref="EarthObservationModelBase" />
    /// <seealso cref="Deploy.LaunchPad.Space.Satellites.GoC.IRadarsatObservationScene" />
    [Serializable()]
    public partial class RadarsatObservationSceneCommonMetadata : EarthObservationModelBase,
        IRadarsatObservationScene
    {
        /// <summary>
        /// Gets or sets the record identifier.
        /// </summary>
        /// <value>The record identifier.</value>
        [JsonProperty("recordId")]
        public string RecordId { get; set; }

        /// <summary>
        /// Gets or sets the collection identifier.
        /// </summary>
        /// <value>The collection identifier.</value>
        [JsonProperty("collectionId")]
        public string CollectionId { get; set; }

        /// <summary>
        /// Gets or sets the type of the product.
        /// </summary>
        /// <value>The type of the product.</value>
        [JsonProperty("productType")]
        public string ProductType { get; set; }

        /// <summary>
        /// Gets or sets the overview URL.
        /// </summary>
        /// <value>The overview URL.</value>
        [JsonProperty("overviewUrl")]
        public Uri OverviewUrl { get; set; }

        /// <summary>
        /// Gets or sets the incidence angle.
        /// </summary>
        /// <value>The incidence angle.</value>
        [JsonProperty("incidenceAngle")]
        public double IncidenceAngle { get; set; }

        /// <summary>
        /// Gets or sets the look orientation.
        /// </summary>
        /// <value>The look orientation.</value>
        [JsonProperty("lookOrientation")]
        public string LookOrientation { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [public good].
        /// </summary>
        /// <value><c>true</c> if [public good]; otherwise, <c>false</c>.</value>
        [JsonProperty("publicGood")]
        public bool PublicGood { get; set; } = false;

        /// <summary>
        /// Gets or sets the metadata full URL.
        /// </summary>
        /// <value>The metadata full URL.</value>
        [JsonProperty("metadataFullUrl")]
        public Uri MetadataFullUrl { get; set; }

        /// <summary>
        /// Gets or sets the orbit direction.
        /// </summary>
        /// <value>The orbit direction.</value>
        [JsonProperty("orbitDirection")]
        public string OrbitDirection { get; set; }

        /// <summary>
        /// Gets or sets the full name of the quicklook.
        /// </summary>
        /// <value>The full name of the quicklook.</value>
        [JsonProperty("quicklookFullName")]
        public string QuicklookFullName { get; set; }

        /// <summary>
        /// Gets or sets the full name of the metadata.
        /// </summary>
        /// <value>The full name of the metadata.</value>
        [JsonProperty("metadataFullName")]
        public string MetadataFullName { get; set; }

        /// <summary>
        /// Gets or sets the feature identifier.
        /// </summary>
        /// <value>The feature identifier.</value>
        [JsonProperty("featureId")]
        public string FeatureId { get; set; }

        /// <summary>
        /// Gets or sets the sequence identifier.
        /// </summary>
        /// <value>The sequence identifier.</value>
        [JsonProperty("sequenceId")]
        public string SequenceId { get; set; }

        /// <summary>
        /// Gets or sets the related products.
        /// </summary>
        /// <value>The related products.</value>
        [JsonProperty("relatedProducts")]
        public string RelatedProducts { get; set; }

        /// <summary>
        /// Gets or sets the service UUID.
        /// </summary>
        /// <value>The service UUID.</value>
        [JsonProperty("serviceUuid")]
        public string ServiceUuid { get; set; }

        /// <summary>
        /// Gets or sets the dataset identifier.
        /// </summary>
        /// <value>The dataset identifier.</value>
        [JsonProperty("datasetId")]
        public string datasetId { get; set; }


    }
}
