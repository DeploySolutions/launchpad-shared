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
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Abp.Timing;
    using DeploySoftware.LaunchPad.Shared.Domain;
    using DeploySoftware.LaunchPad.Space.Satellites.Common;
    using DeploySoftware.LaunchPad.Space.Satellites.Common.ObservationFiles;

    [Table("Radarsat1Observations")]
    public class Radarsat1Observation : EarthObservationBase<Guid>, IRadarsatObservation<Guid>
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
        
        public Radarsat1Observation(
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
        )
        {
            Id = Guid.NewGuid();
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
        
        protected Radarsat1Observation() : base()
        {
            Id = Guid.NewGuid();
            CurrentLocation = new SpaceTimeInformation();
        }

        public class Radarsat1ObservationFiles : IObservationFiles<Guid>
        {
            public NvolFile<Guid> Nvol { get; set; }

            public SardFile<Guid> Sard { get; set; }

            public SarlFile<Guid> Sarl { get; set; }

            public SartFile<Guid> Sart { get; set; }
            
            public TifFile<Guid> Tif { get; set; }

            public TfwFile<Guid> Tfw { get; set; }

            public VolFile<Guid> Vol { get; set; }
        }

    }
}
