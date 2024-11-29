// ***********************************************************************
// Assembly         : Deploy.LaunchPad.Space.Satellites
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 07-26-2023
// ***********************************************************************
// <copyright file="Radarsat1ObservationScene.cs" company="Deploy Software Solutions, inc.">
//     2018-2024 Deploy Software Solutions, inc.
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
    using Abp.Timing;
    using Deploy.LaunchPad.Core.Abp.Domain;
    using Deploy.LaunchPad.Core.Domain;
    using Deploy.LaunchPad.Core.Files;
    using Deploy.LaunchPad.Core.Geospatial;
    using Deploy.LaunchPad.Space.Satellites.Core;
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// Class Radarsat1ObservationScene.
    /// Implements the <see cref="EarthObservationModelBase" />
    /// Implements the <see cref="Deploy.LaunchPad.Space.Satellites.GoC.IRadarsatObservationScene" />
    /// </summary>
    /// <seealso cref="EarthObservationModelBase" />
    /// <seealso cref="Deploy.LaunchPad.Space.Satellites.GoC.IRadarsatObservationScene" />
    [Table("DssRadarsat1Observations")]
    public partial class Radarsat1ObservationScene : EarthObservationModelBase,
        IRadarsatObservationScene
    {
        /// <summary>
        /// Enum FileTypes
        /// </summary>
        public enum FileTypes
        {
            /// <summary>
            /// The nvol
            /// </summary>
            Nvol = 0,
            /// <summary>
            /// The sard
            /// </summary>
            Sard = 1,
            /// <summary>
            /// The sarl
            /// </summary>
            Sarl = 2,
            /// <summary>
            /// The sart
            /// </summary>
            Sart = 3,
            /// <summary>
            /// The tif
            /// </summary>
            Tif = 4,
            /// <summary>
            /// The TFW
            /// </summary>
            Tfw = 5,
            /// <summary>
            /// The vol
            /// </summary>
            Vol = 6
        }

        /// <summary>
        /// Gets or sets the scene identifier.
        /// </summary>
        /// <value>The scene identifier.</value>
        [Required]
        public string SceneId { get; set; }

        /// <summary>
        /// Gets or sets the mda order number.
        /// </summary>
        /// <value>The mda order number.</value>
        [Required]
        public string MdaOrderNumber { get; set; }

        /// <summary>
        /// Gets or sets the geographical area.
        /// </summary>
        /// <value>The geographical area.</value>
        [Required]
        public string GeographicalArea { get; set; }

        /// <summary>
        /// Gets or sets the scene start time.
        /// </summary>
        /// <value>The scene start time.</value>
        [Required]
        public DateTime SceneStartTime { get; set; }

        /// <summary>
        /// Gets or sets the scene stop time.
        /// </summary>
        /// <value>The scene stop time.</value>
        [Required]
        public DateTime SceneStopTime { get; set; }

        /// <summary>
        /// Gets or sets the orbit.
        /// </summary>
        /// <value>The orbit.</value>
        [Required]
        public string Orbit { get; set; }

        /// <summary>
        /// Gets or sets the type of the orbit data.
        /// </summary>
        /// <value>The type of the orbit data.</value>
        [Required]
        public string OrbitDataType { get; set; }

        /// <summary>
        /// Gets or sets the application lut.
        /// </summary>
        /// <value>The application lut.</value>
        [Required]
        public string ApplicationLut { get; set; }

        /// <summary>
        /// Gets or sets the beam mode.
        /// </summary>
        /// <value>The beam mode.</value>
        [Required]
        public string BeamMode { get; set; }

        /// <summary>
        /// Gets or sets the type of the product.
        /// </summary>
        /// <value>The type of the product.</value>
        [Required]
        public string ProductType { get; set; }

        /// <summary>
        /// Gets or sets the format.
        /// </summary>
        /// <value>The format.</value>
        [Required]
        public string Format { get; set; }

        /// <summary>
        /// Gets or sets the number image lines.
        /// </summary>
        /// <value>The number image lines.</value>
        [Required]
        public int NumberImageLines { get; set; }

        /// <summary>
        /// Gets or sets the number image pixels.
        /// </summary>
        /// <value>The number image pixels.</value>
        [Required]
        public int NumberImagePixels { get; set; }

        /// <summary>
        /// Gets or sets the pixel spacing.
        /// </summary>
        /// <value>The pixel spacing.</value>
        [Required]
        public string PixelSpacing { get; set; }

        /// <summary>
        /// Gets or sets the files.
        /// </summary>
        /// <value>The files.</value>
        public Radarsat1ObservationFiles Files { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Radarsat1ObservationScene"/> class.
        /// </summary>
        /// <param name="tenantId">The tenant identifier.</param>
        /// <param name="sceneId">The scene identifier.</param>
        public Radarsat1ObservationScene(
           int? tenantId,
           string sceneId)
        {
            SceneId = sceneId;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Radarsat1ObservationScene"/> class.
        /// </summary>
        /// <param name="tenantId">The tenant identifier.</param>
        /// <param name="sceneId">The scene identifier.</param>
        /// <param name="mdaOrderNumber">The mda order number.</param>
        /// <param name="geographicalArea">The geographical area.</param>
        /// <param name="sceneStart">The scene start.</param>
        /// <param name="sceneStop">The scene stop.</param>
        /// <param name="orbit">The orbit.</param>
        /// <param name="orbitDataType">Type of the orbit data.</param>
        /// <param name="applicationLut">The application lut.</param>
        /// <param name="beamMode">The beam mode.</param>
        /// <param name="productType">Type of the product.</param>
        /// <param name="format">The format.</param>
        /// <param name="numberImageLines">The number image lines.</param>
        /// <param name="numberImagePixels">The number image pixels.</param>
        /// <param name="pixelSpacing">The pixel spacing.</param>
        /// <param name="sceneCentre">The scene centre.</param>
        /// <param name="cornerCoordinates">The corner coordinates.</param>
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
           GeographicPosition sceneCentre,
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

        /// <summary>
        /// Initializes a new instance of the <see cref="Radarsat1ObservationScene"/> class.
        /// </summary>
        protected Radarsat1ObservationScene() : base()
        {
            CurrentLocation = new SpaceTimeInformation();
        }

        /// <summary>
        /// Class Radarsat1ObservationFiles.
        /// Implements the <see cref="Deploy.LaunchPad.Space.Satellites.Core.IObservationFiles{System.Guid}" />
        /// </summary>
        /// <seealso cref="Deploy.LaunchPad.Space.Satellites.Core.IObservationFiles{System.Guid}" />
        public partial class Radarsat1ObservationFiles : IObservationFiles<Guid>
        {
            /// <summary>
            /// Gets or sets the nvol.
            /// </summary>
            /// <value>The nvol.</value>
            public NvolFile<Guid> Nvol { get; set; }

            /// <summary>
            /// Gets or sets the sard.
            /// </summary>
            /// <value>The sard.</value>
            public SardFile<Guid> Sard { get; set; }

            /// <summary>
            /// Gets or sets the sarl.
            /// </summary>
            /// <value>The sarl.</value>
            public SarlFile<Guid> Sarl { get; set; }

            /// <summary>
            /// Gets or sets the sart.
            /// </summary>
            /// <value>The sart.</value>
            public SartFile<Guid> Sart { get; set; }

            /// <summary>
            /// Gets or sets the tif.
            /// </summary>
            /// <value>The tif.</value>
            public TifFile Tif { get; set; }

            /// <summary>
            /// Gets or sets the TFW.
            /// </summary>
            /// <value>The TFW.</value>
            public TifWorldFile Tfw { get; set; }

            /// <summary>
            /// Gets or sets the vol.
            /// </summary>
            /// <value>The vol.</value>
            public VolFile<Guid> Vol { get; set; }
        }

    }
}
