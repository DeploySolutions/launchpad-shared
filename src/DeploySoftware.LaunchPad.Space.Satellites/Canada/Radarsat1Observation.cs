//LaunchPad Space
// Copyright (c) 2018 Deploy Software Solutions, inc. 

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


namespace DeploySoftware.LaunchPad.Space.Satellites.Canada
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Abp.Domain.Entities;
    using Abp.Domain.Entities.Auditing;
    using Abp.Timing;
    using DeploySoftware.LaunchPad.Shared.Domain;
    using DeploySoftware.LaunchPad.Space.Satellites.Common;
    using DeploySoftware.LaunchPad.Space.Satellites.Common.ObservationFiles;

    [Table("Radarsat1Observations")]
    public class Radarsat1Observation : EarthObservationBase<Guid>, IHasCreationTime, IHasModificationTime
    {
        public enum FileTypes
        {
            nvol = 0,
            sard = 1,
            sarl = 2,
            sart = 3,
            tif = 4,
            tfw = 5,
            vol = 6 
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
       
        public Radarsat1ObservationFiles<Guid> Files { get; set; }

        public Radarsat1Observation(
           string _sceneId,
           string _mdaOrderNumber,
           string _geographicalArea,
           DateTime _sceneStart,
           DateTime _sceneStop
        )
        {
            Id = Guid.NewGuid();
            SceneId = _sceneId;
            MdaOrderNumber = _mdaOrderNumber;
            GeographicalArea = _geographicalArea;
            SceneStartTime = _sceneStart;
            SceneStopTime = _sceneStop;
            CreationTime = Clock.Now;
            LastModificationTime = Clock.Now;
        }

        public Radarsat1Observation(
           string _sceneId,
           string _mdaOrderNumber,
           string _geographicalArea,
           DateTime _sceneStart,
           DateTime _sceneStop,
           string _orbit,
           string _orbitDataType,
           string _applicationLut,
           string _beamMode,
           string _productType,
           string _format,
           int _numberImageLines,
           int _numberImagePixels,
           string _pixelSpacing,
           GeographicLocation _sceneCentre,
           ImageObservationCornerCoordinates _cornerCoordinates
        )
        {
            Id = Guid.NewGuid();
            SceneId = _sceneId;
            MdaOrderNumber = _mdaOrderNumber;
            GeographicalArea = _geographicalArea;
            SceneStartTime = _sceneStart;
            SceneStopTime = _sceneStop;
            Orbit = _orbit;
            OrbitDataType = _orbitDataType;
            ApplicationLut = _applicationLut;
            BeamMode = _beamMode;
            ProductType = _productType;
            Format = _format;
            NumberImageLines = _numberImageLines;
            NumberImagePixels = _numberImagePixels;
            PixelSpacing = _pixelSpacing;
            SceneCentre = _sceneCentre;
            Corners = _cornerCoordinates;
            CreationTime = Clock.Now;
            LastModificationTime = Clock.Now;
        }
        

        protected Radarsat1Observation()
        {

        }

        public class Radarsat1ObservationFiles<Guid> : IObservationFiles<Guid>
        {
            public NvolFile<Guid> Nvol { get; set; }

            public SardFile<Guid> Sard { get; set; }

            public SarlFile<Guid> Sarl { get; set; }

            public SartFile<Guid> Sart { get; set; }

            // Original image resolution (unmodified)
            public TifFile<Guid> TifOriginal { get; set; }

            // Thumbnail(default 150px x 150px max)
            public TifFile<Guid> TifThumbnail { get; set; }

            // Medium resolution (default 300px x 300px max)
            public TifFile<Guid> TifMedium { get; set; }

            // Large resolution (default 1024px x 1024px max)
            public TifFile<Guid> TifLarge { get; set; }

            public TfwFile<Guid> Tfw { get; set; }

            public VolFile<Guid> Vol { get; set; }
        }

    }
}
