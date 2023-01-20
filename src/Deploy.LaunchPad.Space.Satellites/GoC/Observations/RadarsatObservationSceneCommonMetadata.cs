﻿using Deploy.LaunchPad.Core.Domain;
using Deploy.LaunchPad.Space.Satellites.Core;
using Newtonsoft.Json;
using System;

namespace Deploy.LaunchPad.Space.Satellites.GoC
{
    [Serializable()]
    public partial class RadarsatObservationSceneCommonMetadata<TPrimaryKey, TFileStorageLocationType> : EarthObservationBase<TPrimaryKey, TFileStorageLocationType>,
        IRadarsatObservationScene<TPrimaryKey, TFileStorageLocationType>
        where TFileStorageLocationType : IFileStorageLocation, new()
    {
        [JsonProperty("recordId")]
        public string RecordId { get; set; }

        [JsonProperty("collectionId")]
        public string CollectionId { get; set; }

        [JsonProperty("productType")]
        public string ProductType { get; set; }

        [JsonProperty("overviewUrl")]
        public Uri OverviewUrl { get; set; }

        [JsonProperty("incidenceAngle")]
        public double IncidenceAngle { get; set; }

        [JsonProperty("lookOrientation")]
        public string LookOrientation { get; set; }

        [JsonProperty("publicGood")]
        public bool PublicGood { get; set; } = false;

        [JsonProperty("metadataFullUrl")]
        public Uri MetadataFullUrl { get; set; }

        [JsonProperty("orbitDirection")]
        public string OrbitDirection { get; set; }

        [JsonProperty("quicklookFullName")]
        public string QuicklookFullName { get; set; }

        [JsonProperty("metadataFullName")]
        public string MetadataFullName { get; set; }

        [JsonProperty("featureId")]
        public string FeatureId { get; set; }

        [JsonProperty("sequenceId")]
        public string SequenceId { get; set; }

        [JsonProperty("relatedProducts")]
        public string RelatedProducts { get; set; }

        [JsonProperty("serviceUuid")]
        public string ServiceUuid { get; set; }

        [JsonProperty("datasetId")]
        public string datasetId { get; set; }


    }
}