// ***********************************************************************
// Assembly         : Deploy.LaunchPad.Space.Satellites
// Author           : Nicholas Kellett
// Created          : 11-19-2023
//
// Last Modified By : Nicholas Kellett
// Last Modified On : 07-26-2023
// ***********************************************************************
// <copyright file="Radarsat1MetadataParser.cs" company="Deploy Software Solutions, inc.">
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


namespace Deploy.LaunchPad.Space.Satellites.GoC
{
    using Deploy.LaunchPad.Domain;
    using Deploy.LaunchPad.Core.Abp;
    using Deploy.LaunchPad.Domain.Model;
    using Deploy.LaunchPad.Files;
    using Deploy.LaunchPad.Files.Storage;
    using Deploy.LaunchPad.Util;
    using Deploy.LaunchPad.Space.Satellites.Core;
    using NetTopologySuite.Geometries;
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Text;
    using System.Text.RegularExpressions;
    using Deploy.LaunchPad.Core.Licenses;
    using Deploy.LaunchPad.Geospatial.Position;
    using Deploy.LaunchPad.Files.Storage;
    using Deploy.LaunchPad.Files;
    using Deploy.LaunchPad.Core.Elements;

    /// <summary>
    /// Utility to parse a Radarsat1 image observation metadata file and populate a Radarsat1Observation object from it.
    /// </summary>
    /// <typeparam name="TPrimaryKey">The type of the t primary key.</typeparam>
    /// <typeparam name="TFileStorageLocationType">The type of the t file storage location type.</typeparam>
    public partial class Radarsat1MetadataParser<TPrimaryKey, TFileStorageLocationType>
            where TFileStorageLocationType : IFileStorageLocation, new()
    {
        /// <summary>
        /// Main utility method to parse a Radarsat1 Metadata text file and populate the Radarsat1 Image Observation from the information
        /// contained within the file.
        /// The metadata is in RADARSAT CEOS format and is (c) Canadian Space Agency 1997 Agence spatiale canadienne, and processed and distributed by MDA Geospatial Services Inc.
        /// The data and metadata used within this software has been made freely available by the Canadian Federal Government under its Open Government initiative,
        /// subject to the following license terms: https://open.canada.ca/en/open-government-licence-canada
        /// </summary>
        /// <param name="radarsat1MetadataFilename">The radarsat1 metadata filename.</param>
        /// <returns>A populated Radarsat1Observation object containing the metadata values, or null if the object couldn't be populated</returns>
        /// <exception cref="System.IO.FileLoadException"></exception>
        /// <exception cref="ArgumentNullException"></exception>
        /// <throws>A FileLoadException if the file cannot be found or loaded</throws>
        public Radarsat1ObservationScene GetRadarsat1ObservationFromMetadataFile(string radarsat1MetadataFilename)
        {
            Radarsat1ObservationScene observation = null;
            // Radarsat 1 metadata files are in .txt format
            // ReSharper disable once RedundantAssignment
            var metadataFileText = string.Empty;
            if (radarsat1MetadataFilename.EndsWith(".txt"))
            {
                try
                {   // Open the Radarsat1 Metadata text file
                    using (StreamReader sr = new StreamReader(radarsat1MetadataFilename, Encoding.GetEncoding("iso-8859-1")))
                    {
                        metadataFileText = sr.ReadToEnd();
                    }
                }
                catch (Exception ex)
                {
                    throw new FileLoadException(ex.Message);
                }
                String radarsatUniqueId = Path.GetFileNameWithoutExtension(radarsat1MetadataFilename);
                String sceneId = metadataFileText.FindStringWithinAnchorText("SCENE_ID", "MDA ORDER NUMBER", true, true);
                String mdaOrderNumber = metadataFileText.FindStringWithinAnchorText("MDA ORDER NUMBER", "GEOGRAPHICAL AREA", true, true);
                String geographicalArea = metadataFileText.FindStringWithinAnchorText("GEOGRAPHICAL AREA", "SCENE START TIME", true, true);
                String sceneStartTimeText = metadataFileText.FindStringWithinAnchorText("SCENE START TIME", "SCENE STOP TIME", true, true);
                const string dateFormatPattern = "MMM dd yyyy HH:mm:ss.FFF";
                Guard.Against<ArgumentNullException>(
                    String.IsNullOrEmpty(sceneStartTimeText),
                    Deploy_LaunchPad_Space_Resources.Exception_Radarsat1MetadataParser_GetRadarsat1ObservationFromMetadataFile_SceneStartTime_ArgumentNullExpection
                );
                DateTime.TryParseExact(sceneStartTimeText,
                                       dateFormatPattern,
                                       CultureInfo.InvariantCulture,
                                       DateTimeStyles.None,
                                       out DateTime sceneStartTime);
                String sceneStopTimeText = metadataFileText.FindStringWithinAnchorText("SCENE STOP TIME", "ORBIT", true, true);
                Guard.Against<ArgumentNullException>(
                    String.IsNullOrEmpty(sceneStopTimeText),
                    Deploy_LaunchPad_Space_Resources.Exception_Radarsat1MetadataParser_GetRadarsat1ObservationFromMetadataFile_SceneStopTime_ArgumentNullExpection
                    );
                DateTime.TryParseExact(sceneStopTimeText,
                                           dateFormatPattern,
                                           CultureInfo.InvariantCulture,
                                           DateTimeStyles.None,
                                           out DateTime sceneStopTime);
                String orbit = metadataFileText.FindStringWithinAnchorText("ORBIT", "ORBIT DATA TYPE", true, true);
                String orbitDataType = metadataFileText.FindStringWithinAnchorText("ORBIT DATA TYPE", "APPLICATION LUT", true, true);
                String applicationLut = metadataFileText.FindStringWithinAnchorText("APPLICATION LUT", "BEAM MODE", true, true);
                String beamMode = metadataFileText.FindStringWithinAnchorText("BEAM MODE", "PRODUCT TYPE", true, true);
                String productType = metadataFileText.FindStringWithinAnchorText("PRODUCT TYPE", "FORMAT", true, true);
                String format = metadataFileText.FindStringWithinAnchorText("FORMAT", "# OF IMAGE LINES", true, true);
                Int32.TryParse(metadataFileText.FindStringWithinAnchorText("# OF IMAGE LINES", "# OF IMAGE PIXELS", true, true), out var numberImageLines);
                Int32.TryParse(metadataFileText.FindStringWithinAnchorText("# OF IMAGE PIXELS", "PIXEL SPACING", true, true), out var numberImagePixels);
                String pixelSpacing = metadataFileText.FindStringWithinAnchorText("PIXEL SPACING", "SCENE CENTRE", true, true);


                // get the scene centre coordinates. Centre is spelled properly in Canadian, eh.
                String sceneCentre = metadataFileText.FindStringWithinAnchorText("SCENE CENTRE", "CORNER COORDINATES", true, true);
                Guard.Against<ArgumentNullException>(
                    String.IsNullOrEmpty(sceneCentre),
                    Deploy_LaunchPad_Space_Resources.Exception_Radarsat1MetadataParser_GetRadarsat1ObservationFromMetadataFile_SceneCentre_ArgumentNullExpection
                );
                // ReSharper disable once PossibleNullReferenceException
                string[] latLongSplit = sceneCentre.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                Coordinate c = new Coordinate();
                Double.TryParse(latLongSplit[0], out double latitude);
                c.Y = latitude;
                Double.TryParse(latLongSplit[1], out double longitude);
                c.X = longitude;
                GeographicPosition centre = new GeographicPosition
                (
                    new Point(
                    c.Y,
                    c.X),0
                );

                // get the image corner coordinates
                String cornerCoordinatesString = metadataFileText.FindStringWithinAnchorText("CORNER COORDINATES:", "For information on RADARSAT CEOS format see README.TXT", true, true);
                Guard.Against<ArgumentNullException>(
                    String.IsNullOrEmpty(cornerCoordinatesString),
                    Deploy_LaunchPad_Space_Resources.Exception_Radarsat1MetadataParser_GetRadarsat1ObservationFromMetadataFile_CornerCoordinates_ArgumentNullExpection
                );
                ImageObservationCornerCoordinates cornerCoords = GetCornerCoordinates(cornerCoordinatesString);

                ILicense license = new OpenGovernmentCanadaLicense();
                IUsageRights copyright = new Radarsat1DataUsageRights();

                // Create a new Radarsat1 Earth Observation image
                observation = new Radarsat1ObservationScene(
                    null, // tenant Id - not used in this application
                    sceneId,
                    mdaOrderNumber,
                    geographicalArea,
                    sceneStartTime,
                    sceneStopTime,
                    orbit,
                    orbitDataType,
                    applicationLut,
                    beamMode,
                    productType,
                    format,
                    numberImageLines,
                    numberImagePixels,
                    pixelSpacing,
                    centre,
                    cornerCoords
                )
                {
                    Name = new ElementName(radarsatUniqueId),

                    //Metadata.Description = radarsatUniqueId,
                    Copyright = copyright
                };

            }

            // ReSharper disable once PossibleNullReferenceException
            observation.Files = LoadExpectedObservationFiles(observation, radarsat1MetadataFilename);
            return observation;
        }

        /// <summary>
        /// Loads the expected observation files.
        /// </summary>
        /// <param name="observation">The observation.</param>
        /// <param name="radarsat1MetadataFilename">The radarsat1 metadata filename.</param>
        /// <returns>Radarsat1ObservationScene.Radarsat1ObservationFiles.</returns>
        protected Radarsat1ObservationScene.Radarsat1ObservationFiles LoadExpectedObservationFiles(Radarsat1ObservationScene observation, string radarsat1MetadataFilename)
        {
            // get the list of related observation files
            String baseFilePath = radarsat1MetadataFilename.Substring(0, radarsat1MetadataFilename.Length - 4);
            IList<KeyValuePair<Radarsat1ObservationScene.FileTypes, String>> expectedFiles = new List<KeyValuePair<Radarsat1ObservationScene.FileTypes, String>>
            {
                new KeyValuePair<Radarsat1ObservationScene.FileTypes, string>(
                Radarsat1ObservationScene.FileTypes.Nvol,
                baseFilePath + "." + Radarsat1ObservationScene.FileTypes.Nvol
                ),
                new KeyValuePair<Radarsat1ObservationScene.FileTypes, string>(
                Radarsat1ObservationScene.FileTypes.Sard, baseFilePath + "." + Radarsat1ObservationScene.FileTypes.Sard
                ),
                new KeyValuePair<Radarsat1ObservationScene.FileTypes, string>(
                Radarsat1ObservationScene.FileTypes.Sarl, baseFilePath + "." + Radarsat1ObservationScene.FileTypes.Sarl
                ),
                new KeyValuePair<Radarsat1ObservationScene.FileTypes, string>(
                Radarsat1ObservationScene.FileTypes.Sart, baseFilePath + "." + Radarsat1ObservationScene.FileTypes.Sart
                ),
                new KeyValuePair<Radarsat1ObservationScene.FileTypes, string>(
                Radarsat1ObservationScene.FileTypes.Tfw, baseFilePath + "." + Radarsat1ObservationScene.FileTypes.Tfw
                ),
                new KeyValuePair<Radarsat1ObservationScene.FileTypes, string>(
                Radarsat1ObservationScene.FileTypes.Tif, baseFilePath + "." + Radarsat1ObservationScene.FileTypes.Tif
                ),
                new KeyValuePair<Radarsat1ObservationScene.FileTypes, string>(
                Radarsat1ObservationScene.FileTypes.Vol, baseFilePath + "." + Radarsat1ObservationScene.FileTypes.Vol
                )
            };

            // initialize the list of observation files
            Radarsat1ObservationScene.Radarsat1ObservationFiles observationFiles = new Radarsat1ObservationScene.Radarsat1ObservationFiles();

            var location = new TFileStorageLocationType();
            location.RootUri = new System.Uri(Directory.GetCurrentDirectory());

            // add each expected file type (if it exists)
            if (File.Exists(expectedFiles[0].Value))
            {
                observationFiles.Nvol = new NvolFile<Guid>()
                {
                    Id = SequentialGuid.NewGuid(),
                    Name = new ElementName(Path.GetFileName(expectedFiles[0].Value)) 
                };
            }
            if (File.Exists(expectedFiles[1].Value))
            {
                observationFiles.Sard = new SardFile<Guid>()
                {
                    Id = SequentialGuid.NewGuid(),
                    Name = new ElementName(Path.GetFileName(expectedFiles[1].Value))
                };
            }
            if (File.Exists(expectedFiles[2].Value))
            {
                observationFiles.Sarl = new SarlFile<Guid>()
                {
                    Id = SequentialGuid.NewGuid(),
                    Name = new ElementName(Path.GetFileName(expectedFiles[2].Value))
                };
            }
            if (File.Exists(expectedFiles[3].Value))
            {
                observationFiles.Sart = new SartFile<Guid>()
                {
                    Id = SequentialGuid.NewGuid(),
                    Name = new ElementName(Path.GetFileName(expectedFiles[3].Value))
                };
            }
            if (File.Exists(expectedFiles[4].Value))
            {
                observationFiles.Tfw = new TifWorldFile(Path.GetFileName(expectedFiles[4].Value))
                {
                };
            }
            if (File.Exists(expectedFiles[5].Value))
            {
                observationFiles.Tif = new TifFile(Path.GetFileName(expectedFiles[5].Value));
            }
            if (File.Exists(expectedFiles[6].Value))
            {
                observationFiles.Vol = new VolFile<Guid>()
                {
                    Id = SequentialGuid.NewGuid(),
                    Name = new ElementName(Path.GetFileName(expectedFiles[6].Value))
                };
            }
            return observationFiles;
        }

        /// <summary>
        /// Returns a series of four corner coordinates from a provided string of coordinate information.
        /// </summary>
        /// <param name="cornerCoordinatesString">The coordinate information to parse</param>
        /// <returns>An ImageObservationCornerCoordinates object containing four coordinate points for each side of a square</returns>
        protected ImageObservationCornerCoordinates GetCornerCoordinates(string cornerCoordinatesString)
        {
            string[] coordinates = Regex.Split(cornerCoordinatesString, @"(?<=[N,S,E,W])");
            ImageObservationCornerCoordinates cornerCoords = new ImageObservationCornerCoordinates();

            Coordinate UpperLeft = new Coordinate();
            Coordinate UpperRight = new Coordinate();
            Coordinate LowerLeft = new Coordinate();
            Coordinate LowerRight = new Coordinate();


            // upper left latitude
            if (!String.IsNullOrEmpty(coordinates[0]))
            {
                Double.TryParse(coordinates[0].Trim(), out double ulLat);
                UpperLeft.Y = ulLat;
            }
            // upper left longitude
            if (!String.IsNullOrEmpty(coordinates[2]))
            {
                Double.TryParse(coordinates[2].Trim(), out double ulLong);
                UpperLeft.X = ulLong;
            }
            // upper right latitude
            if (!String.IsNullOrEmpty(coordinates[1]))
            {
                Double.TryParse(coordinates[1].Trim(), out double urLat);
                UpperRight.Y = urLat;

            }
            // upper right longitude
            if (!String.IsNullOrEmpty(coordinates[3]))
            {
                Double.TryParse(coordinates[3].Trim(), out double urLong);
                UpperRight.X = urLong;
            }
            // lower left latitude
            if (!String.IsNullOrEmpty(coordinates[4]))
            {
                Double.TryParse(coordinates[4].Trim(), out double llLat);
                LowerLeft.Y = llLat;
            }
            // lower left longitude
            if (!String.IsNullOrEmpty(coordinates[6]))
            {
                Double.TryParse(coordinates[6].Trim(), out double llLong);
                LowerLeft.X = llLong;
            }
            // lower right latitude
            if (!String.IsNullOrEmpty(coordinates[5]))
            {
                Double.TryParse(coordinates[5].Trim(), out double lrLat);
                LowerRight.Y = lrLat;
            }
            // lower right longitude
            if (!String.IsNullOrEmpty(coordinates[7]))
            {
                Double.TryParse(coordinates[7].Trim(), out double lrLong);
                LowerRight.X = lrLong;
            }
            cornerCoords.LowerLeft = LowerLeft;
            cornerCoords.LowerRight = LowerRight;
            cornerCoords.UpperLeft = UpperLeft;
            cornerCoords.LowerRight = UpperRight;
            return cornerCoords;
        }


    }
}
