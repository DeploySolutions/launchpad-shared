//LaunchPad Space
// Copyright (c) 2018-2023 Deploy Software Solutions, inc. 

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
    using Abp.Timing;
    using Deploy.LaunchPad.Core.Abp.Domain;
    using Deploy.LaunchPad.Core.Domain;
    using Deploy.LaunchPad.Space.Satellites.Core;
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("DssRadarsat1Observations")]
    public partial class Radarsat1ObservationScene<TPrimaryKey, TFileStorageLocationType> : EarthObservationBase<TPrimaryKey, TFileStorageLocationType>,
        IRadarsatObservationScene<TPrimaryKey, TFileStorageLocationType>
        where TFileStorageLocationType : IFileStorageLocation, new()
    {
        public enum FileTypes
        {
            Nvol = 0,
            Sard = 1,
            Sarl = 2,
            Sart = 3,
            Tif = 4,
            Tfw = 5,
            Vol = 6
        }

        [Required]
        public string SceneId { get; set; }

        [Required]
        public string MdaOrderNumber { get; set; }

        [Required]
        public string GeographicalArea { get; set; }

        [Required]
        public DateTime SceneStartTime { get; set; }

        [Required]
        public DateTime SceneStopTime { get; set; }

        [Required]
        public string Orbit { get; set; }

        [Required]
        public string OrbitDataType { get; set; }

        [Required]
        public string ApplicationLut { get; set; }

        [Required]
        public string BeamMode { get; set; }

        [Required]
        public string ProductType { get; set; }

        [Required]
        public string Format { get; set; }

        [Required]
        public int NumberImageLines { get; set; }

        [Required]
        public int NumberImagePixels { get; set; }

        [Required]
        public string PixelSpacing { get; set; }

        public Radarsat1ObservationFiles Files { get; set; }

        public Radarsat1ObservationScene(
           int? tenantId,
           string sceneId)
        {
            SceneId = sceneId;
        }

        public Radarsat1ObservationScene(
           int? tenantId,
           string sceneId,
           string mdaOrderNumber,
           string geographicalArea,
           DateTime sceneStart,
           DateTime sceneStop,
           string orbit,
           string orbitDataType,
           string applicationLut,
           string beamMode,
           string productType,
           string format,
           int numberImageLines,
           int numberImagePixels,
           string pixelSpacing,
           GeographicLocation sceneCentre,
           ImageObservationCornerCoordinates cornerCoordinates
        ) : base()
        {
            CurrentLocation = new SpaceTimeInformation();
            SceneId = sceneId;
            MdaOrderNumber = mdaOrderNumber;
            GeographicalArea = geographicalArea;
            SceneStartTime = sceneStart;
            SceneStopTime = sceneStop;
            Orbit = orbit;
            OrbitDataType = orbitDataType;
            ApplicationLut = applicationLut;
            BeamMode = beamMode;
            ProductType = productType;
            Format = format;
            NumberImageLines = numberImageLines;
            NumberImagePixels = numberImagePixels;
            PixelSpacing = pixelSpacing;
            SceneCentre = sceneCentre;
            Corners = cornerCoordinates;
            CreationTime = Clock.Now;
            LastModificationTime = Clock.Now;
            CurrentLocation.PhysicalLocation = SceneCentre;
            CurrentLocation.PointInTime = SceneStartTime;
        }

        public Radarsat1ObservationScene(
           TPrimaryKey id,
           int? tenantId,
           string sceneId,
           string mdaOrderNumber,
           string geographicalArea,
           DateTime sceneStart,
           DateTime sceneStop,
           string orbit,
           string orbitDataType,
           string applicationLut,
           string beamMode,
           string productType,
           string format,
           int numberImageLines,
           int numberImagePixels,
           string pixelSpacing,
           GeographicLocation sceneCentre,
           ImageObservationCornerCoordinates cornerCoordinates
        ) : base()
        {
            Id = id;
            new Radarsat1ObservationScene<TPrimaryKey, TFileStorageLocationType>(
                tenantId,
                sceneId,
                mdaOrderNumber,
                geographicalArea,
                sceneStart,
                sceneStop,
                orbit,
                orbitDataType,
                applicationLut,
                beamMode,
                productType,
                format,
                numberImageLines,
                numberImagePixels,
                pixelSpacing,
                sceneCentre,
                cornerCoordinates
            );
        }

        protected Radarsat1ObservationScene() : base()
        {
            CurrentLocation = new SpaceTimeInformation();
        }

        public class Radarsat1ObservationFiles : IObservationFiles<Guid>
        {
            public NvolFile<Guid> Nvol { get; set; }

            public SardFile<Guid> Sard { get; set; }

            public SarlFile<Guid> Sarl { get; set; }

            public SartFile<Guid> Sart { get; set; }

            public TifFile<Guid> Tif { get; set; }

            public TifWorldFile<Guid> Tfw { get; set; }

            public VolFile<Guid> Vol { get; set; }
        }

    }
}
